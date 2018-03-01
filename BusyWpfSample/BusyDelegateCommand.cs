using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusyWpfSample
{
    public class BusyDelegateCommand : DelegateCommandBase
    {
        private Action _executeMethod;
        private Func<bool> _canExecuteMethod;

        volatile bool IsBusy;

        MainViewModel _vm;

        public BusyDelegateCommand(Action executeMethod)
        {
            this._executeMethod = executeMethod;
            this._canExecuteMethod = new Func<bool>(() => true);
        }

        public BusyDelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            this._executeMethod = executeMethod;
            this._canExecuteMethod = canExecuteMethod;
        }

        public BusyDelegateCommand(MainViewModel vm, Action executeMethod)
        {
            this._executeMethod = executeMethod;
            this._canExecuteMethod = new Func<bool>(() => true);
            this._vm = vm;
        }

        public BusyDelegateCommand(MainViewModel vm, Action executeMethod, Func<bool> canExecuteMethod)
        {
            this._executeMethod = executeMethod;
            this._canExecuteMethod = canExecuteMethod;
            this._vm = vm;
        }

        protected override bool CanExecute(object parameter)
        {
            return _canExecuteMethod.Invoke() && !IsBusy;
        }

        protected override async void Execute(object parameter)
        {
            IsBusy = true;
            _vm.IsBusy = true;
            RaiseCanExecuteChanged();

            await Execute();

            IsBusy = false;
            _vm.IsBusy = false;
            RaiseCanExecuteChanged();
        }

        public async Task Execute()
        {
            await Task.Run(_executeMethod);
        }


    }
}
