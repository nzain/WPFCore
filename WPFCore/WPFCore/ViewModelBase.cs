using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace WPFCore
{
    /// <summary>Base class for all view models implements INotifyPropertyChanged and a 'loaded event' hook.</summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>Occurs when any of the RaisePropertyChanged methods is called. This is the implementation of
        /// INotifyPropertyChanged for WPF's data binding.</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Notifies WPF (and other listeners) that this (caller's context) property has changed. Don't enter the property
        /// name manually (use the expression variant instead for refactoring robustness).</summary>
        /// <param name="propertyName">[automatic] The compiler inserts the property name for you using the
        /// CallerMemberNameAttribute.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>Notifies WPF (and other listeners) that a certain property has changed, where the property name is given as an
        /// expression:
        /// <code>this.RaisePropertyChanged(() => this.Name);</code>
        /// </summary>
        /// <typeparam name="T">The type of the property.</typeparam>
        /// <param name="propertyExpression">The property expression in the form <c>() => this.SomeProperty</c>.</param>
        protected void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var memberExpr = propertyExpression.Body as MemberExpression;
            if (memberExpr == null)
            {
                throw new ArgumentException("propertyExpression should represent access to a member");
            }
            string propertyName = memberExpr.Member.Name;
            Debug.Assert(!string.IsNullOrWhiteSpace(propertyName), "property name from expression is null/empty");
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>Notifies this view model that the loaded event has occured on its view. Call this method from the view's
        /// Loaded event to propagate the event to this viewmodel. The viewmodel may override the no-op implementation to react
        /// accordingly.</summary>
        /// <param name="sender">The sender.</param>
        /// <see cref="System.Windows.Window.Loaded" />
        public virtual void OnLoaded(object sender)
        {
        }
    }
}