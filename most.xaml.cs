using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace game
{
    public partial class Most : Page
    {
        Player player { get; set; }
        Task task { get; set; }

        public Most(Player _player, Task _task)
        {
            this.task = _task;
            this.player = _player;
            InitializeComponent();
            dealer_shop_items.ItemsSource = ShopItem.GetDealerItems();
        }

        private void dealeros_button_click_shop(object sender, RoutedEventArgs e)
        {
            dealeros_standing_btn.Visibility = Visibility.Hidden;
            dealeros_panel.Visibility = Visibility.Visible;
            dealer_money_label.Content = player.money.ToString();

            // Default view: dialog
            dialog_full_panel.Visibility = Visibility.Visible;
            shop_panel.Visibility = Visibility.Hidden;
        }

        private void dealeros_button_click_close(object sender, RoutedEventArgs e)
        {
            dealeros_standing_btn.Visibility = Visibility.Visible;
            dealeros_panel.Visibility = Visibility.Hidden;
        }

        private void Tab_Dialog_Click(object sender, RoutedEventArgs e)
        {
            dialog_full_panel.Visibility = Visibility.Visible;
            shop_panel.Visibility = Visibility.Hidden;
        }

        private void Tab_Shop_Click(object sender, RoutedEventArgs e)
        {
            dialog_full_panel.Visibility = Visibility.Hidden;
            shop_panel.Visibility = Visibility.Visible;
            dealer_money_label.Content = player.money.ToString();
        }

        private async void DealerBuy_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var item = button?.Tag as ShopItem;
            if (item == null) return;

            if (player.money >= item.Price)
            {
                player.money -= item.Price;
                dealer_money_label.Content = player.money.ToString();

                var db = new Database(FileHelper.GetPath("players.db"));
                await db.SaveMoney(player);

                var newItem = new InventoryItem
                {
                    PlayerId = player.id,
                    Name = item.Name,
                    Type = item.Type,
                    Value = item.StatBonus,
                    ImagePath = item.ImagePath,
                    Description = item.Description,
                    IsEquipped = false,
                    StatType = "hp"
                };
                await db.AddInventoryItem(newItem);

                dealeros_dialog_label.Text = $"Dobrej výběr! {item.Name} — a pamatuj, ode mě nic neslyšels. Přijď znovu!";
            }
            else
            {
                dealeros_dialog_label.Text = "Nemáš dost peněz, kamaráde. Jdi si vydělat a vrať se. Já tu budu.";
            }
        }

        private void Get_home(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("./task_file/task_page.xaml", UriKind.Relative));
            NavigationService.Navigate(new Mainmenu(player, task));
        }
    }
}
