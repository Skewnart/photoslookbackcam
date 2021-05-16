using PhotosLookBackCam.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace PhotosLookBackCam
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private DirectoryInfo SourcePath { get; set; } = null;
        public List<string> PhotosPath { get; set; } = new List<string>();
        private bool canDisplay = true;

        private BitmapImage imageSource = null;
        public BitmapImage ImageSource {
            get { return imageSource; }
            set { imageSource = value;
                this.OnPropertyChanged("ImageSource");
            }
        }

        public MainWindow(string path)
        {
            this.SourcePath = new DirectoryInfo(path);

            foreach (string file in Directory.GetFiles(this.SourcePath.FullName).ToList()) {
                this.PhotosPath.Add(file);
            }

            InitializeComponent();

            DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (canDisplay) {
                this.canDisplay = false;

                foreach(string file in Directory.GetFiles(this.SourcePath.FullName).ToList().Where(x => x.ToLower().EndsWith($".jpg") && !this.PhotosPath.Any(y => x.Equals(y)))){
                    this.ChangePhoto(file);
                    this.PhotosPath.Add(file);
                }

                this.canDisplay = true;
            }
        }

        public void ChangePhoto(string path)
        {
            var image = Image.FromFile(path);
            image.NormalizeOrientation();
            var bitmap = new BitmapImage();

            using (MemoryStream stream = new MemoryStream()) {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                stream.Seek(0, SeekOrigin.Begin);

                bitmap.BeginInit();
                bitmap.StreamSource = stream;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
            }
            this.ImageSource = bitmap;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (MessageBox.Show("Veux-tu vraiment fermer l'application ?", "Demande de confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) {
                e.Cancel = true;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D) {
                Process.Start("explorer.exe", this.SourcePath.FullName);
            }
            else if (e.Key == Key.Enter || e.Key == Key.F) {
                if (this.WindowState == WindowState.Normal) {
                    this.WindowState = WindowState.Maximized;
                    this.WindowStyle = WindowStyle.None;
                }
                else {
                    this.WindowState = WindowState.Normal;
                    this.WindowStyle = WindowStyle.SingleBorderWindow;
                }
            }
            else if (e.Key == Key.Escape) {
                this.Close();
            }
        }
    }
}
