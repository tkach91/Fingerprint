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
using System.Collections;
using BusinessObjects;
using DBInteraction;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для MetricsWindow.xaml
    /// </summary>
    public partial class MetricsWindow : Window
    {
        ArrayList FingsArray;
        ArrayList Fingers;
        int MetricId;
        int EmployeeId;
        Metrics Metric;
        Employee Empl;
        int RightClickedFingerIndex;

        public MetricsWindow(int e_id)
        {
            InitializeComponent();

            this.EmployeeId = e_id;
            
            FingsArray = new ArrayList();
            /*FingsArray.Add(pB1);
            FingsArray.Add(pB2);
            FingsArray.Add(pB3);
            FingsArray.Add(pB4);
            FingsArray.Add(pB5);
            FingsArray.Add(pB6);
            FingsArray.Add(pB7);
            FingsArray.Add(pB8);
            FingsArray.Add(pB9);
            FingsArray.Add(pB10);*/

            Metric = new Metrics();
            Metric.EmployeeId = EmployeeId;
            Metric.Date = DateTime.Now;
            Metric.Name = DateTime.Now.ToString();
            Metric.insertIntoDB();
            MetricId = Metric.Id;

            Fingers = new ArrayList();

            // установка лэйблов
            Empl = new Employee(EmployeeId);
            lblName.Content += " " + Empl.FirstName;
            lblFamil.Content += " " + Empl.LastName;
            lblSName.Content += " " + Empl.MiddleName;
            lblSnapshot.Content += " " + Metric.Name;
        }

        private MySqlDataReader GetData()
        {
            DBNetBroker DBB = new DBNetBroker("localhost", "", "", "fingerdata");
            DBB.Connection.Open();

            string query = "SELECT * FROM finger WHERE finger.MetricsId = " + "'" + Metric.Id + "'" + "AND finger.EmployeeID = " + "'" + Empl.Id + "'";

            MySqlCommand msc = new MySqlCommand(query, DBB.Connection);

            return msc.ExecuteReader();
        }

        public MetricsWindow(int e_id, int m_id)
        {
            InitializeComponent();

            this.EmployeeId = e_id;

            FingsArray = new ArrayList();
            /*FingsArray.Add(pB1);
            FingsArray.Add(pB2);
            FingsArray.Add(pB3);
            FingsArray.Add(pB4);
            FingsArray.Add(pB5);
            FingsArray.Add(pB6);
            FingsArray.Add(pB7);
            FingsArray.Add(pB8);
            FingsArray.Add(pB9);
            FingsArray.Add(pB10);*/

            Metric = new Metrics("EmployeeId = " + "'" + EmployeeId + "'");

            Fingers = new ArrayList();

            // установка лэйблов
            Empl = new Employee(EmployeeId);
            lblName.Content += " " + Empl.FirstName;
            lblFamil.Content += " " + Empl.LastName;
            lblSName.Content += " " + Empl.MiddleName;
            lblSnapshot.Content += " " + Metric.Name;

            MySqlDataReader reader = GetData();

            while (reader.Read())
            {
                if ((int)reader.GetValue(2) == 1)
                    label11.Content = "В базе";
                if ((int)reader.GetValue(2) == 2)
                    label12.Content = "В базе";
                if ((int)reader.GetValue(2) == 3)
                    label13.Content = "В базе";
                if ((int)reader.GetValue(2) == 4)
                    label14.Content = "В базе";
                if ((int)reader.GetValue(2) == 5)
                    label15.Content = "В базе";
                if ((int)reader.GetValue(2) == 6)
                    label6.Content = "В базе";
                if ((int)reader.GetValue(2) == 7)
                    label7.Content = "В базе";
                if ((int)reader.GetValue(2) == 8)
                    label8.Content = "В базе";
                if ((int)reader.GetValue(2) == 9)
                    label9.Content = "В базе";
                if ((int)reader.GetValue(2) == 10)
                    label10.Content = "В базе";
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            MySqlDataReader reader = GetData();

            while (reader.Read())
            {
                if ((int)reader.GetValue(2) == 1)
                    label11.Content = "В базе";
                if ((int)reader.GetValue(2) == 2)
                    label12.Content = "В базе";
                if ((int)reader.GetValue(2) == 3)
                    label13.Content = "В базе";
                if ((int)reader.GetValue(2) == 4)
                    label14.Content = "В базе";
                if ((int)reader.GetValue(2) == 5)
                    label10.Content = "В базе";
                if ((int)reader.GetValue(2) == 6)
                    label6.Content = "В базе";
                if ((int)reader.GetValue(2) == 7)
                    label7.Content = "В базе";
                if ((int)reader.GetValue(2) == 8)
                    label8.Content = "В базе";
                if ((int)reader.GetValue(2) == 9)
                    label9.Content = "В базе";
                if ((int)reader.GetValue(2) == 10)
                    label10.Content = "В базе";
            }
        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            ScanWindow SCWin = new ScanWindow(Empl.Id, Metric.Id);
            SCWin.Show();
        }   

        /*private void WindowsFormsHost_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MinutiaMapWindow MMW = new MinutiaMapWindow(Finger.getFingerWithNumber(Fingers, FingsArray.IndexOf(sender)).Id);
            MMW.Show();
        }*/
    }
}
