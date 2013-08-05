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
using DBInteraction;
using BusinessObjects;

namespace Diplom
{
    /// <summary>
    /// Логика взаимодействия для AccessWindow.xaml
    /// </summary>
    public partial class AccessWindow : Window
    {
        public AccessWindow()
        {
            InitializeComponent();

            initPosAndLocs();
        }

        private void initPosAndLocs()
        {
            DataSet dsPositions = Position.getDataSetByQuery("1 = 1");
            dataGridPosition.DataContext = dsPositions.Tables[0].DefaultView;

            DataSet dsLocations = Location.getDataSetByQuery("1 = 1");
            dataGridLocation.DataContext = dsLocations.Tables[0].DefaultView;
        }

        private void refreshAllLocations()
        {
            DataRowView rowView = dataGridPosition.SelectedValue as DataRowView;
            int position_id = Convert.ToInt32(rowView[0]);

            DataSet dsAllLocations = Position.GetLocationsForPosition(position_id);
            dataGridAllLocations.DataContext = dsAllLocations.Tables[0].DefaultView;

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowView = dataGridPosition.SelectedValue as DataRowView;
            int position_id = Convert.ToInt32(rowView[0]);

            DataRowView rowView1 = dataGridLocation.SelectedValue as DataRowView;
            int location_id = Convert.ToInt32(rowView1[0]);

            Position.addPosLocRelation(position_id, location_id);

            refreshAllLocations();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowView = dataGridPosition.SelectedValue as DataRowView;
            int position_id = Convert.ToInt32(rowView[0]);

            DataRowView rowView1 = dataGridAllLocations.SelectedValue as DataRowView;
            int location_id = Convert.ToInt32(rowView1[0]);

            Position.deletePosLocRelation(position_id, location_id);

            refreshAllLocations();
        }

        private void dataGridPosition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            refreshAllLocations();
        }
    }
}
