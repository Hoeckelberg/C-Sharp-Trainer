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
using Memory;
using System.ComponentModel;
using System.Threading;

namespace GothicTrainer
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Mem m = new Mem();
        private readonly BackgroundWorker BGWorker = new BackgroundWorker();

        public MainWindow()
        {
            BGWorker.DoWork += BGWorker_DoWork;
            BGWorker.RunWorkerCompleted += BGWorker_RunWorkerCompleted;
            BGWorker.ProgressChanged += BGWorker_ProgessChanged;
            InitializeComponent();
            
        }
        bool ProcOpen = false;

        private void BGWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ProcOpen = m.OpenProcess("Gothic2");
            if (!ProcOpen)
            {
                Thread.Sleep(1000);
                return;
            }
            BGWorker.WorkerReportsProgress = true;
            Thread.Sleep(1000);
            BGWorker.ReportProgress(0);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BGWorker.RunWorkerAsync();
        }

        private void BGWorker_ProgessChanged(object sender, ProgressChangedEventArgs e)
        {
            if (ProcOpen)
                ProcOpenLabel.Content = "Game Found";
        }

        private async Task ChangeValueAsync()
        {
            m.WriteMemory("base+004CECBC,1B8", "int", HealthTB.Text);
            await Task.Delay(200);
        }

        private void BGWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BGWorker.RunWorkerAsync();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (HealthTB.Text != "")
                m.WriteMemory("base+004CECBC,1B8", "int", HealthTB.Text);
        }

        private void Stärke_BTN_Click(object sender, RoutedEventArgs e)
        {
            if (StärkeTB.Text != "")
                m.WriteMemory("base+004CECBC,1C8", "int", StärkeTB.Text);
        }


        private async void cb_Click(object sender, RoutedEventArgs e)
        {
            while (cb.IsChecked == true)
            {
                await ChangeValueAsync();
            }
        }

        private void EHBTN_Click(object sender, RoutedEventArgs e)
        {
            if (EHTB.Text != "")
                m.WriteMemory("base+004CECBC,1DC", "int", EHTB.Text);
        }

        private void GBTN_Click(object sender, RoutedEventArgs e)
        {
            if (GTB.Text != "")
                m.WriteMemory("base+004CECBC,230", "int", GTB.Text);
        }

        private async Task ChangeValueAsyncFlags()
        {
            m.WriteMemory("base+004CECBC,1B4", "int", "2");
            await Task.Delay(200);
        }

        private async Task ChangeValueAsyncFlagsback()
        {
            m.WriteMemory("base+004CECBC,1B4", "int", "0");
            await Task.Delay(200);
        }

        private async void FCB_Click(object sender, RoutedEventArgs e)
        {
            int i = 1;
            while (i == 1)
            {
                if (FCB.IsChecked == true)
                {
                    await ChangeValueAsyncFlags();
                }
                else
                {
                    await ChangeValueAsyncFlagsback();
                }
            }
        }
        private void ErzBTN_Click(object sender, RoutedEventArgs e)
        {
            if (ErzTB.Text != "")
                m.WriteMemory("base+006AC470,FC,100,B0,14,8,4,32C", "int", ErzTB.Text);
        }
    }
}
