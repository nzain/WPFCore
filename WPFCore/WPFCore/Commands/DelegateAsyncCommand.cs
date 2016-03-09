using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace WPFCore.Commands
{
    /// <summary>An asynchronous command based on delegates.</summary>
    public class DelegateAsyncCommand : AsyncCommandBase
    {
        private readonly Func<object, CancellationToken, Task> _executeAsync;
        private readonly Predicate<object> _canExecute;

        /// <summary>Creates the command.</summary>
        /// <param name="executeAsync">The asynchronous action to perform.</param>
        public DelegateAsyncCommand(Func<object, CancellationToken, Task> executeAsync)
            : this(executeAsync, null)
        {
        }

        /// <summary>Creates the command.</summary>
        /// <param name="executeAsync">The asynchronous action to perform.</param>
        /// <param name="canExecute">This predicate defines, if the command is executable.</param>
        public DelegateAsyncCommand(Func<object, CancellationToken, Task> executeAsync, Predicate<object> canExecute)
        {
            if (executeAsync == null)
            {
                throw new ArgumentNullException(nameof(executeAsync));
            }
            this._executeAsync = executeAsync;
            this._canExecute = canExecute;
        }

        #region ICommand Members

        /// <summary>Checks, if this command can execute right now.</summary>
        /// <param name="parameter">An optional parameter.</param>
        /// <returns><c>true</c> if the command can execute right now.</returns>
        [DebuggerStepThrough]
        public override bool CanExecute(object parameter)
        {
            return (this._canExecute == null || this._canExecute(parameter))
                   && !this.IsBusy;
        }

        protected override Task ExecuteAsync(object parameter, CancellationToken cancellationToken)
        {
            return this._executeAsync(parameter, cancellationToken);
        }

        #endregion
    }
}