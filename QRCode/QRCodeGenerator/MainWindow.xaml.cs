using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QRCoder;

namespace QRGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnConvert_Click(object sender, RoutedEventArgs e)
        {
            QRCodeGenerator qrgenerator = new QRCodeGenerator();
            QRCodeData QRCodeData = qrgenerator.CreateQrCode(txtTextToConvert.Text, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(QRCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            BitmapImage bmpImage = new BitmapImage();
            bmpImage = Convert(qrCodeImage);

            QRImage.Source = bmpImage;

        }

        public BitmapImage Convert(Bitmap bmp)
        {
            using (var memory = new MemoryStream())
            {
                bmp.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }
    }
}
