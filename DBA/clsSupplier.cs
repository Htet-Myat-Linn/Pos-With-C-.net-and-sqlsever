using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace POS.DBA
{
    class clsSupplier
    {
        public int SID { get; set; }
        public string SNAME { get; set; }
        public string ADDRESS { get; set; }
        public string PHONE { get; set; }
        public string UPDATE { get; set; }
        public int ACTION { get; set; }

        clsMainDB obj_clsMainDB = new clsMainDB();

        public void SaveData()
        {
            try
            {
                obj_clsMainDB.DatatBaseConn();
                SqlCommand sql = new SqlCommand("SP_Insert_Supplier", obj_clsMainDB.con);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@SupplierID", SID);
                sql.Parameters.AddWithValue("@SupplierName", SNAME);
                sql.Parameters.AddWithValue("@Address", ADDRESS);
                sql.Parameters.AddWithValue("@Phone", PHONE);
                sql.Parameters.AddWithValue("@UpdateDate", UPDATE);
                sql.Parameters.AddWithValue("@action", ACTION);
                sql.ExecuteNonQuery();
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error In Save Data");

            }
            finally
            {
                obj_clsMainDB.con.Close();
            }
        }
    }
}
