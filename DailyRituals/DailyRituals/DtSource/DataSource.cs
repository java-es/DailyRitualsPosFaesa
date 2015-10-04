using DailyRituals.DataModel;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Windows.Storage;

namespace DailyRituals.DtSource
{
    public class DataSource
    {
        const string fileName = "ritual.json";
        private ObservableCollection<Ritual> _ritual;
        public DataSource()
        {
            _ritual = new ObservableCollection<Ritual>();
        }

        private async Task EnsureDataLoaded()
        {
            if (_ritual.Count == 0)
            {
                await getRitualDataAsync();
            }
            return;
        }
        private async Task getRitualDataAsync()
        {
            if (_ritual.Count != 0)
            {
                return;
            }
            var jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<Ritual>));
            try
            {
                using (var stram = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(fileName))
                {
                    _ritual = (ObservableCollection<Ritual>)jsonSerializer.ReadObject(stram);

                }
            }
            catch
            {
                _ritual = new ObservableCollection<Ritual>();
            }
        }
        private async Task saveRitualDataAsync()
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(ObservableCollection<Ritual>));
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(fileName, CreationCollisionOption.ReplaceExisting))
            {
                jsonSerializer.WriteObject(stream, _ritual);
            }
        }

        public async void AddRitual(string name, string description)
        {
            var ritual = new Ritual();
            ritual.Name = name;
            ritual.Description = description;
            ritual.Dates = new ObservableCollection<DateTime>();
            _ritual.Add(ritual);
            await saveRitualDataAsync();
        }

        public async Task<ObservableCollection<Ritual>> GetRituals()
        {
            await EnsureDataLoaded();
            return _ritual;
        }

        public async void CompleteRitualToday(Ritual ritual)
        {
            int index = _ritual.IndexOf(ritual);
            _ritual[index].AddDate();
            await saveRitualDataAsync();
        }
    }
}
