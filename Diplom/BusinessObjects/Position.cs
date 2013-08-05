using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DBInteraction;

namespace BusinessObjects
{
    class Position
    {
        public int Id;
        public String Name;

        public static DBHandler DBHandlerInstance = null;

        public Position()
        { }

        public Position(int id)
        {
            this.Id = id;
            if (DBHandlerInstance != null)
            {
                String query = "SELECT p.p_id, p.Name FROM posit p WHERE p.p_id = " + Id.ToString();

                DataSet dtSet = DBHandlerInstance.execute(query);
                fillFields(dtSet);
            }
        }

        public Position(string userQuery)
        {
            if (DBHandlerInstance != null)
            {
                String query = "SELECT p.p_id, p.Name FROM posit p WHERE " + userQuery;

                DataSet dtSet = DBHandlerInstance.execute(query);
                fillFields(dtSet);
            }
        }

        public void fillFields(DataSet dtSet)
        {
            Id = (int)((uint)dtSet.Tables[0].Rows[0]["p_id"]);
            Name = (String)dtSet.Tables[0].Rows[0]["Name"];
        }

        public void insertIntoDB()
        {
            String query = "INSERT INTO posit (Name) "
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
                String query = "SELECT p.p_id, p.Name FROM " +
                                "posit p WHERE " + userQuery;
                DataSet dtSet = DBHandlerInstance.execute(query);

                return dtSet;
            }
            return null;
        }

        public static void addPosLocRelation(int pId, int lId)
        {
            String query = "SELECT pli.pl_id FROM " +
                                "posjlocint pli WHERE PositId = " + pId.ToString() + " AND LocationId = " + lId.ToString();
            DataSet dtSet = DBHandlerInstance.execute(query);
            if (dtSet.Tables[0].Rows.Count == 0)
            {
                query = "INSERT INTO posjlocint (PositId, LocationId) VALUES (" + pId.ToString() + ", " + lId.ToString() + ")";
                dtSet = DBHandlerInstance.execute(query);
            }
        }

        public static void deletePosLocRelation(int pId, int lId)
        {
            String query = "DELETE FROM posjlocint WHERE PositId = " + pId.ToString() + " AND LocationId = " + lId.ToString();
            DataSet dtSet = DBHandlerInstance.execute(query);
        }

        public static void delDataSetByPositionId(int Id)
        {
            if (DBHandlerInstance != null)
            {
                String query = "DELETE FROM posit WHERE posit.p_id = " + Id.ToString();
                DBHandlerInstance.executeNQnoData(query);
            }
        }

        public static void updateDB(int Id, string Name)
        {
            String query = "UPDATE posit SET Name = " + "'" + Name + "'"
                + " WHERE p_id = " + Id.ToString();

            DBHandlerInstance.executeNQnoData(query);
        }

        public static DataSet GetLocationsForPosition(int Id)
        {
            String query = "SELECT l_id, Name FROM posjlocint JOIN location ON "
                           + "posjlocint.LocationId = location.l_id WHERE "
                           + "posjlocint.positId = " + Id;

            DataSet dtSet = DBHandlerInstance.execute(query);

            return dtSet;
        }
    }
}

