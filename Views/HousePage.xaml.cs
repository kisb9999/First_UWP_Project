using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Homework.ViewModels;
using Homework.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Homework.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HousePage : Page
    {
        public HousePage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            await HouseViewModel.LoadDataAsync();
        }

        //When we click on a character item we will be navigated to the character's details page.
        private void OnItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedItem = (HouseObject)e.ClickedItem;
            Frame.Navigate(typeof(HouseDetailsPage), selectedItem.url);
        }

        //Button click event that returns us to the main page.
        private void BackToMain_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        //Loads the next page from the API. (A page contains max 50 characters/houses.)
        private void LoadNextPage_Click(object sender, RoutedEventArgs e)
        {
            HouseViewModel.LoadNewDataAsync(HouseViewModel.Counter);
        }
    }
}
