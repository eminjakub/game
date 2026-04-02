using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        MediaPlayer mediaPlayer = new MediaPlayer();
        public void clicking()
        {
            var path = new Uri("C:/Users/emingja19/source/repos/game/fotky/click.mp3");
            mediaPlayer.Open(path);
            mediaPlayer.Volume = 100;
            mediaPlayer.Play();
        }
        public MainWindow()
        {
            
            InitializeComponent();
            this.WindowStyle = WindowStyle.None;
            AllowsTransparency = true;
            bg();

        }
      
        private void bg()
        {
            var path = new Uri("C:/Users/emingja19/source/repos/game/fotky/bg.mp3");
            mediaPlayer.Open(path);
            mediaPlayer.Play();
        }
        
        public void close()
        {
            this.Close();
        }



    }
}
