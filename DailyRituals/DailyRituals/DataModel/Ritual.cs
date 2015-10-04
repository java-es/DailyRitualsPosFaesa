using DailyRituals.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows.Input;

namespace DailyRituals.DataModel
{
    public class Ritual: INotifyPropertyChanged
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ObservableCollection<DateTime> Dates { get; set; }
        [IgnoreDataMember]
        public ICommand CompletedCommand { get; set; }

        public Ritual()
        {
            CompletedCommand = new CompletedButtonClick();
            Dates = new ObservableCollection<DateTime>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void AddDate()
        {
            Dates.Add(DateTime.Today);
            NotifyPropertyChanged("Dates");
        }
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
