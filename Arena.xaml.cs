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
using System.Diagnostics;

namespace game
{
    public partial class Arena : Page
    {
        Player player { get; set; }
        Task task { get; set; }
        Fight fight { get; set; }
        List<InventoryItem> potions;

        public Arena(Player _player, Task _task)
        {
            this.task = _task;
            this.player = _player;
            InitializeComponent();

            var enemies = Enemy.GetEnemies(player.dungeon_lv);
            enemyListControl.ItemsSource = enemies;

            LoadPotions();
        }

        private async void LoadPotions()
        {
            var db = new Database(FileHelper.GetPath("players.db"));
            var items = await db.GetInventory(player.id);
            potions = items.Where(i => i.Type == "Consumable").ToList();
            UpdatePotionButton();
        }

        private void UpdatePotionButton()
        {
            int count = potions?.Count ?? 0;
            potion_button.Content = $"Lektvar ({count})";
            potion_button.IsEnabled = count > 0 && fight != null && !fight.isFinished;
        }

        private void SelectEnemy_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var enemy = button?.Tag as Enemy;
            if (enemy == null) return;

            fight = new Fight(player, enemy);

            enemySelectPanel.Visibility = Visibility.Hidden;
            fightPanel.Visibility = Visibility.Visible;

            enemy_name_label.Text = enemy.Name;
            try { enemy_fight_img.Source = new BitmapImage(new Uri(enemy.ImagePath, UriKind.Relative)); } catch { }

            UpdateHP();
            UpdateLog();
            UpdatePotionButton();
        }

        private void Attack_Click(object sender, RoutedEventArgs e)
        {
            if (fight == null || fight.isFinished) return;

            fight.PlayerAttack();

            if (!fight.isFinished)
            {
                fight.EnemyAttack();
            }

            UpdateHP();
            UpdateLog();
            CheckFinished();
        }

        private async void UsePotion_Click(object sender, RoutedEventArgs e)
        {
            if (fight == null || fight.isFinished) return;
            if (potions == null || potions.Count == 0) return;

            var potion = potions[0];
            fight.UsePotion(potion.Value);

            var db = new Database(FileHelper.GetPath("players.db"));
            await db.RemoveInventoryItem(potion);
            potions.Remove(potion);

            if (!fight.isFinished)
            {
                fight.EnemyAttack();
            }

            UpdateHP();
            UpdateLog();
            UpdatePotionButton();
            CheckFinished();
        }

        private void UpdateHP()
        {
            if (fight == null) return;

            player_hp_bar.Maximum = fight.playerMaxHP;
            player_hp_bar.Value = fight.playerHP;
            player_hp_text.Text = $"{fight.playerHP}/{fight.playerMaxHP} HP";

            enemy_hp_bar.Maximum = fight.enemy.max_health;
            enemy_hp_bar.Value = fight.enemy.health;
            enemy_hp_text.Text = $"{fight.enemy.health}/{fight.enemy.max_health} HP";
        }

        private void UpdateLog()
        {
            if (fight == null) return;
            battle_log_text.Text = string.Join("\n", fight.battleLog);
            logScroll.ScrollToEnd();
        }

        private void CheckFinished()
        {
            if (fight == null || !fight.isFinished) return;

            attack_button.IsEnabled = false;
            potion_button.IsEnabled = false;
            resultPanel.Visibility = Visibility.Visible;

            if (fight.result == 1)
            {
                result_text.Text = $"Vítězství!\n+{fight.enemy.reward_money} Gold\n+{fight.enemy.reward_xp} XP";
            }
            else
            {
                result_text.Text = $"{fight.enemy.Name} tě porazil!\nPříště to zkus znovu.";
            }
        }

        private void BackToSelect_Click(object sender, RoutedEventArgs e)
        {
            fightPanel.Visibility = Visibility.Hidden;
            resultPanel.Visibility = Visibility.Hidden;
            enemySelectPanel.Visibility = Visibility.Visible;
            attack_button.IsEnabled = true;
            fight = null;

            var enemies = Enemy.GetEnemies(player.dungeon_lv);
            enemyListControl.ItemsSource = enemies;
            LoadPotions();
        }

        private void Get_home(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("mainmenu.xaml", UriKind.Relative));
            NavigationService.Navigate(new Mainmenu(player, task));
        }
    }
}
