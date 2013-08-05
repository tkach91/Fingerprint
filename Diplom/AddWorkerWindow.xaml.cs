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
using System.Data;
using BusinessObjects;

namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для AddWorkerWindow.xaml
    /// </summary>
    public partial class AddWorkerWindow : Window
    {
        private DataSet dtPositions;
        MainWindow mainw;

        public AddWorkerWindow(MainWindow mw)
        {
            InitializeComponent();

            mainw = mw;

            dtPositions = Position.getDataSetByQuery("1 = 1");
            for (int i = 0; i < dtPositions.Tables[0].Rows.Count; i++)
                cBPost.Items.Add(dtPositions.Tables[0].Rows[i][1]);
            cBPost.SelectedItem = cBPost.Items[0];
        }

        private void btnAddData_Click(object sender, RoutedEventArgs e)
        {
            Employee Empl = new Employee();

            Empl.FirstName = tBName.Text;
            Empl.LastName = tBFamil.Text;
            Empl.MiddleName = tBSName.Text;

            foreach (DataRow dr in dtPositions.Tables[0].Rows)
            {
                if (dr[1] == cBPost.SelectedItem)
                {
                    Empl.PositionId = Int16.Parse(dr[0].ToString());
                    break;
                }
            }

            Empl.insertIntoDB();
            mainw.refreshTable();
            this.Close();
        }
    }
}
