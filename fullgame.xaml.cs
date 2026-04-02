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
using System.Data.SQLite;

namespace game
{
    /// <summary>
    /// Interakční logika pro fullgame.xaml
    /// </summary>
    public partial class Fullgame : Window
    {
        public int money  { get; set; } 
        public Player player { get; set; } 
        
        public Task task { get; set; }
        Player item;
        //public int dungeon_lv { get; set; } = 1;
        public Database database { get; set; }

        public Fullgame()
        {
            this.database = new Database(FileHelper.GetPath("players.db"));
            //dungeon_lv = 1;
            this.player = new Player();
            this.task = new Task();

            //List<int> money = database.Syncmoney().Result;
            //List<int> xp = database.Syncxp().Result;
            
            //string cs = FileHelper.GetPath("players.db");
            //var con = new SQLiteConnection(FileHelper.GetPath("players.db"));



            /* string cs = @"URI=file:C:\Users\Kubunacz\Source\Repos\Kubunacz\politika\players.db";
             using var con = new SQLiteConnection(cs);*/
            List<Player> sync = database.SyncALL().Result;
            this.player = sync[0];

            //MessageBox.Show(player.money.ToString(), "money");
            //MessageBox.Show(player.xp.ToString(), "xp");
            //MessageBox.Show(player.dexterity.ToString(), "dexterity");
            //con.Open();

            /*string sync_xp = $"SELECT xp FROM player where id = 1";
            using var cmd_xp = new SQLiteCommand(sync_xp, con);
            string xp = cmd_xp.ExecuteScalar().ToString();
            int pl_xp = Int32.Parse(xp);
            this.player.exp = pl_xp;

            string sync_money = $"SELECT money FROM player where id = 1";
            using var cmd_money = new SQLiteCommand(sync_money, con);
            string mn = cmd_money.ExecuteScalar().ToString();
            int pl_mn = Int32.Parse(mn);
            this.player.money = pl_mn;

            string sync_strenght = $"SELECT strenght FROM player where id = 1";
            using var cmd_strenght = new SQLiteCommand(sync_strenght, con);
            string st = cmd_strenght.ExecuteScalar().ToString();
            int pl_st = Int32.Parse(st);
            this.player.strenght = pl_st;

            string sync_armor = $"SELECT armor FROM player where id = 1";
            using var cmd_armor = new SQLiteCommand(sync_armor, con);
            string ar = cmd_armor.ExecuteScalar().ToString();
            int pl_ar = Int32.Parse(ar);
            this.player.armor = pl_ar;

            string sync_dexterity = $"SELECT dexterity FROM player where id = 1";
            using var cmd_dexterity = new SQLiteCommand(sync_dexterity, con);
            string dx = cmd_dexterity.ExecuteScalar().ToString();
            int pl_dx = Int32.Parse(dx);
            this.player.dexterity = pl_dx;

            con.Close();*/


            InitializeComponent();
            mainFrame.Content = new Mainmenu(this.player, this.task);
            this.WindowStyle = WindowStyle.None;
            AllowsTransparency = true;

            
            //Home_but = home_but;
            //Textblock1 = text1;
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Exit_game(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /*private void get_home(object sender, RoutedEventArgs e)
        {
            Mainmenu mainMenuPage = new Mainmenu(this.player);
            mainFrame.Content = mainMenuPage;
            mainFrame.NavigationService.Navigate(new Mainmenu(player));

            /*
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("mainmenu.xaml", UriKind.Relative));
            
        }*/

        
    }
}
