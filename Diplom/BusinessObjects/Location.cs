using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBInteraction;

namespace BusinessObjects
{
    class Location
    {
        public int Id;
        public String Name;

        public static DBHandler DBHandlerInstance = null;

        public Location()
        { }

        public Location(int id)
        {
            this.Id = id;
            if (DBHandlerInstance != null)
            {
                String query = "SELECT l.l_id, l.Name FROM location l WHERE l.l_id = " + Id.ToString();
                DataSet dtSet = DBHandlerInstance.execute(query);
                fillFields(dtSet);
            }
        }

        public Location(string userQuery)
        {
            if (DBHandlerInstance != null)
            {
                String query = "SELECT l.l_id, l.Name FROM location l WHERE " + userQuery;
                DataSet dtSet = DBHandlerInstance.execute(query);
                fillFields(dtSet);
            }
        }

        public void fillFields(DataSet dtSet)
        {
            Id = (int)((uint)dtSet.Tables[0].Rows[0]["l_id"]);
            Name = (String)dtSet.Tables[0].Rows[0]["Name"];
        }

        public void insertIntoDB()
        {
            String query = "INSERT INTO location (Name) "
                + "VALUES ('"
                + Name
                + "')";

            DBHandlerInstance.executeNQnoData(query);
            query = "select last_insert_id()";
            DataSet dtSet = DBHandlerInstance.execute(query);
            this.Id = (int)(Int64)dtSet.Tables[0].Rows[0][0];
        }

        public static DataSet getDataSetByQuery(string userQuery)
        {
            if (DBHandlerInstance != null)
            {
                String query = "SELECT l.l_id, l.Name FROM " +
                                "location l WHERE " + userQuery;
                DataSet dtSet = DBHandlerInstance.execute(query);

                return dtSet;
            }
            return null;
        }

        public static DataSet getDataSetByPositionId(int Id)
        {
            if (DBHandlerInstance != null)
            {
                String query = "SELECT l.l_id, l.Name FROM " +
                                "location l JOIN posjlocint pli ON l.Id = pli.LocationId WHERE pli.PositId = " + Id.ToString();
                DataSet dtSet = DBHandlerInstance.execute(query);

                return dtSet;
            }
            return null;
        }

        public static void delDataSetByLocationId(int Id)
        {
            if (DBHandlerInstance != null)
            {
                String query = "DELETE FROM location WHERE location.l_id = " + Id.ToString();
                DBHandlerInstance.executeNQnoData(query);
            }
        }

        public static void updateDB(int Id, string Name)
        {
            String query = "UPDATE location SET Name = " + "'" + Name + "'"
                + " WHERE l_id = " + Id.ToString();

            DBHandlerInstance.executeNQnoData(query);
        }
    }
}
