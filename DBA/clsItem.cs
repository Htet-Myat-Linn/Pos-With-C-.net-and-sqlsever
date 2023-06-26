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
    class clsItem
    {
        public int ITEMID{get;set;}
        public string ITEMNAME { get; set; }
        public int QTY{get;set;}
        public int PRICE{get;set;}
        public string UPDATE{get;set;}
        public int ACTION{get;set;}

        clsMainDB obj_clsMainDB=new clsMainDB();
 
        public void SaveData()
        {
            try
            {
               obj_clsMainDB.DatatBaseConn();
               SqlCommand sql=new SqlCommand("SP_Insert_Item",obj_clsMainDB.con);
               sql.CommandType=CommandType.StoredProcedure;
               sql.Parameters.AddWithValue("@ItemID",ITEMID);
               sql.Parameters.AddWithValue("@ItemName",ITEMNAME);
               sql.Parameters.AddWithValue("@Qty",QTY);
               sql.Parameters.AddWithValue("@Price",PRICE);
               sql.Parameters.AddWithValue("@UpdateDate",UPDATE);
               sql.Parameters.AddWithValue("@action",ACTION);
               sql.ExecuteNonQuery();
            }

        
            catch(Exception ex)
            {
                 MessageBox.Show(ex.ToString(),"Error In Save Data Item");

            }
            finally
            {
                 obj_clsMainDB.con.Close();
            }
         }
    }
}
