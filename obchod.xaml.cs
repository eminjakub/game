using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace game
{
    public partial class Obchod : Page
    {
        public Player player { get; set; }
        public Task task { get; set; }

        public Obchod(Player _player, Task _task)
        {
            this.player = _player;
            this.task = _task;
            InitializeComponent();
        }

        private void button_in_shop(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("Obchod_inter.xaml", UriKind.Relative));
            NavigationService.Navigate(new Obchod_inter(player, task));
        }

        private void zpevak_action(object sender, RoutedEventArgs e)
        {
            zpevak_dialog_panel.Visibility = Visibility.Visible;
            zpevak_bt_action.Visibility = Visibility.Hidden;
            obchod_in_bt.Visibility = Visibility.Hidden;
        }

        private void zpevak_close(object sender, RoutedEventArgs e)
        {
            zpevak_dialog_panel.Visibility = Visibility.Hidden;
            zpevak_bt_action.Visibility = Visibility.Visible;
            obchod_in_bt.Visibility = Visibility.Visible;
        }

        private void Get_home(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("./task_file/task_page.xaml", UriKind.Relative));
            NavigationService.Navigate(new Mainmenu(player, task));
        }
    }
}
