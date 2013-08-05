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
using DBInteraction;
using System.Data;

namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для WorkerWindow.xaml
    /// </summary>
    public partial class WorkerWindow : Window
    {
        private Employee Empl;

        public WorkerWindow(DataRowView dr)
        {
            InitializeComponent();

            initData(Convert.ToInt32(dr[0]));

            tBFamil.Text = dr[2].ToString();
            tBName.Text = dr[1].ToString();
            tBSName.Text = dr[3].ToString();
            tBNumb.Text = dr[0].ToString();
            cBPost.Items.Add(dr[5].ToString());
            cBPost.Text = dr[5].ToString();
        }

        private void initData(int employeeIndex)
        {
            Empl = new Employee(employeeIndex);

            tBFamil.Text = Empl.LastName;
            tBName.Text = Empl.FirstName;
            tBSName.Text = Empl.MiddleName;
            tBNumb.Text = Empl.Id.ToString();
            cBPost.Items.Add(Empl.Position);
            cBPost.Text = Empl.Position;
            cBPost.IsEnabled = false;

            fillMetrics();
            //int i = employeeIndex;
        }

        private void fillMetrics()
        {
            DataSet dsMetrics = Metrics.getDataSetByQuery("EmployeeId = " + Empl.Id);
            dataGrid1.DataContext = dsMetrics.Tables[0].DefaultView;
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowView = dataGrid1.SelectedValue as DataRowView;
            //tBAdd.Text = rowView[1].ToString();
            int num = Convert.ToInt32(rowView[0]);
            MetricsWindow MW = new MetricsWindow(Empl.Id, 1);
            MW.ShowDialog();
            fillMetrics();
        }

        private void btnAddFing_Click(object sender, RoutedEventArgs e)
        {
            MetricsWindow MW = new MetricsWindow(Empl.Id);
            MW.ShowDialog();
            fillMetrics();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
