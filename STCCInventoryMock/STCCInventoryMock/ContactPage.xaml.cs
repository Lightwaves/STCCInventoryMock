using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace STCCInventoryMock
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactPage : ContentPage
	{
		public ContactPage ()
		{
			InitializeComponent ();
            listView.ItemsSource = new List<Models.MenuItem>
            {
                new Models.MenuItem {Text = "Inventory Mode" }

                
            };
           
		}



        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            var menuItem = e.SelectedItem as Label;
            //menuItem.Text
            listView.SelectedItem = null;
        }

    }
}