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
using System.Windows.Shapes;
using BusinessObjects;
using System.IO;
using System.Threading;
using System.Drawing;
using System.Diagnostics;

namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для MinutiaMapWindow.xaml
    /// </summary>
    public partial class MinutiaMapWindow : Window
    {
        private Finger Fing;
        private MinutiaMap MMap;

        public MinutiaMapWindow(int id)
        {
            InitializeComponent();

            button2.IsEnabled = false;
            lblStatus.Content = "Карта минюций не создана";
        }

        private void startImpMonitoring()
        {
            IMPProcessingWindow IMPW = new IMPProcessingWindow();
            IMPW.Show();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //ScanWindow SCWin = new ScanWindow();
            //SCWin.Show();
        }
    }
}
