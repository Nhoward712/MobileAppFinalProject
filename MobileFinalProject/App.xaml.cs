using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileFinalProject.Data;
using MobileFinalProject.Model;
using System.Collections.Generic;

namespace MobileFinalProject
{
    public partial class App : Application
    {
        static ItemDatabase database;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }
        public static ItemDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new ItemDatabase();
                    prefillDatabase();

                }
                return database;
            }
        }

        private static void prefillDatabase()
        {
            //1=Produce
            //2=Meats
            //3=Bakery
            //4=Staples
            //5=Dairy
            //6=Frozen
            //10=unused
            database.ClearAllAsync();
            List<Item> items = new List<Item>();
            items.Add(new Item() { ItemName = "Banana", Category = 1, IsNeeded = false, SortOrder = 1, IsChecked = false });
            items.Add(new Item() { ItemName = "Milk", Category = 5, IsNeeded = false, SortOrder = 5, IsChecked = false });
            items.Add(new Item() { ItemName = "Ground Beef", Category = 2, IsNeeded = true, SortOrder = 2, IsChecked = false });
            items.Add(new Item() { ItemName = "Bread", Category = 3, IsNeeded = true, SortOrder = 3, IsChecked = false });
            items.Add(new Item() { ItemName = "Flour", Category = 3, IsNeeded = true, SortOrder = 3, IsChecked = false });
            items.Add(new Item() { ItemName = "Chicken", Category = 3, IsNeeded = true, SortOrder = 3, IsChecked = false });
            items.Add(new Item() { ItemName = "Apples", Category = 3, IsNeeded = true, SortOrder = 3, IsChecked = false });
            items.Add(new Item() { ItemName = "Tomatoes", Category = 3, IsNeeded = true, SortOrder = 3, IsChecked = false });
            items.Add(new Item() { ItemName = "Ham", Category = 3, IsNeeded = true, SortOrder = 3, IsChecked = false });
            items.Add(new Item() { ItemName = "Yogurt", Category = 3, IsNeeded = true, SortOrder = 3, IsChecked = false });
            items.Add(new Item() { ItemName = "Butter", Category = 3, IsNeeded = true, SortOrder = 3, IsChecked = false });
            items.Add(new Item() { ItemName = "Cereal", Category = 3, IsNeeded = true, SortOrder = 3, IsChecked = false });






            database.InsertList(items);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
