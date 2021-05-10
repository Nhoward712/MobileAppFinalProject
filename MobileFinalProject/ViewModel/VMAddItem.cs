using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using MobileFinalProject.Model;
using MobileFinalProject.Data;

namespace MobileFinalProject.ViewModel
{
    public class VMAddItem : INotifyPropertyChanged
    {
        private Item _items { get; set; }

        public Item product
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

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

        private string _btnSaveLabel { get; set; }
        public string btnSaveLabel
        {
            get { return _btnSaveLabel; }
            set
            {
                _btnSaveLabel = value;
                OnPropertyChanged();
            }
        }

        public Command btnSaveProduct { get; set; }
        public Command btnClearProduct { get; set; }
        public VMAddItem(Item obj)
        {
            if (obj == null || obj.ID == 0)
                ClearProduct();

            else
            {
                product = obj;
                btnSaveLabel = "UPDATE";
            }
            btnSaveProduct = new Command(SaveProduct);
            btnClearProduct = new Command(ClearProduct);
        }

        public void SaveProduct()
        {
            try
            {
                ItemDatabase itemDatabase = new ItemDatabase();
                int i = itemDatabase.SaveItemAsync(product).Result;

                if (i == 1)
                {

                    if (btnSaveLabel.Equals("ADD"))
                    {
                        ClearProduct();
                        lblInfo = "Your Product saved successfully.";
                    }
                    else
                    {
                        ClearProduct();
                        lblInfo = "Your Product updated successfully.";
                    }
                }
                else
                    lblInfo = "Cannot save Product Information";
            }

            catch (Exception ex)
            {
                lblInfo = ex.Message.ToString();
            }
        }

        public void ClearProduct()
        {
            product = new Item();
            product.ID = 0;
            product.ItemName = "";
            product.IsNeeded = false;
            product.IsChecked = false;
            lblInfo = "";
            btnSaveLabel = "ADD";

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