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
    class clsPurchase
    {

        public int PID { get; set; }
        public string PDATE { get; set; }
        public int SUPID { get; set; }
        public int TOTALAMT { get; set; }
        public int USERID { get; set; }
        public int ACTION { get; set; }

        clsMainDB obj_clsMainDB = new clsMainDB();

        public void SaveData()
        {
            try
            {
                obj_clsMainDB.DatatBaseConn();
                SqlCommand sql = new SqlCommand("SP_Insert_Purchase", obj_clsMainDB.con);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@PurchaseID", PID);
                sql.Parameters.AddWithValue("@PurchaseDate", PDATE);
                sql.Parameters.AddWithValue("@SupplierID", SUPID);
                sql.Parameters.AddWithValue("@totalAmount", TOTALAMT);
                sql.Parameters.AddWithValue("@UserID", USERID);
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
