using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS.DBA;

namespace POS.MasterData
{
    public partial class frm_Item : Form
    {
        public frm_Item()
        {
            InitializeComponent();
        }
        clsItem obj_clsItem = new clsItem();
        clsMainDB obj_clsMainDB = new clsMainDB();

        DataTable DT = new DataTable();
        public bool _IsEdit = false;
        public int _ItemID = 0;
        string SPString = "";

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int Ok;
            if (txtItemName.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type ItemName");
                txtItemName.Focus();
            }
            else if (txtQty.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Qty");
                txtQty.Focus();
            }
            else if (int.TryParse(txtQty.Text, out Ok) == false)
            {
                MessageBox.Show("Qty Should be Number");
                txtQty.Focus();
                txtQty.SelectAll();
            }
            else if (Convert.ToInt32(txtQty.Text) < 0 || Convert.ToInt32(txtQty.Text) > 100)
            {
                MessageBox.Show("Qty Should be Between 0 and 100");
                txtQty.Focus();
                txtQty.SelectAll();
            }
            else if (txtPrice.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Price");
                txtPrice.Focus();
            }
            else if (int.TryParse(txtPrice.Text, out Ok) == false)
            {
                MessageBox.Show("Price Should be Number");
                txtPrice.Focus();
                txtPrice.SelectAll();
            }
            else if (Convert.ToInt32(txtPrice.Text) !=0 && (Convert.ToInt32(txtPrice.Text) < 1000 || Convert.ToInt32(txtPrice.Text) > 1000000))
            {
                MessageBox.Show("Price Should be Between 1 thousand and 10 Lakh Or 0 Price ");
                txtPrice.Focus();
                txtPrice.SelectAll();


            }
            else
            {
                SPString = string.Format("SP_Select_Item N'{0}',N'{1}',N'{2}'", txtItemName.Text.Trim().ToString(), "0", "1");
                DT = obj_clsMainDB.SelectData(SPString);
                if (DT.Rows.Count > 0 && _ItemID != Convert.ToInt32(DT.Rows[0]["ItemID"]))
                {
                    MessageBox.Show("This Item Is Already Exit");
                    txtItemName.Focus();
                    txtItemName.SelectAll();
                }
                else
                {
                    obj_clsItem.ITEMID = _ItemID;
                    obj_clsItem.ITEMNAME = txtItemName.Text;
                    obj_clsItem.QTY = Convert.ToInt32(txtQty.Text);
                    obj_clsItem.PRICE = Convert.ToInt32(txtPrice.Text);
                    obj_clsItem.UPDATE = lblUpdateDate.Text;
                    if (_IsEdit)
                    {
                        obj_clsItem.ACTION = 1;
                        obj_clsItem.SaveData();
                        MessageBox.Show("Successfully Edit", "Succesfully", MessageBoxButtons.OK);
                        this.Close();
                    }
                    else
                    {
                        obj_clsItem.ACTION = 0;
                        obj_clsItem.SaveData();
                        MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                        this.Close();
                    }
                }


            }
        }

          
        private void frm_Item_Load(object sender, EventArgs e)
        {
            string Day = string.Format("{0:D2}", DateTime.Now.Day);
            string Month = string.Format("{0:D2}", DateTime.Now.Month);
            string Year = string.Format("{0:D2}", DateTime.Now.Year);
            lblUpdateDate.Text = Month + "/" + Day + "/" + Year;
            txtQty.Text = "0"; txtPrice.Text = "0";
            txtItemName.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
    }
}
