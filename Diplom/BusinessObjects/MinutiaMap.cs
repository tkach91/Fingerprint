using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.IO;
using DBInteraction;

namespace BusinessObjects
{
    class MinutiaMap
    {
        public int Id;
        public String Name;
        public int FingerId;
        public byte[] Map;

        public static DBHandler DBHandlerInstance = null;

        public MinutiaMap()
        { }

        public MinutiaMap(int id)
        {
            this.Id = id;
            if (DBHandlerInstance != null)
            {
                String query = "SELECT m.Id, m.Name, m.FingerId FROM MinutiaMap m WHERE m.Id = " + Id.ToString();
                DataSet dtSet = DBHandlerInstance.execute(query);
                fillFields(dtSet);
            }
        }

        public MinutiaMap(String userQuery)
        {
            if (DBHandlerInstance != null)
            {
                String query = "SELECT m.Id, m.Name, m.FingerId, m.Map FROM MinutiaMap m WHERE " + userQuery;
                DataSet dtSet = DBHandlerInstance.execute(query);
                fillFields(dtSet);
            }
        }

        public MinutiaMap(String path, int fingId)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            Map = new byte[fs.Length];
            fs.Read(Map, 0, (int)fs.Length);
            FingerId = fingId;
            fs.Close();
        }

        public void insertIntoDB()
        {
            String query = "INSERT INTO MinutiaMap (FingerId, Map) " +
                "VALUES ("
                + FingerId.ToString()
                + ", ?Data)";

            //DataSet dtSet = DBHandlerInstance.execute(query);
            DBHandlerInstance.executeNQ(query, Map);
            query = "select last_insert_id()";
            DataSet dtSet = DBHandlerInstance.execute(query);
            this.Id = (int)(Int64)dtSet.Tables[0].Rows[0][0];
        }

        public void toFile(String path)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            fs.Write(Map, 0, Map.Length);
            fs.Close();
        }

        public void fillFields(DataSet dtSet)
        {
            Id = (int)((uint)dtSet.Tables[0].Rows[0]["Id"]);
            try { Name = (String)dtSet.Tables[0].Rows[0]["Name"]; }
            catch (Exception exc) { }
            FingerId = (int)((uint)dtSet.Tables[0].Rows[0]["FingerId"]);
            Map = (byte[])dtSet.Tables[0].Rows[0]["Map"];
        }

        public static ArrayList getArrayBySet(DataSet dtSet)
        {
            ArrayList mmArray = new ArrayList();
            foreach (DataRow drc in dtSet.Tables[0].Rows)
            {
                MinutiaMap m = new MinutiaMap();
                m.Id = (int)((uint)drc["Id"]);
                m.Name = (String)drc["Name"];
                m.FingerId = (int)((uint)drc["FingerId"]);
                mmArray.Add(m);
            }
            return mmArray;
        }

        public static ArrayList getArrayByFingerId(int fId)
        {
            if (DBHandlerInstance != null)
            {
                String query = "SELECT m.Id, m.Name, m.FingerId FROM MinutiaMap m WHERE m.FingerId = " + fId;
                DataSet dtSet = DBHandlerInstance.execute(query);
                return getArrayBySet(dtSet);
            }
            return null;
        }
    }
}
