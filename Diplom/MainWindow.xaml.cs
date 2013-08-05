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
using System.Data;
using DBInteraction;
using BusinessObjects;

namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DBNetBroker DBNB;

        public MainWindow()
        {
            InitializeComponent();

            initDB();

            refreshTable();
        }

        private void initDB()
        {
            DBNB = new DBNetBroker("localhost", "", "", "fingerdata");
            DBHandler DBH = new DBHandler(DBNB);

            Employee.DBHandlerInstance = DBH;
            //DirectionMap.DBHandlerInstance = DBH;
            //Finger.DBHandlerInstance = DBH;
            Location.DBHandlerInstance = DBH;
            Metrics.DBHandlerInstance = DBH;
            //Minutia.DBHandlerInstance = DBH;
            //MinutiaMap.DBHandlerInstance = DBH;
            Position.DBHandlerInstance = DBH;
        }

        public void refreshTable()
        {
            DataSet dsEmployees = Employee.getAllEmployees();
            dGRes.DataContext = dsEmployees.Tables[0].DefaultView;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddWorkerWindow AWorkWin = new AddWorkerWindow(this);
            AWorkWin.Show();
        }

        private void btnPosts_Click(object sender, RoutedEventArgs e)
        {
            PositionWindow DWin = new PositionWindow();
            DWin.Show();
        }

        private void btnRooms_Click(object sender, RoutedEventArgs e)
        {
            DirectoryWindow DWin = new DirectoryWindow();
            DWin.Show();
        }

        private void btnAccess_Click(object sender, RoutedEventArgs e)
        {
            AccessWindow acsWin = new AccessWindow();
            acsWin.Show();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowView = dGRes.SelectedValue as DataRowView;
            WorkerWindow WorkWin = new WorkerWindow(rowView);
            WorkWin.Show();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            DataSet dsEmployees;
            if ((tBFamil.Text != "") && (tBName.Text != "") && (tBSName.Text != "") && (tBNumb.Text != ""))
            {
                dsEmployees = Employee.getDataSetByQuery("e.LastName = " + "'" + tBFamil.Text + "'" + " AND e.FirstName = " + "'" + tBName.Text + "'" + " AND e.MiddleName = " + "'" + tBSName.Text + "'" + " AND e.e_id = " + "'" + tBNumb.Text + "'");
            }
            else if ((tBFamil.Text != "") && (tBName.Text != "") && (tBNumb.Text != ""))
            {
                dsEmployees = Employee.getDataSetByQuery("e.LastName = " + "'" + tBFamil.Text + "'" + " AND e.FirstName = " + "'" + tBName.Text + "'" + " AND e.e_id = " + "'" + tBNumb.Text + "'");
            }
            else if ((tBFamil.Text != "") && (tBName.Text != "") && (tBSName.Text != ""))
            {
                dsEmployees = Employee.getDataSetByQuery("e.LastName = " + "'" + tBFamil.Text + "'" + " AND e.FirstName = " + "'" + tBName.Text + "'" + " AND e.MiddleName = " + "'" + tBSName.Text + "'");
            }
            else if ((tBSName.Text != "") && (tBNumb.Text != ""))
            {
                dsEmployees = Employee.getDataSetByQuery("e.MiddleName = " + "'" + tBSName.Text + "'" + " AND e.e_id = " + "'" + tBNumb.Text + "'");
            }
            else if ((tBName.Text != "") && (tBNumb.Text != ""))
            {
                dsEmployees = Employee.getDataSetByQuery("e.FirstName = " + "'" + tBName.Text + "'" + " AND e.e_id = " + "'" + tBNumb.Text + "'");
            }
            else if ((tBName.Text != "") && (tBSName.Text != ""))
            {
                dsEmployees = Employee.getDataSetByQuery("e.FirstName = " + "'" + tBName.Text + "'" + " AND e.MiddleName = " + "'" + tBSName.Text + "'");
            }
            else if ((tBFamil.Text != "") && (tBNumb.Text != ""))
            {
                dsEmployees = Employee.getDataSetByQuery("e.LastName = " + "'" + tBFamil.Text + "'" + " AND e.e_id = " + "'" + tBNumb.Text + "'");
            }
            else if ((tBFamil.Text != "") && (tBSName.Text != ""))
            {
                dsEmployees = Employee.getDataSetByQuery("e.LastName = " + "'" + tBFamil.Text + "'" + " AND e.MiddleName = " + "'" + tBSName.Text + "'");
            }
            else if ((tBFamil.Text != "") && (tBName.Text != ""))
            {
                dsEmployees = Employee.getDataSetByQuery("e.LastName = " + "'" + tBFamil.Text + "'" + " AND e.FirstName = " + "'" + tBName.Text + "'");
            }
            else if (tBFamil.Text != "")
            {
                dsEmployees = Employee.getDataSetByQuery("e.LastName = " + "'" + tBFamil.Text + "'");
            }
            else if (tBName.Text != "")
            {
                dsEmployees = Employee.getDataSetByQuery("e.FirstName = " + "'" + tBName.Text + "'");
            }
            else if (tBSName.Text != "")
            {
                dsEmployees = Employee.getDataSetByQuery("e.MiddleName = " + "'" + tBSName.Text + "'");
            }
            else if (tBNumb.Text != "")
            {
                dsEmployees = Employee.getDataSetByQuery("e.e_id = " + "'" + tBNumb.Text + "'");
            }
            else
            {
                dsEmployees = Employee.getAllEmployees();
            }

            dGRes.DataContext = dsEmployees.Tables[0].DefaultView;
        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            /*MinutiaMapWindow mmp = new MinutiaMapWindow(1);
            mmp.Show();*/
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Verification VW = new Verification();
            VW.Show();
        }
    }
}
