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
    public partial class Obchod_inter : Page
    {
        public Player player { get; set; }
        public Task task { get; set; }

        public Obchod_inter(Player _player, Task _task)
        {
            this.player = _player;
            this.task = _task;
            InitializeComponent();
            UpdateMoney();
            shopItemsControl.ItemsSource = ShopItem.GetShopItems();
        }

        private void UpdateMoney()
        {
            if (money_label != null)
                money_label.Content = player.money.ToString();
        }

        private async void Buy_Item_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var item = button?.Tag as ShopItem;
            if (item == null) return;

            if (player.money >= item.Price)
            {
                player.money -= item.Price;
                UpdateMoney();

                var db = new Database(FileHelper.GetPath("players.db"));
                await db.SaveMoney(player);

                string statType = "hp";
                if (item.Type == "Weapon") statType = "strenght";
                else if (item.Type == "Armor") statType = "armor";

                var newItem = new InventoryItem()
                {
                    PlayerId = player.id,
                    Name = item.Name,
                    Type = item.Type,
                    Value = item.StatBonus,
                    ImagePath = item.ImagePath,
                    Description = item.Description,
                    IsEquipped = false,
                    StatType = statType
                };
                await db.AddInventoryItem(newItem);

                alenka_dialog.Text = $"Výborná volba! {item.Name} se ti bude hodit. Ještě něco?";
            }
            else
            {
                alenka_dialog.Text = "Na to nemáš dost peněz! Běž si nejdřív něco vydělat u Miloše na hradě.";
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
