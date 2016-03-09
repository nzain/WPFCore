using System;
using System.Diagnostics;

namespace WPFCore.Commands
{
    /// <summary>Simple ICommand implementation.</summary>
    public class DelegateCommand : CommandBase
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        /// <summary>Creates a new command that is always executable.</summary>
        /// <param name="execute">The action to perform.</param>
        public DelegateCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>Creates a new command that is conditionally executable.</summary>
        /// <param name="execute">The action to perform.</param>
        /// <param name="canExecute">The condition that deterimines if the action is executable.</param>
        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute));
            }
            this._execute = execute;
            this._canExecute = canExecute;
        }

        #region ICommand Members

        /// <summary>Checks, if this command can execute right now.</summary>
        /// <param name="parameter">An optional parameter.</param>
        /// <returns><c>true</c> if the command can execute right now.</returns>
        [DebuggerStepThrough]
        public override bool CanExecute(object parameter)
        {
            return this._canExecute == null || this._canExecute(parameter);
        }

        /// <summary>Executes the command.</summary>
        /// <param name="parameter">An optional parameter.</param>
        public override void Execute(object parameter)
        {
            this._execute(parameter);
        }

        #endregion
    }
}