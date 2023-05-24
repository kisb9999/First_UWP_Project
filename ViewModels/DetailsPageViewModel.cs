using Homework.Models;
using Homework.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.ViewModels
{
    public class DetailsPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //Variables that store the book/character/house data.
        private BookObject book;
        public BookObject Book
        {
            get { return book; }
            set
            {
                book = value;
                OnPropertyChanged(nameof(Book));
            }
        }
        private CharacterObject character;
        public CharacterObject Character
        {
            get { return character; }
            set
            {
                character = value;
                OnPropertyChanged(nameof(Character));
            }
        }
        private HouseObject house;
        public HouseObject House
        {
            get { return house; }
            set
            {
                house = value;
                OnPropertyChanged(nameof(House));
            }
        }

        //This helps with converting the URLs into character names.
        private ObservableCollection<CharacterObject> characterList;
        public ObservableCollection<CharacterObject> CharacterList
        {
            get { return characterList; }
            set
            {
                characterList = value;
                OnPropertyChanged(nameof(CharacterList));
            }
        }



        private ObservableCollection<CharacterObject> parentList;
        public ObservableCollection<CharacterObject> ParentList
        {
            get { return parentList; }
            set
            {
                parentList = value;
                OnPropertyChanged(nameof(ParentList));
            }
        }


        //private ObservableCollection<CharacterObject> allcharacters;
        //public ObservableCollection<CharacterObject> AllCharacters
        //{
        //    get { return allcharacters; }
        //    set
        //    {
        //        allcharacters = value;
        //        OnPropertyChanged(nameof(AllCharacters));
        //    }
        //}

        private string url;
        public string Url
        {
            get { return url; }
            set
            {
                url = value;
                OnPropertyChanged(nameof(Url));
            }
        }

        public async Task LoadDataAsync(string id)
        {
            var service = new GOT_Service();

            //The id is an url so the if statements check whether it is a book/house/character call.
            if (id.IndexOf("books") != -1)
            {
                //get book data
                Book = await service.GetBookAsync(id);
                
                //init CharacterList
                CharacterList = new ObservableCollection<CharacterObject>();

                //Listing all the characters that are in the book.
                //This part is to convert the URLs into a string format (name).
                foreach(string name in Book.characters)
                {
                    //Had to add a limit otherwise there would be too many API calls.
                    if (CharacterList.Count() > 20)
                        break;

                    var co = await service.GetCharacterAsync(name);
                    
                    if(!string.IsNullOrEmpty(co.name))
                        CharacterList.Add(co);
                }

                //var c = await service.GetCharacterGroupAsync();
                //AllCharacters = new ObservableCollection<CharacterObject>(c);

                //foreach(CharacterObject co in AllCharacters)
                //{
                //    bool contains = false;
                //    for(int i = 0; i<Book.characters.Count(); i++)
                //    {
                //        if (co.url == Book.characters[i])
                //        {
                //            contains = true;
                //            break;
                //        }
                //    }
                //    if (contains && co.name != null)
                //    {
                //        CharacterList.Add(co.name);
                //    }
                //}
            }
            else if (id.IndexOf("characters") != -1)
            {
                //Get character data
                Character = await service.GetCharacterAsync(id);

                //This part is to convert the spouse URL into a name.
                CharacterList = new ObservableCollection<CharacterObject>();
                ParentList = new ObservableCollection<CharacterObject>();

                var co = await service.GetCharacterAsync(Character.spouse);
                if (!string.IsNullOrEmpty(co.name))
                    CharacterList.Add(co);

                //This part is to convert the parents URLs into names.
                var cof = await service.GetCharacterAsync(Character.father);
                var com = await service.GetCharacterAsync(Character.mother);
                if(!string.IsNullOrEmpty(com.name))
                    ParentList.Add(com);
                if (!string.IsNullOrEmpty(cof.name))
                    ParentList.Add(cof);
            }
            else
            {
                //Get house data
                House = await service.GetHouseAsync(id);

                //This part is to convert the current lord URL into a name.
                CharacterList = new ObservableCollection<CharacterObject>();

                var co = await service.GetCharacterAsync(House.currentLord);
                if (!string.IsNullOrEmpty(co.name))
                    CharacterList.Add(co);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
