using ZXing.Net.Maui.Controls;

namespace MauiAppProvaQR
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            cameraBarcodeReaderView.Options = new ZXing.Net.Maui.BarcodeReaderOptions
            {
                Formats = ZXing.Net.Maui.BarcodeFormat.QrCode, // Set to recognize QR codes
                AutoRotate = true, // Automatically rotate the image to detect barcodes from different angles
                TryHarder = true,
                //Multiple = true // Allow the detection of multiple barcodes at once
            };
        }

		bool alreadyDisplayed = false;
		protected void BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
        {
            var first = e.Results?.FirstOrDefault();
            if (first is null)
            {
                return;
            }

            Dispatcher.DispatchAsync(async () =>
            {
                if (!alreadyDisplayed)
				{
					alreadyDisplayed = true; // visualizza popup una sola volta
                    bool userResponse = await DisplayAlert("QR code scanned", $"Scanned value: {first.Value}", "Ok", "Cancel");

					if (userResponse || !userResponse) // soluzione molto poco elegante per resettare il booleano alla conferma dell'utente
                        alreadyDisplayed = false;
				}
            });
        }
    }
}