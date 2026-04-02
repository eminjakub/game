using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Data.SQLite;
using System.Diagnostics;
using System.Threading;

namespace game
{
    public partial class Mainmenu : Page
    {
        public Player player { get; set; }
        public Task task { get; set; }
        public Arena arena { get; set; }
        public Database database { get; set; } 
        Player item;
        
        public Mainmenu(Player _player, Task _task)
        {
           InitializeComponent();
            task = _task;
           player = _player;

           string lv = player.xp.ToString();
           if (lv.Length >= 2)
           {
               char player_xp = lv[0];
               int lvl = (int)Char.GetNumericValue(player_xp);
               char lv_bar = lv[1];
               int lvl_bar = (int)Char.GetNumericValue(lv_bar);
               exp_bar_menu.Value = lvl_bar * 10;
               lv_status.Content = lvl;
           }
           else
           {
               lv_status.Content = "0";
               exp_bar_menu.Value = player.xp * 10;
           }
           
           label_money.Content = player.money.ToString();

           progress_bar_strenght.Value = player.strenght;
           progress_bar_armor.Value = player.armor;
           progress_bar_dexterity.Value = player.dexterity;
        }

        private void hrad_move(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("hrad.xaml", UriKind.Relative));
            NavigationService.Navigate(new Hrad(player, task));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("most.xaml", UriKind.Relative));
            NavigationService.Navigate(new Most(player, task));
        }

        private void obchod_move(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("Obchod.xaml", UriKind.Relative));
            NavigationService.Navigate(new Obchod(player, task));
        }

        private async void inventory_button_Click(object sender, RoutedEventArgs e)
        {
            inventoryOverlay.Visibility = Visibility.Visible;

            progress_bar_strenght.Value = player.strenght;
            progress_bar_armor.Value = player.armor;
            progress_bar_dexterity.Value = player.dexterity;

            var db = new Database(FileHelper.GetPath("players.db"));
            var items = await db.GetInventory(player.id);
            inventory_items_control.ItemsSource = items;
        }   

        private void home_exit(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Opravdu chcete ukončit hru? Obsah bude automaticky uložen.","Vypnutí hry", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                MessageBox.Show("Děkujeme za zahrání mé úžasné hry", "Hra byla uložena");
                this.database = new Database(FileHelper.GetPath("players.db"));
                database.SaveMoney(player);
                database.SaveArmor(player);
                database.SaveDexterity(player);
                database.SaveXP(player);
                database.SaveStrenght(player);
                database.SaveDungeonLv(player);
                App.Current.Shutdown();
            }
        }

        private void Arena_button(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("Arena.xaml", UriKind.Relative));
            NavigationService.Navigate(new Arena(player, task));
        }    

        private void save_button(object sender, RoutedEventArgs e)
        {
            this.database = new Database(FileHelper.GetPath("players.db"));
            database.SaveMoney(player);
            database.SaveArmor(player);
            database.SaveDexterity(player);
            database.SaveXP(player);
            database.SaveStrenght(player);
            database.SaveDungeonLv(player);
            MessageBox.Show("Hra byla uložena", "Ukládání");
        }

        private void close_bt(object sender, RoutedEventArgs e)
        {
            inventoryOverlay.Visibility = Visibility.Hidden;
            label_money.Content = player.money.ToString();
        }

        private async void Equip_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var invItem = button?.Tag as InventoryItem;
            if (invItem == null) return;

            if (invItem.Type == "Weapon")
            {
                player.strenght += invItem.Value;
                progress_bar_strenght.Value = player.strenght;
                label_money.Content = player.money.ToString();
            }
            else if (invItem.Type == "Armor")
            {
                player.armor += invItem.Value;
                progress_bar_armor.Value = player.armor;
            }
            else
            {
                return;
            }

            var db = new Database(FileHelper.GetPath("players.db"));
            await db.RemoveInventoryItem(invItem);
            await db.SaveStrenght(player);
            await db.SaveArmor(player);

            var items = await db.GetInventory(player.id);
            inventory_items_control.ItemsSource = items;
        }

        private async void UseItem_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var invItem = button?.Tag as InventoryItem;
            if (invItem == null) return;

            if (invItem.Type == "Consumable")
            {
                player.health += invItem.Value;
                MessageBox.Show($"Použil jsi {invItem.Name}! Obnoveno {invItem.Value} HP.", "Lektvar");
            }
            else
            {
                MessageBox.Show("Tento předmět nelze přímo použít. Zkus ho nasadit.", "Info");
                return;
            }

            var db = new Database(FileHelper.GetPath("players.db"));
            await db.RemoveInventoryItem(invItem);

            var items = await db.GetInventory(player.id);
            inventory_items_control.ItemsSource = items;
        }

        private void strenght_add(object sender, RoutedEventArgs e)
        {
            if (player.money >= 100)
            {
                if (player.strenght < 100)
                {
                    player.strenght += 2;
                    progress_bar_strenght.Value = player.strenght;
                    player.money -= 100;
                    label_money.Content = player.money;
                }
            }
            else
            {
                MessageBox.Show("Nemáte dostatek peněz!!");
            }
        }

        private void armor_add(object sender, RoutedEventArgs e)
        {
            if (player.money >= 100)
            {
                if (player.armor < 100)
                {
                    player.armor += 2;
                    progress_bar_armor.Value = player.armor;
                    player.money -= 100;
                    label_money.Content = player.money;
                }
            }
            else
            {
                MessageBox.Show("Nemáte dostatek peněz!!");
            }
        }

        private void dexterity_add(object sender, RoutedEventArgs e)
        {
            if (player.money >= 100)
            {
                if (player.dexterity < 100)
                {
                    player.dexterity += 2;
                    progress_bar_dexterity.Value = player.dexterity;
                    player.money -= 100;
                    label_money.Content = player.money;
                }
            }
            else
            {
                MessageBox.Show("Nemáte dostatek peněz!!");
            }
        }
    }
}
