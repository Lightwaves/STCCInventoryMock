using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using ZXing.Mobile;
//using Windows.UI.Xaml;
using Xamarin.Forms.Xaml;
//using Windows.UI.Xaml;
using System.Collections.ObjectModel;
using STCCInventoryMock.Models;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace STCCInventoryMock
{
    

    public partial class MainPage : ContentPage
    {
        ZXing.Net.Mobile.Forms.ZXingScannerView zxing;
        ZXingDefaultOverlay overlay;
        ObservableCollection<InventoryEntity> inventoryentries = new ObservableCollection<InventoryEntity>();

        public MainPage() : base()
        {
            InitializeComponent();


            var inventoryEntityDataTemplate = new Xamarin.Forms.DataTemplate(() =>
            {
                var grid = new Grid();
            
            var nameLabel = new Label { FontAttributes = FontAttributes.Bold };
                var serialNumberLabel = new Label();
                var typeLabel = new Label { HorizontalTextAlignment = Xamarin.Forms.TextAlignment.End };

                nameLabel.SetBinding(Label.TextProperty, "Name");
                serialNumberLabel.SetBinding(Label.TextProperty, "SerialNumber");
                typeLabel.SetBinding(Label.TextProperty, "Type");

                grid.Children.Add(nameLabel);
                grid.Children.Add(serialNumberLabel, 1, 0);
                grid.Children.Add(typeLabel, 2, 0);

                return new ViewCell { View = grid };
            });


            /*
            var scanPage = new ZXingScannerPage();

            var listView = new ListView();
            listView.ItemsSource = new string[]{
                                    "mono",
                                    "monodroid",
                                    "monotouch",
                                    "monorail",
                                    "monodevelop",
                                    "monotone",
                                    "monopoly",
                                    "monomodal",
                                    "mononucleosis"
                                    };


            InventoryGrid.Children.Add(scanPage.Overlay, 0, 0);
            InventoryGrid.Children.Add(listView, 1, 0);
            Grid.SetColumnSpan(listView, 2);
            

         
            Navigation.PushModalAsync(scanPage);
            scanPage.OnScanResult += (result) =>
            {
                scanPage.IsScanning = true;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await DisplayAlert("Scanned Barcode", result.Text, "OK");
                });

                
            };
            */

            zxing = new ZXing.Net.Mobile.Forms.ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AutomationId = "zxingScannerView",
                
            };
            var options = new ZXing.Mobile.MobileBarcodeScanningOptions();
            options.DelayBetweenContinuousScans = 3000;
            zxing.Options = options;
            zxing.OnScanResult += (result) =>
                Device.BeginInvokeOnMainThread(async () => {

                    // Stop analysis until we navigate away so we don't keep reading barcodes
                    //zxing.IsAnalyzing = false;

                    // Show an alert
                    inventoryentries.Add(new InventoryEntity { Name="Test", SerialNumber=result.Text, Type="Desktop" });
                    //await DisplayAlert("Scanned Barcode", result.Text, "OK");
                    

                    // Navigate away
                   // await Navigation.PopAsync();
                });

            overlay = new ZXingDefaultOverlay
            {
                TopText = "Hold your phone up to the barcode",
                BottomText = "Scanning will happen automatically",
                ShowFlashButton = zxing.HasTorch,
                AutomationId = "zxingDefaultOverlay",
            };
            overlay.FlashButtonClicked += (sender, e) => {
                zxing.IsTorchOn = !zxing.IsTorchOn;
            };


            /*            var grid = new Grid
                        {
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                        };
                        grid.Children.Add(zxing);
                        grid.Children.Add(overlay);
                        */
            
            InventoryGrid.Children.Add(zxing);
            InventoryGrid.Children.Add(overlay);


            var listView = new ListView
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                ItemsSource = inventoryentries,
                ItemTemplate = inventoryEntityDataTemplate,
                Margin = new Xamarin.Forms.Thickness(0, 20, 0, 0)
                
                
                
            };


            var stackLayout = new StackLayout
            {
                Margin = new Xamarin.Forms.Thickness(20),
                Children = {
                    listView
                }

            };

            InventoryGrid.Children.Add(stackLayout, 1, 0);


            // The root page of your application
            //Content = grid;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            zxing.IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            zxing.IsScanning = false;

            base.OnDisappearing();
        }






    }
}
