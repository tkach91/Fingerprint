using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using DBInteraction;

namespace BusinessObjects
{
    class Metrics
    {        
        public int Id;
        public String Name;
        public DateTime Date;
        public int EmployeeId;

        public static DBHandler DBHandlerInstance = null;

        public Metrics()
        { }

        public Metrics(int id)
        {
            this.Id = id;
            if (DBHandlerInstance != null)
            {
                String query = "SELECT m.m_id, m.Name, m.CDate, m.EmployeeId FROM Metrics m WHERE m.m_id = " + Id.ToString();
                DataSet dtSet = DBHandlerInstance.execute(query);
                fillFields(dtSet);
            }
        }

        public Metrics(string userQuery)
        {
            if (DBHandlerInstance != null)
            {
                String query = "SELECT m.m_id, m.Name, m.CDate, m.EmployeeId FROM Metrics m WHERE " + userQuery;
                DataSet dtSet = DBHandlerInstance.execute(query);
                fillFields(dtSet);
            }
        }

        public void fillFields(DataSet dtSet)
        {
            Id = (int)(dtSet.Tables[0].Rows[0]["m_id"]);
            Name = (String)dtSet.Tables[0].Rows[0]["Name"];
            Date = (DateTime)dtSet.Tables[0].Rows[0]["CDate"];
            EmployeeId = (int)(dtSet.Tables[0].Rows[0]["EmployeeId"]);
        }


        public static ArrayList getArrayBySet(DataSet dtSet)
        {
            ArrayList mArray = new ArrayList();
            foreach (DataRow drc in dtSet.Tables[0].Rows)
            {
                Metrics m = new Metrics();
                m.Id = (int)((uint)drc["Id"]);
                m.Name = (String)drc["Name"];
                m.Date = (DateTime)drc["Date"];
                m.EmployeeId = (int)(uint)drc["EmployeeId"];
                mArray.Add(m);
            }
            return mArray;
        }

        public static DataSet getDataSetByQuery(string userQuery)
        {
            if (DBHandlerInstance != null)
            {
                String query = "SELECT m.m_id, m.Name, m.CDate, m.EmployeeId FROM " +
                                "Metrics m WHERE " + userQuery;
                DataSet dtSet = DBHandlerInstance.execute(query);

                return dtSet;
            }
            return null;
        }

        public void insertIntoDB()
        {
            String query = "INSERT INTO Metrics (Name, CDate, EmployeeId) " +
                "VALUES ('"
                + Name
                + "', "
                + "?Date"
                + ", "
                + EmployeeId.ToString()
                + ")";

            //DataSet dtSet = DBHandlerInstance.execute(query);
            DBHandlerInstance.executeNQDate(query, Date);
            query = "select last_insert_id()";
            DataSet dtSet = DBHandlerInstance.execute(query);
            this.Id = (int)(Int64)dtSet.Tables[0].Rows[0][0];
        }
    }
}
