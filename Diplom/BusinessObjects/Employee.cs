using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql;
using DBInteraction;

namespace BusinessObjects
{
    class Employee
    {
        public int Id;
        public String FirstName;
        public String LastName;
        public String MiddleName;
        public int PositionId;
        public String Position;

        public static DBHandler DBHandlerInstance = null;

        public Employee()
        { }

        public Employee(int id)
        {
            this.Id = id;
            if (DBHandlerInstance != null)
            {
                String query = "SELECT e.e_id, e.FirstName, e.LastName, e.MiddleName, e.positId, p.Name FROM " + 
                                "employee e JOIN posit p ON p.p_id = e.positId WHERE e.e_id = " + Id.ToString();

                DataSet dtSet = DBHandlerInstance.execute(query);
                fillFields(dtSet);
            }
        }

        public Employee(string userQuery)
        {
            if (DBHandlerInstance != null)
            {
                String query = "SELECT e.e_id, e.FirstName, e.LastName, e.MiddleName, e.positId, p.Name FROM " +
                                "employee e JOIN posit p ON p.p_id = e.positId WHERE " + userQuery;

                DataSet dtSet = DBHandlerInstance.execute(query);
                fillFields(dtSet);
            }
        }

        public void fillFields(DataSet dtSet)
        {
            Id = (int)dtSet.Tables[0].Rows[0]["e_id"];
            FirstName = (String)dtSet.Tables[0].Rows[0]["FirstName"];
            LastName = (String)dtSet.Tables[0].Rows[0]["LastName"];
            MiddleName = (String)dtSet.Tables[0].Rows[0]["MiddleName"];
            PositionId = (int)dtSet.Tables[0].Rows[0]["positId"];
            Position = (String)dtSet.Tables[0].Rows[0]["Name"];
        }

        public static ArrayList getArrayBySet(DataSet dtSet)
        {
            ArrayList eArray = new ArrayList();
            foreach (DataRow drc in dtSet.Tables[0].Rows)
            {
                Employee e = new Employee();
                e.Id = (int)((uint)drc["e_id"]);
                e.FirstName = (String)drc["FirstName"];
                e.LastName = (String)drc["LastName"];
                e.MiddleName = (String)drc["MiddleName"];
                e.PositionId = (int)((uint)drc["positId"]);
                e.Position = (String)drc["Name"];
                eArray.Add(e);
            }
            return eArray;
        }

        public static ArrayList getArrayByQuery(string userQuery)
        {
            if (DBHandlerInstance != null)
            {
                String query = "SELECT e.Id, e.FirstName, e.LastName, e.MiddleName, e.PositionId, p.Name FROM " +
                                "Employee e JOIN posit p ON p.Id = e.PositionId WHERE " + userQuery;
                DataSet dtSet = DBHandlerInstance.execute(query);
                return getArrayBySet(dtSet);
            }
            return null;
        }

        public static DataSet getDataSetByQuery(string userQuery)
        {
            if (DBHandlerInstance != null)
            {
                String query =  "SELECT e.e_id, e.FirstName, e.LastName, e.MiddleName, e.positId, p.Name FROM " +
                                "employee e JOIN posit p ON p.p_id = e.positId WHERE " + userQuery;
                DataSet dtSet = DBHandlerInstance.execute(query);

                return dtSet;
            }
            return null;
        }

        public static DataSet getAllEmployees()
        {
            if (DBHandlerInstance != null)
            {
                String query =  "SELECT e.e_id, e.FirstName, e.LastName, e.MiddleName, e.positId, p.Name FROM " +
                                "employee e JOIN posit p ON p.p_id = e.positId";
                DataSet dtSet = DBHandlerInstance.execute(query);

                return dtSet;
            }
            return null;
        }

        public void insertIntoDB()
        {
            String query = "INSERT INTO employee (FirstName, LastName, MiddleName, positId) " +
                "VALUES ('"
                + FirstName
                + "', '" + LastName
                + "', '" + MiddleName
                + "', '" + PositionId
                + "')";

            //DataSet dtSet = DBHandlerInstance.execute(query);
            DBHandlerInstance.executeNQnoData(query);
            query = "select last_insert_id()";
            DataSet dtSet = DBHandlerInstance.execute(query);
            this.Id = (int)(Int64)dtSet.Tables[0].Rows[0][0];
        }
    }
}
