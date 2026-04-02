using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace game
{
    public partial class Hrad : Page
    {
        public Player player { get; set; }
        public Task task { get; set; }
        public Database database { get; set; }
        private int currentBet = 50;
        private Random rng = new Random();

        private static readonly string[] BuresWinQuotes =
        {
            "Vidíte? Já jsem řikal, že jsem nevinný! Kostky to potvrdily!",
            "Podívejte, já jsem vyhrál fer a čtvér. Bez dotací!",
            "ANO! Já to věděl. Já jsem prostý člověk, ale šikovný.",
            "To je jednoduché — stačí hlasovat pro mě a vyhrajete taky.",
            "Moje kostky jsou čisté, na rozdíl od těch ostatních politiků!",
        };

        private static readonly string[] BuresLoseQuotes =
        {
            "Tohle je mediální útok! Někdo mi sabotoval kostky!",
            "Já nevím proč jsem prohrál, to je nesmysl. Já jsem to neudělal!",
            "Opozice mi přála špatné číslo. To je spiknutí!",
            "Podívejte, prohrál jsem, ale jen proto že mám špatné PR. Jinak bych vyhrál.",
            "Budu to řešit přes svého právníka. A přes média. A přes EU.",
        };

        private static readonly string[] BuresGreetings =
        {
            "Podívejte, já nevím proč mi tady říkáte Bureš. Já jsem normální člověk, prostý zemědělec. Já jsem to neudělal!",
            "Já jsem přišel na hrad jen tak. Náhodou. Žádné lobbování. Vůbec.",
            "Hrajeme kostky? Dobře, ale říkám vám — já jsem čistý. Jak křišťál.",
        };

        public Hrad(Player _player, Task _task)
        {
            this.task = _task;
            this.player = _player;
            InitializeComponent();
            milos_greeting.Visibility = Visibility.Visible;
            bures_dialog.Text = BuresGreetings[rng.Next(BuresGreetings.Length)];
        }

        int entry = 0;

        private void milos_greeting_close(object sender, RoutedEventArgs e)
        {
            milos_greeting.Visibility = Visibility.Hidden;
        }

        // ===== Miloš quest =====
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (milos_greeting.Visibility == Visibility.Visible)
                milos_greeting.Visibility = Visibility.Hidden;

            if (entry == 0)
            {
                this.database = new Database(FileHelper.GetPath("tasks.db"));
                Random random = new Random();
                task.task_id = random.Next(0, 12);

                task.Task_name = database.Taskddd(task.task_id)[0].task_name;
                task.task_description = database.Taskddd(task.task_id)[0].task_description;
                task.Task_lenght = database.Taskddd(task.task_id)[0].task_time;
                task.Task_money = database.Taskddd(task.task_id)[0].money;
                task.Task_xp = database.Taskddd(task.task_id)[0].xp;
                entry = +1;
            }

            task_name_label.Content = task.Task_name;
            task_description_label.Text = task.task_description;
            task_lenght_label.Content = $"Délka: {Int32.Parse(task.Task_lenght)*10} sekund";
            task_money_label.Content = $"Peníze: {task.Task_money}";

            task_name_label.Visibility = Visibility.Visible;
            task_description_label.Visibility = Visibility.Visible;
            task_lenght_label.Visibility = Visibility.Visible;
            quest_box.Visibility = Visibility.Visible;
            quest_milos.Visibility = Visibility.Visible;
            frame.Visibility = Visibility.Visible;
            close_button.Visibility = Visibility.Visible;
            close_button_img.Visibility = Visibility.Visible;
            task_1.Visibility = Visibility.Visible;
            task_money_label.Visibility = Visibility.Visible;
            milos_button.Visibility = Visibility.Hidden;
            milos_button_img.Visibility = Visibility.Hidden;
        }

        private void milos_quest_close(object sender, RoutedEventArgs e)
        {
            quest_box.Visibility = Visibility.Hidden;
            quest_milos.Visibility = Visibility.Hidden;
            frame.Visibility = Visibility.Hidden;
            close_button.Visibility = Visibility.Hidden;
            close_button_img.Visibility = Visibility.Hidden;
            task_1.Visibility = Visibility.Hidden;
            task_money_label.Visibility = Visibility.Hidden;
            milos_button.Visibility = Visibility.Visible;
            milos_button_img.Visibility = Visibility.Visible;
            task_name_label.Visibility = Visibility.Hidden;
            task_description_label.Visibility = Visibility.Hidden;
            task_lenght_label.Visibility = Visibility.Hidden;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("./task_file/task_page.xaml", UriKind.Relative));
            NavigationService.Navigate(new Task_page(player, task));
        }

        // ===== Bureš kostky =====
        private void Bures_Click(object sender, RoutedEventArgs e)
        {
            bures_money_label.Text = player.money.ToString();
            player_dice.Text = "?";
            bures_dice.Text = "?";
            player_dice_label.Text = "";
            bures_dice_label.Text = "";
            dice_result_text.Text = "Hoď kostkou a uvidíš, jestli jsi lepší než Bureš!";
            bures_dialog.Text = BuresGreetings[rng.Next(BuresGreetings.Length)];
            UpdateBetUI();
            bures_panel.Visibility = Visibility.Visible;
        }

        private void Bures_Close(object sender, RoutedEventArgs e)
        {
            bures_panel.Visibility = Visibility.Hidden;
        }

        private void Bet_50(object sender, RoutedEventArgs e) { currentBet = 50; UpdateBetUI(); }
        private void Bet_100(object sender, RoutedEventArgs e) { currentBet = 100; UpdateBetUI(); }
        private void Bet_200(object sender, RoutedEventArgs e) { currentBet = 200; UpdateBetUI(); }

        private void UpdateBetUI()
        {
            bet_label.Text = currentBet.ToString() + " Gold";
        }

        private void Roll_Dice_Click(object sender, RoutedEventArgs e)
        {
            if (player.money < currentBet)
            {
                bures_dialog.Text = "Podívejte, vy nemáte ani na sázku. Já vím jak to je být chudý, ale tohle je ostuda.";
                dice_result_text.Text = "Nemáš dost peněz na tuto sázku!";
                dice_result_text.Foreground = System.Windows.Media.Brushes.OrangeRed;
                return;
            }

            int playerRoll = rng.Next(1, 7);
            int buresRoll = rng.Next(1, 7);

            player_dice.Text = DiceEmoji(playerRoll);
            bures_dice.Text = DiceEmoji(buresRoll);
            player_dice_label.Text = $"hodil jsi {playerRoll}";
            bures_dice_label.Text = $"Bureš hodil {buresRoll}";

            if (playerRoll > buresRoll)
            {
                // Hráč vyhrál
                player.money += currentBet;
                bures_money_label.Text = player.money.ToString();
                dice_result_text.Text = $"Vyhrál jsi! +{currentBet} Gold!";
                dice_result_text.Foreground = System.Windows.Media.Brushes.LimeGreen;
                bures_dialog.Text = BuresLoseQuotes[rng.Next(BuresLoseQuotes.Length)];
            }
            else if (buresRoll > playerRoll)
            {
                // Bureš vyhrál
                player.money -= currentBet;
                bures_money_label.Text = player.money.ToString();
                dice_result_text.Text = $"Bureš vyhrál! -{currentBet} Gold!";
                dice_result_text.Foreground = System.Windows.Media.Brushes.OrangeRed;
                bures_dialog.Text = BuresWinQuotes[rng.Next(BuresWinQuotes.Length)];
            }
            else
            {
                // Remíza
                dice_result_text.Text = "Remíza! Bureš chce hrát znovu...";
                dice_result_text.Foreground = System.Windows.Media.Brushes.Yellow;
                bures_dialog.Text = "Podívejte, remíza. Jako v koaliční vládě. Nikdo nevyhrál, nikdo neprohrál.";
            }
        }

        private string DiceEmoji(int n)
        {
            return n switch { 1 => "⚀", 2 => "⚁", 3 => "⚂", 4 => "⚃", 5 => "⚄", 6 => "⚅", _ => "?" };
        }

        private void get_home(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("mainmenu.xaml", UriKind.Relative));
            NavigationService.Navigate(new Mainmenu(player, task));
        }
    }
}
