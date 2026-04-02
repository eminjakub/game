using System;
using System.Threading;
using System.Collections.Generic;
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
using System.Windows.Threading;
using System.ComponentModel;

namespace game
{
    /// <summary>
    /// Interakční logika pro task_page.xaml
    /// </summary>
    public partial class Task_page : Page
    {
        public Player player { get; set; }
        public Task task { get; set; }
     
        public Task_page(Player p1, Task t1)
        {
            InitializeComponent();
            BackgroundWorker worker = new BackgroundWorker();

            this.task = t1;
            this.player = p1;


            //h1.Task_name = "d";
            task_name_label_tp.Content = task.Task_name;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerAsync();
            

        }
        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressb.Value = e.ProgressPercentage;
            
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int time = Int32.Parse(task.Task_lenght);
            var worker = sender as BackgroundWorker;
            worker.ReportProgress(0);
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(time * 100);
                worker.ReportProgress(i + 1);         

            }
            worker.ReportProgress(100);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            player.money = player.money + Int32.Parse(task.Task_money);
            player.xp = player.xp + Int32.Parse(task.Task_xp);
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(new Uri("mainmenu.xaml", UriKind.Relative));
            NavigationService.Navigate(new Mainmenu(player, task));
        }

        private void progressb_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
