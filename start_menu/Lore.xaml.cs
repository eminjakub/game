using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace game
{
    /// <summary>
    /// Interakční logika pro lore.xaml
    /// </summary>
    public partial class Lore : Page
    {
        MediaPlayer mediaPlayer = new MediaPlayer();
        public void clicking()
        {
            var path = new Uri("C:/Users/emingja19/source/repos/game/fotky/click.mp3");
            mediaPlayer.Open(path);
            mediaPlayer.Volume = 100;
            mediaPlayer.Play();
        }
        public Lore()
        {
            
            InitializeComponent();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            clicking();
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("/start_menu/menu.xaml", UriKind.Relative));

        }
    }
}
