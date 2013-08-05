using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using DBInteraction;

namespace BusinessObjects
{
    public class Finger
    {
        public int Id;
        public byte[] Image;
        public int Number;
        public int MetricsId;
        public int EmployeeId;

        public static DBHandler DBHandlerInstance = null;

        public Finger(int id)
        {
            this.Id = id;
            if (DBHandlerInstance != null)
            {
                String query = "SELECT f.Id, f.Finger, f.Number, f.MetricsId, f.EmployeeId FROM Finger f WHERE f.Id = " + Id.ToString();
                DataSet dtSet = DBHandlerInstance.execute(query);
                fillFields(dtSet);
            }
        }

        public Finger(string userQuery)
        {
            if (DBHandlerInstance != null)
            {
                String query = "SELECT f.Id, f.Finger, f.Number, f.MetricsId, f.EmployeeId FROM Finger f WHERE " + userQuery;
                DataSet dtSet = DBHandlerInstance.execute(query);
                fillFields(dtSet);
            }
        }

        public Finger(FileStream fs, int mId, int num, int eId)
        {
            Image = new byte[fs.Length];
            fs.Read(Image, 0, (int)fs.Length);
            MetricsId = mId;
            Number = num;
            EmployeeId = eId;
            fs.Close();
        }

        public void fillFields(DataSet dtSet)
        {
            Id = (int)((uint)dtSet.Tables[0].Rows[0]["Id"]);
            Image = (byte[])dtSet.Tables[0].Rows[0]["Finger"];
            Number = (int)((uint)dtSet.Tables[0].Rows[0]["Number"]);
            MetricsId = (int)((uint)dtSet.Tables[0].Rows[0]["MetricsId"]);
            try
            {
                EmployeeId = (int)((uint)dtSet.Tables[0].Rows[0]["EmployeeId"]);
            }
            catch (Exception exc) { }
        }

        public static ArrayList getArrayBySet(DataSet dtSet)
        {
            ArrayList fArray = new ArrayList();
            foreach (DataRow drc in dtSet.Tables[0].Rows)
            {
                Finger f = new Finger((int)((uint)drc["Id"]));
                fArray.Add(f);
            }
            return fArray;
        }

        public static DataSet getDataSetByQuery(string userQuery)
        {
            if (DBHandlerInstance != null)
            {
                String query = "SELECT f.Id, f.Finger, f.Number, f.MetricsId, f.EmployeeId FROM " +
                                "Finger f WHERE " + userQuery;
                DataSet dtSet = DBHandlerInstance.execute(query);

                return dtSet;//prepareDataSetForDisplaying(dtSet);
            }
            return null;
        }

        public static DataSet prepareDataSetForDisplaying(DataSet dtSet)
        {
            //dtSet.Tables[0].Columns[0].ColumnMapping = MappingType.Hidden;
            //dtSet.Tables[0].Columns[1].ColumnName = "Название";
            //dtSet.Tables[0].Columns[2].ColumnName = "Дата";
            //dtSet.Tables[0].Columns[3].ColumnMapping = MappingType.Hidden;
            return dtSet;
        }

        public static Finger getFingerWithNumber(ArrayList fingsArray, int numb)
        {
            foreach (Finger f in fingsArray)
                if (f.Number == numb) return f;
            return null;
        }

        public void toPictureBox(PictureBox pb)
        {
            pb.Image = new System.Drawing.Bitmap(new System.IO.MemoryStream(Image));
            pb.SizeMode = PictureBoxSizeMode.Zoom;
        }

        public void insertIntoDB()
        {
            //MemoryStream ms = new MemoryStream(Image);
            //char[] c;
            //ms.Read(c, 0, Image.Length);
            
            String imgstr = new string(Encoding.UTF8.GetChars(Image));
            //foreach (byte b in Image)
            //{
            //    imgstr += (char)b;
            //}

            String query = "INSERT INTO Finger (Finger, Number, MetricsId, EmployeeId) " +
                "VALUES (?Data, " 
                + Number.ToString() 
                + ", " 
                + MetricsId.ToString() 
                + ", " 
                + EmployeeId.ToString() 
                + ")";
            
            //DataSet dtSet = DBHandlerInstance.execute(query);
            DBHandlerInstance.executeNQ(query, Image);
            query = "select last_insert_id()";
            DataSet dtSet = DBHandlerInstance.execute(query);
            this.Id = (int)(Int64)dtSet.Tables[0].Rows[0][0];
        }
       
        /*public static void allFromDbToFiles()
        {
            ArrayList Fingers = Finger.getArrayBySet(Finger.getDataSetByQuery("1=1"));
            String path;
            foreach (Finger f in Fingers)
            {
                path = DllImports.ImageProc.IMP_TMP_DIR + f.Id.ToString()//DateTime.Now.ToBinary().ToString() 
                    + ".bmp";
                FileStream fs = new FileStream(path, FileMode.Create);
                fs.Write(f.Image, 0, f.Image.Length);
                fs.Close();
            }
        }*/
    }
}
