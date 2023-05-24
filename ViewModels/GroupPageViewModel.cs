using Homework.Models;
using Homework.Services;
using Homework.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Homework.ViewModels
{
    public class GroupPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //Variables that store the data groups from the API.
        private ObservableCollection<BookObject> books;
        public ObservableCollection<BookObject> Books
        {
            get { return books; }
            set 
            { 
                books = value;
                OnPropertyChanged(nameof(Books));
            }
        }

        private ObservableCollection<CharacterObject> characters;
        public ObservableCollection<CharacterObject> Characters
        {
            get { return characters; }
            set
            {
                characters = value;
                OnPropertyChanged(nameof(Characters));
            }
        }
        private ObservableCollection<HouseObject> houses;
        public ObservableCollection<HouseObject> Houses
        {
            get { return houses; }
            set
            {
                houses = value;
                OnPropertyChanged(nameof(Houses));
            }
        }

        //This method loads the data groups from the API into ObservableCollections.
        public async Task LoadDataAsync()
        {
            var service = new GOT_Service();

            //setting the page counter to its default value
            Counter = 1;

            //load every book information
            var books = await service.GetBookGroupAsync();
            Books = new ObservableCollection<BookObject>(books);

            //load every character information
            var characters = await service.GetCharacterGroupAsync();
            Characters = new ObservableCollection<CharacterObject>(characters);

            //load every house information
            var houses = await service.GetHouseGroupAsync();
            Houses = new ObservableCollection<HouseObject>(houses);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //for refreshing the data groups (houses, characters)
        public async Task LoadNewDataAsync(int id)
        {
            Counter++;
            var service = new GOT_Service();
            var characters = await service.GetCharacterGroupAsync(id);
            var houses = await service.GetHouseGroupAsync(id);

            Characters = new ObservableCollection<CharacterObject>(characters);
            Houses = new ObservableCollection<HouseObject>(houses);
        }

        //The counter stores the page number of the API.
        private int counter;
        public int Counter
        {
            get { return counter; }
            set 
            { 
                counter = value;
                OnPropertyChanged(nameof(Counter));
            }
        }
    }
}
