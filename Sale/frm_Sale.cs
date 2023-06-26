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

namespace POS.Sale
{
    public partial class frm_Sale : Form
    {
        clsSale obj_clsSale = new clsSale();
        clsSaleDetail obj_clsSaleDetail = new clsSaleDetail();
        clsItem obj_clsItem = new clsItem();
        clsMainDB obj_clsMainDB = new clsMainDB();

        DataTable DT = new DataTable();
        DataTable DTSale = new DataTable();
        string SPString = "";
        int _SaleID = 0;
        public frm_Sale()
        {
            InitializeComponent();
        }
        private void CreateTable()
        {
            DTSale.Rows.Clear();
            DTSale.Columns.Clear();

            DTSale.Columns.Add("ItemID");
            DTSale.Columns.Add("ItemName");
            DTSale.Columns.Add("QtyOnHand");
            DTSale.Columns.Add("SalePrice");
            DTSale.Columns.Add("SaleQty");
            DTSale.Columns.Add("Total");
            dgvSale.DataSource = DTSale;

            dgvSale.Columns[0].Visible = false;
            dgvSale.Columns[1].Width = (dgvSale.Width / 100) * 30;
            dgvSale.Columns[2].Width = (dgvSale.Width / 100) * 20;
            dgvSale.Columns[2].ReadOnly = true;
            dgvSale.Columns[3].Width = (dgvSale.Width / 100) * 20;
            dgvSale.Columns[3].ReadOnly = true;
            dgvSale.Columns[4].Width = (dgvSale.Width / 100) * 15;
            dgvSale.Columns[5].Width = (dgvSale.Width / 100) * 15;
            dgvSale.Columns[5].ReadOnly = true;

        }

        private void mnuNew_Click(object sender, EventArgs e)
        {

            CreateTable();
            lblTotalAmount.Text = "";
            lblTax.Text = "";
            lblGrandTotal.Text = "";
            txtPayment.Text = "";
            lblRefund.Text = "";
            dgvSale.Focus();
        }

        private void mnuPayment_Click(object sender, EventArgs e)
        {
            if (lblGrandTotal.Text == "")
            {
                MessageBox.Show("There Is No Item Record");
                dgvSale.Focus();
            }
            else
            {
                txtPayment.Text = "";
                txtPayment.Focus();
            }
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            if (dgvSale.Rows.Count <= 1)
            {
                MessageBox.Show("There Is No Record");
                dgvSale.Focus();
            }
            else if (lblRefund.Text == "" || lblRefund.Text == "0")
            {
                MessageBox.Show("Please Check Payment");
                txtPayment.Focus();
            }
            else
            {
                obj_clsSale.VOUCHER = lblSaleVoucher.Text;
                obj_clsSale.SDATE = dtpDate.Value.ToShortDateString();
                obj_clsSale.TOTALAMT = Convert.ToInt32(lblTotalAmount.Text);
                obj_clsSale.TAX = Convert.ToInt32(lblTax.Text);
                obj_clsSale.GRANDTOTAL = Convert.ToInt32(lblGrandTotal.Text);
                obj_clsSale.USERID = Program.UserID;
                obj_clsSale.ACTION = 0;
                obj_clsSale.SaveData();

                SPString = string.Format("SP_Select_Sale N'{0}',N'{1}',N'{2}'","0","0","3");
                DT = obj_clsMainDB.SelectData(SPString);
                _SaleID = Convert.ToInt32(DT.Rows[0]["SaleID"].ToString());

                for (int i = 0; i < DTSale.Rows.Count; i++)
                {
                    obj_clsSaleDetail.SID = _SaleID;
                    obj_clsSaleDetail.ITEMID = Convert.ToInt32(DTSale.Rows[i]["ItemID"].ToString());
                    obj_clsSaleDetail.SQTY = Convert.ToInt32(DTSale.Rows[i]["SaleQty"].ToString());
                    obj_clsSaleDetail.SPRICE = Convert.ToInt32(DTSale.Rows[i]["SalePrice"].ToString());
                    obj_clsSaleDetail.ACTION = 0;
                    obj_clsSaleDetail.SaveData();

                    obj_clsItem.ITEMID = Convert.ToInt32(DTSale.Rows[i]["ItemID"].ToString());
                    obj_clsItem.QTY = Convert.ToInt32(DTSale.Rows[i]["SaleQty"].ToString());
                    obj_clsItem.ACTION = 4;
                }
               MessageBox.Show("Successfully Save","Successfully",MessageBoxButtons.OK,MessageBoxIcon.Information);
               this.Close();
            }
        }

        private void frm_Sale_Load(object sender, EventArgs e)
        {
            CreateTable();

            SPString = string.Format("SP_Select_Sale N'{0}',N'{1}',N'{2}'", dtpDate.Value.ToShortDateString(), "0", "1");

            lblSaleVoucher.Text = obj_clsMainDB.GetVoucher(SPString, dtpDate.Value.ToShortDateString());

            dgvSale.Focus();
        }

        private void dgvSale_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox txtItemName = (TextBox)e.Control;
            txtItemName.AutoCompleteCustomSource.Clear();

            int CurCol = 0;
            CurCol = dgvSale.CurrentCell.ColumnIndex;
            if (CurCol == 1)
            {
                SPString = string.Format("SP_Select_Item N'{0}',N'{1}',N'{2}'", "0", "0", "5");
                obj_clsMainDB.TextBoxData(ref txtItemName, SPString, "ItemName");
            }
        }
        private void CalculateTotal()
        {
            int GrandTotal = 0;
            int TotalSaleQty = 0;

            for (int i = 0; i < dgvSale.Rows.Count - 1; i++)
            {
                DataGridViewRow DR = dgvSale.Rows[i];
                int SaleQty = Convert.ToInt32(DR.Cells["SaleQty"].Value.ToString());
                int SalePrice = Convert.ToInt32(DR.Cells["SalePrice"].Value.ToString());
                int Total = SaleQty * SalePrice;

                DR.Cells["Total"].Value = Total.ToString();

                GrandTotal += Total;
                TotalSaleQty += SaleQty;
            }
            lblTotalAmount.Text = GrandTotal.ToString();
            lblTax.Text = (TotalSaleQty * 50).ToString();
            lblGrandTotal.Text = (Convert.ToInt32(lblTotalAmount.Text) + Convert.ToInt32(lblTax.Text)).ToString();
            txtPayment.Text = "0";
            lblRefund.Text = "0";
        }

        private void dgvSale_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int CurRow = 0, CurCol = 0;
            String ItemName = "";
            String SaleQty = "0";

            CurRow = dgvSale.CurrentCell.RowIndex;
            CurCol = dgvSale.CurrentCell.ColumnIndex;

            ItemName = dgvSale.Rows[CurRow].Cells["ItemName"].Value.ToString();

            if (ItemName != "")
            {
                if (CurCol - 1 == 1)
                {
                    SPString = string.Format("SP_Select_Item N'{0}',N'{1}',N'{2}'", ItemName, "0", "6");

                    DT = obj_clsMainDB.SelectData(SPString);
                    if (DT.Rows.Count <= 0)
                    {
                        MessageBox.Show("This ItemName Is Not Exit");
                        SendKeys.Send("{HOME}");
                    }
                    else
                    {
                        bool AddRow = true;
                        for (int i = 0; i < dgvSale.Rows.Count - 1; i++)
                        {
                            if (dgvSale.Rows[i].Cells["ItemName"].Value.ToString() == ItemName && i != CurRow)
                            {
                                MessageBox.Show("This Item Is Already Exit");
                                AddRow = false;
                                SendKeys.Send("{HOME}");

                            }
                        }
                        if (AddRow)
                        {
                            dgvSale.Rows[CurRow].Cells["ItemID"].Value = DT.Rows[0]["ItemID"].ToString();
                            dgvSale.Rows[CurRow].Cells["QtyOnHand"].Value = DT.Rows[0]["Qty"].ToString();
                            dgvSale.Rows[CurRow].Cells["SalePrice"].Value = DT.Rows[0]["Price"].ToString();
                            dgvSale.Rows[CurRow].Cells["SaleQty"].Value = "0";
                            dgvSale.Rows[CurRow].Cells["Total"].Value = "0";
                            SendKeys.Send("{TAB}");
                            SendKeys.Send("{TAB}");
                            CalculateTotal();

                        }
                    }
                }

                if (CurCol - 1 == 4)
                {
                    dgvSale.CurrentRow.Cells["Total"].Value = "0";

                    int OK;
                    SaleQty = dgvSale.Rows[CurRow].Cells["SaleQty"].Value.ToString();
                    if (int.TryParse(SaleQty, out OK) == false)
                    {
                        MessageBox.Show("SaleQty Should Be Number");
                        SendKeys.Send("{HOME}");
                        SendKeys.Send("{TAB}");
                    }
                    else if (Convert.ToInt32(SaleQty) <= 0 || Convert.ToInt32(SaleQty) > Convert.ToInt32(dgvSale.Rows[CurRow].Cells["QtyOnHand"].Value.ToString()))
                    {
                        MessageBox.Show("SaleQty Should Be Between 1 And " + dgvSale.Rows[CurRow].Cells["QtyOnHand"].Value.ToString());
                        SendKeys.Send("{HOME}");
                        SendKeys.Send("{TAB}");
                    }
                    else
                    {
                        CalculateTotal();
                    }

                }
            }
        }

        private void txtPayment_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (lblTotalAmount.Text == "" || lblTotalAmount.Text == "0")
            {
                MessageBox.Show("There Is No Item Record");
                dgvSale.Focus();
            }
            else
            {
                int OK;
                lblRefund.Text = "0";

                if (e.KeyChar.Equals('\r'))
                {
                    if (txtPayment.Text == "")
                    {
                        MessageBox.Show("Please Type Payment");
                        txtPayment.Focus();
                    }
                    else if (int.TryParse(txtPayment.Text, out OK) == false)
                    {
                        MessageBox.Show("Payment Should Be Number");
                        txtPayment.Focus();
                        txtPayment.SelectAll();
                    }
                    else if (Convert.ToInt32(txtPayment.Text) < Convert.ToInt32(lblGrandTotal.Text))
                    {
                        MessageBox.Show("Payment Amount Should Be Above " + lblGrandTotal.Text);
                        txtPayment.Focus();
                        txtPayment.SelectAll();
                    }
                    else if (Convert.ToInt32(txtPayment.Text) > Convert.ToInt32(lblGrandTotal.Text) + 10000)
                    {
                        MessageBox.Show("Payment Amount Should Be Below " + (Convert.ToInt32(lblGrandTotal.Text) + 10000));
                        txtPayment.Focus();
                        txtPayment.SelectAll();
                    }
                    else
                    {
                        lblRefund.Text = (Convert.ToInt32(txtPayment.Text) - Convert.ToInt32(lblGrandTotal.Text)).ToString();
                    }
                }
            }
        
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
          SPString = String.Format("SP_Select_Sale N'{0}',N'{1}',N'{2}'",dtpDate.Value.ToShortDateString(),"0","2");
          DT = obj_clsMainDB.SelectData(SPString);
          int DateDiff = Convert.ToInt32(DT.Rows[0]["No"]);

          if (DateDiff > 0)
          {
              MessageBox.Show("Please Check SaleDate");
              dtpDate.Text = DateTime.Now.ToShortDateString();
          }
          else if (DateDiff <= -7)
          {
              MessageBox.Show("Pleae Check SaleDate");
              dtpDate.Text = DateTime.Now.ToShortDateString();
          }
          else
          {
              SPString = string.Format("SP_Select_Sale N'{0}',N'{1}',N'{2}'",dtpDate.Value.ToShortDateString(),"0","1");
              lblSaleVoucher.Text = obj_clsMainDB.GetVoucher(SPString,dtpDate.Value.ToShortDateString());
          }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

       
    }

}