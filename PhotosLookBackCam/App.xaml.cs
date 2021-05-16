using System;
using System.Windows;

namespace PhotosLookBackCam
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length == 1) {
                (new MainWindow(e.Args[0])).Show();
            }
            else {
                MessageBox.Show("Il faut lancer le logiciel avec un dossier en paramètre !", "Erreur de lancement", MessageBoxButton.OK, MessageBoxImage.Warning);
                Environment.Exit(1);
            }
        }
    }
}
