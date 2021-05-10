using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using MobileFinalProject.Data;
using MobileFinalProject.Model;

namespace XamSQLite.ViewModels
{
    public class VMProducts : INotifyPropertyChanged
    {
        private ObservableCollection<Item> _lstProducts { get; set; }

        public ObservableCollection<Item> lstProducts
        {
            get { return _lstProducts; }
            set
            {
                _lstProducts = value;
                OnPropertyChanged();
            }
        }

        public Command btnAddProduct { get; set; }

        private string _lblInfo { get; set; }
        public string lblInfo
        {
            get { return _lblInfo; }
            set
            {
                _lblInfo = value;
                OnPropertyChanged();
            }
        }

        public VMProducts()
        {
            lstProducts = new ObservableCollection<Item>();

        }

        public void GetProducts()
        {
            try
            {
                ItemDatabase itemDatabase = new ItemDatabase();
                var items = itemDatabase.GetAllItemsAsync().Result;

                if (items != null && items.Count > 0)
                {
                    lstProducts = new ObservableCollection<Item>();

                    foreach (var item in items)
                    {
                        lstProducts.Add(new Item
                        {
                            ID = item.ID,
                            ItemName = item.ItemName,

                        });
                    }

                    lblInfo = "Total " + items.Count.ToString() + " record(s) found";
                }
                else
                    lblInfo = "No product records found. Please add one";
            }

            catch (Exception ex)
            {
                lblInfo = ex.Message.ToString();
            }
        }

        public void DeleteProduct(Item item)
        {
            try
            {
                ItemDatabase itemDatabase = new ItemDatabase();
                var result = itemDatabase.DeleteItemAsync(item).Result;

                if (result == 1)
                    GetProducts();
                else
                    lblInfo = "Cannot Delete this Product.";
            }

            catch (Exception ex)
            {
                lblInfo = ex.Message.ToString();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}