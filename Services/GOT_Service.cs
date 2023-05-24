using Homework.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace Homework.Services
{
    //This class is for loading the data from the API.
    public class GOT_Service
    {

        private readonly Uri serverUrl = new Uri("https://anapioficeandfire.com/");

        //Asynchronous method for the Http GET command.
        private async Task<T> GetAsync<T>(Uri uri)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                T result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
        }

        //These list all the books/houses/characters.
        public async Task<List<BookObject>> GetBookGroupAsync()
        {
            return await GetAsync<List<BookObject>>(new Uri(serverUrl, "api/books?pageSize=12"));
        }

        public async Task<List<CharacterObject>> GetCharacterGroupAsync()
        {
            return await GetAsync<List<CharacterObject>>(new Uri(serverUrl, "api/characters?page=3&pageSize=30"));
        }

        public async Task<List<HouseObject>> GetHouseGroupAsync()
        {
            return await GetAsync<List<HouseObject>>(new Uri(serverUrl, "api/houses?pageSize=30"));
        }

        //These list the next page. There is no need for a book method here because there are only 12 of them in the API.
        public async Task<List<CharacterObject>> GetCharacterGroupAsync(int id)
        {
            return await GetAsync<List<CharacterObject>>(new Uri(serverUrl, $"api/characters?page={id}&pageSize=30"));
        }

        public async Task<List<HouseObject>> GetHouseGroupAsync(int id)
        {
            return await GetAsync<List<HouseObject>>(new Uri(serverUrl, $"api/houses?page={id}&pageSize=30"));
        }

        //These methods return a single book/house/character object.
        public async Task<BookObject> GetBookAsync(string id)
        {
            return await GetAsync<BookObject>(new Uri(id));
        }

        public async Task<CharacterObject> GetCharacterAsync(string id)
        {
            return await GetAsync<CharacterObject>(new Uri(id));
        }

        public async Task<HouseObject> GetHouseAsync(string id)
        {
            return await GetAsync<HouseObject>(new Uri(id));
        }
    }
}
