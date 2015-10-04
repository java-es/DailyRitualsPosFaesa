using DailyRituals.DataModel;
using System;
using System.Windows.Input;

namespace DailyRituals.Command
{
    public class CompletedButtonClick : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            App.dataSource.CompleteRitualToday((Ritual)parameter);
        }
    }
}
