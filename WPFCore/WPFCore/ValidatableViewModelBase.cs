using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace WPFCore
{
    /// <summary>Abstract base class for viewmodels that need validation (using Fluent Validation).</summary>
    /// <typeparam name="T">The type of viewmodel.</typeparam>
    public abstract class ValidatableViewModelBase<T> : ViewModelBase, INotifyDataErrorInfo
        where T : ViewModelBase
    {
        private readonly ConcurrentDictionary<string, string[]> _errors = new ConcurrentDictionary<string, string[]>();
        private IValidator<T> _validator;

        #region Protected

        /// <summary>Base constructor registers validation handler.</summary>
        protected ValidatableViewModelBase()
        {
            this.PropertyChanged += this.OnPropertyChanged;
        }

        /// <summary>Gets the validator for this class, where <c>T</c> is this type.</summary>
        protected abstract IValidator<T> Validator();

        #endregion

        #region INotifyDataErrorInfo Implementation

        /// <summary>[INotifyDataErrorInfo] This flag is <c>true</c> if this viewmodel has errors.</summary>
        public bool HasErrors => this._errors.Count > 0;

        /// <summary>Gets all errors ordered by property name and separated by newline.</summary>
        public string Errors
        {
            get
            {
                IEnumerable<string> allErrors = this._errors.OrderBy(o => o.Key).SelectMany(s => s.Value);
                return string.Join(Environment.NewLine, allErrors);
            }
        }

        /// <summary>[INotifyDataErrorInfo] Occurs when the errors of a property have changed (absence of errors included).</summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>[INotifyDataErrorInfo] Returns a <c>string[]</c> array of errors for a given propterty.</summary>
        public IEnumerable GetErrors(string propertyName)
        {
            string[] errors;
            return propertyName != null && this._errors.TryGetValue(propertyName, out errors) ? errors : new string[0];
        }

        #endregion

        #region Private Methods

        private IValidator<T> GetOrCreateValidatorInstance()
        {
            return this._validator ?? (this._validator = this.Validator());
        }

        // async void EventHandler - dangerous.
        private async void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var handler = this.ErrorsChanged;
            if (handler == null)
            {
                return; // quick return, if noone is listening (e.g. no view bound to vm)
            }
            var instance = this as T;
            if (instance == null)
            {
                throw new InvalidOperationException("generic T should be this type (" + this.GetType().Name + ")");
            }
            IValidator<T> validator = this.GetOrCreateValidatorInstance();
            if (validator == null)
            {
                throw new InvalidOperationException("IValidator<T> not implemented");
            }
            string propertyName = args.PropertyName;
            if (propertyName == null || propertyName == "HasErrors" || propertyName == "Errors")
            {
                return;
            }
            // do not fire & forget - viewmodels might listen to this event
            await this.ValidateAsync(instance, propertyName, validator);

            // send notifications, but don't re-trigger validation
            handler(instance, new DataErrorsChangedEventArgs(propertyName));
            this.PropertyChanged -= this.OnPropertyChanged;
            this.OnPropertyChanged(() => this.Errors);
            this.OnPropertyChanged(() => this.HasErrors);
            this.PropertyChanged += this.OnPropertyChanged;
        }

        private async Task ValidateAsync(T instance, string propertyName, IValidator<T> validator)
        {
            // clear existing errors for the given property
            string[] errors;
            this._errors.TryRemove(propertyName, out errors);
            ValidationResult results = await validator.ValidateAsync(instance, propertyName);
            if (!results.IsValid)
            {
                // eventually add new errors
                errors = results.Errors.Select(s => s.ErrorMessage).ToArray();
                this._errors.TryAdd(propertyName, errors);
            }
        }

        #endregion
    }
}