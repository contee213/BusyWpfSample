using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using System.Windows.Input;
using Prism.Commands;
using System.Threading;

namespace BusyWpfSample
{
    public class MainViewModel : BindableBase
    {
        private bool isBusy;

        private ICommand customActionCommand;

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        public ICommand CustomActionCommand
        {
            get { return customActionCommand; }
            set { SetProperty(ref customActionCommand, value); }
        }

        public MainViewModel()
        {
            // CustomActionCommand = new DelegateCommand(CustomAction);
            CustomActionCommand = new BusyDelegateCommand(this, CustomAction2, CanCustomAction2);
        }

        private bool CanCustomAction2()
        {
            return true;
        }

        public async void CustomAction()
        {
            IsBusy = true;

            await CustomActionImpl();

            IsBusy = false;
        }

        public Task CustomActionImpl()
        {
            return Task.Run(() => 
            {
                Thread.Sleep(5000);
            });
        }

        public void CustomAction2()
        {
            Console.WriteLine("Start");

            Thread.Sleep(5000);

            Console.WriteLine("End");
        }
    }
}
