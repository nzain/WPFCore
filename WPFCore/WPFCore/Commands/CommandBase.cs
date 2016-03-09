using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Input;

namespace WPFCore.Commands
{
    public abstract class CommandBase : ViewModelBase, ICommand
    {
        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public abstract void Execute(object parameter);

        public void RaiseCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>Listen on the given viewmodel and its property. This call can be chained to listen on multiple properties.</summary>
        /// <typeparam name="TViewModel">The type of viewmodel to listen on.</typeparam>
        /// <typeparam name="TPropertyType">The type of property to listen on.</typeparam>
        /// <param name="viewModel">The viewmodel.</param>
        /// <param name="propertyExpression">An expresion like <c>vm => vm.SomeProperty</c> that identifies the property this
        /// command's CanExecute status depends on.</param>
        /// <returns>This command for chaining multiple calls.</returns>
        public CommandBase ListenOn<TViewModel, TPropertyType>(TViewModel viewModel,
            Expression<Func<TViewModel, TPropertyType>> propertyExpression)
            where TViewModel : INotifyPropertyChanged
        {
            string propertyName = ExpressionHelper.GetPropertyName(propertyExpression);
            viewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == propertyName)
                {
                    this.RaiseCanExecuteChanged();
                }
            };
            return this;
        }

        /// <summary>Listen on the given viewmodel's validation errors.</summary>
        /// <param name="viewModel">The validatable viewmodel.</param>
        /// <returns>This command for chainging multiple calls.</returns>
        public CommandBase ListenOnValidationErrors(INotifyDataErrorInfo viewModel)
        {
            viewModel.ErrorsChanged += (sender, e) => this.RaiseCanExecuteChanged();
            return this;
        }
    }
}