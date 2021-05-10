using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileFinalProject.Data;
using MobileFinalProject.Model;
using Xamarin.Forms;

namespace MobileFinalProject
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            listView.ItemsSource = await App.Database.GetItemsAsync();
            
        }

        public async void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            listView.BeginRefresh();
            int i = e.SelectedItemIndex;
            //needs to get specific item, change its attribute, save it back
            var item = await App.Database.GetItemsAsync();
            if(item[i].IsChecked == true)
            {
                item[i].IsChecked = false;
            }
            else
            {
                item[i].IsChecked = true;
            }
            //item[i].IsChecked = false ? true : false;
            await App.Database.SaveItemAsync(item[i]);
            listView.ItemsSource = await App.Database.GetItemsAsync();
            listView.EndRefresh();
            
        }

        async void btnAddItem(System.Object sender, System.EventArgs e)
        {

            await Navigation.PushAsync(new AddItemPage(null));
            //var item = new Item{ ItemName = "Pasta", Category = 3, IsNeeded = true, SortOrder = 4, IsChecked = true };
            //listView.BeginRefresh();
            //await App.Database.SaveItemAsync(item);
            //listView.ItemsSource = await App.Database.GetItemsAsync();
            //listView.EndRefresh();


        }

        async void btnViewPrevPurch_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new PreviouslyPurchased());
        }

        async void btnClrList_Clicked(System.Object sender, System.EventArgs e)
        {
            //need to add popup to ask if they are sure to delete list
            var clr = await DisplayAlert("Confirm", "Do you want to clear your shopping list?", "Yes", "No");
            if (clr)
            {
                listView.BeginRefresh();
                var items = await App.Database.GetItemsAsync();
                foreach (var item in items)
                {
                    item.IsNeeded = false;

                }
                await App.Database.UpdateList(items);
                listView.ItemsSource = await App.Database.GetItemsAsync();

                listView.EndRefresh();
            }


        }
    }
}
