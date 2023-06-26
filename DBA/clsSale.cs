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
    class clsSale
    {
        public int SID { get; set; }
        public string VOUCHER { get; set; }
        public string SDATE { get; set; }
        public int TOTALAMT { get; set; }
        public int TAX { get; set; }
        public int GRANDTOTAL { get; set; }
        public int USERID { get; set; }
        public int ACTION { get; set; }

        clsMainDB obj_clsMainDB = new clsMainDB();

        public void SaveData()
        {
            try
            {
                obj_clsMainDB.DatatBaseConn();
                SqlCommand sql = new SqlCommand("SP_Insert_Sale", obj_clsMainDB.con);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@SaleID", SID);
                sql.Parameters.AddWithValue("@Voucher", VOUCHER);
                sql.Parameters.AddWithValue("@SaleDate", SDATE);
                sql.Parameters.AddWithValue("@TotalAmount", TOTALAMT);
                sql.Parameters.AddWithValue("@Tax", TAX);
                sql.Parameters.AddWithValue("@GrandTotal", GRANDTOTAL);
                sql.Parameters.AddWithValue("@UserID", USERID);
                sql.Parameters.AddWithValue("@action", ACTION);
                sql.ExecuteNonQuery();
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error In Save Data Sale");

            }
            finally
            {
                obj_clsMainDB.con.Close();
            }
        }
    }
}
