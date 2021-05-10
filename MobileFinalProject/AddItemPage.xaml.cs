using System;
using System.Collections.Generic;
using MobileFinalProject.Model;
using MobileFinalProject.ViewModel;
using Xamarin.Forms;

namespace MobileFinalProject
{
    public partial class AddItemPage : ContentPage
    {
        public AddItemPage(Item item)
        {
            InitializeComponent();
            VMAddItem vm = new VMAddItem(item);
            this.BindingContext = vm;
        }
        
    }
}
