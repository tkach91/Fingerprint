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
using DBInteraction;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для PersonPage.xaml
    /// </summary>
    public partial class PersonPage : Window
    {
        private MySqlDataReader GetPositionForUser(int uid)
        {
            DBNetBroker DBB = new DBNetBroker("localhost", "", "", "fingerdata");
            DBB.Connection.Open();

            string query = "SELECT * FROM employee WHERE employee.e_id = " + "'" + uid + "'";

            MySqlCommand msc = new MySqlCommand(query, DBB.Connection);

            return msc.ExecuteReader();
        }

        public PersonPage(int uid)
        {
            InitializeComponent();

            MySqlDataReader r = GetPositionForUser(uid);

            r.Read();
            int position_id = r.GetInt32(4);

            DataSet dsAllLocations = Position.GetLocationsForPosition(position_id);
            dataGridAllLocations.DataContext = dsAllLocations.Tables[0].DefaultView;
        }
    }
}
