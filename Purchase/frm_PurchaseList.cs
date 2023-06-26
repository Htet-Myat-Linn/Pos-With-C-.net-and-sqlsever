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
using POS.Report;

namespace POS.Purchase
{
    public partial class frm_PurchaseList : Form
    {
       

        clsPurchase obj_clsUserSetting = new clsPurchase();
        clsPurchaseDetail obj_clsPurchaseDetail= new clsPurchaseDetail();
        clsMainDB obj_clsMainDB = new clsMainDB();
        UserControl PurchaseDetail;

        string SPString="";

        private void ShowPurchase()
        {
            DataGridViewTextBoxColumn DGCol = new DataGridViewTextBoxColumn();
            DGCol.DefaultCellStyle.NullValue = "+";
            DGCol.HeaderText = "";
            DGCol.Width = 30;
            DGCol.ReadOnly = true;
            DGCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPurchase.Columns.Add(DGCol);

            SPString = string.Format("SP_Select_Purchase N'{0}',N'{1}',N'{2}'", "0", "0", "0");

            dgvPurchase.DataSource = obj_clsMainDB.SelectData(SPString);

            dgvPurchase.Columns[1].Width = (dgvPurchase.Width / 100) * 10;
            dgvPurchase.Columns[2].Visible = false;
            dgvPurchase.Columns[3].Width = (dgvPurchase.Width / 100) * 15;
            dgvPurchase.Columns[4].Visible = false;
            dgvPurchase.Columns[5].Width = (dgvPurchase.Width / 100) * 30;
            dgvPurchase.Columns[6].Visible = false;
            dgvPurchase.Columns[7].Width = (dgvPurchase.Width / 100) * 30;
            dgvPurchase.Columns[8].Width = (dgvPurchase.Width / 100) * 15;

            obj_clsMainDB.ToolStripTextBoxData(ref tstSearchWith, SPString, "PurchaseDate");
        }
        public void ShowPurchaseDetail()
        {
            PurchaseDetail = new ctl_PurchaseDetail();
            PurchaseDetail.Visible=false;
            Controls.Add(PurchaseDetail);
            Controls.SetChildIndex(PurchaseDetail,0);
        }
        public frm_PurchaseList()
        {
            InitializeComponent();
        }

        private void frm_PurchaseList_Load(object sender, EventArgs e)
        {
            ShowPurchase();
            ShowPurchaseDetail();
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            if (dgvPurchase.Rows.Count > 0)
            {
                DataTable DT = new DataTable();
                if(tslLabel.Text=="PurchaseDate")
                {
                    SPString = string.Format("SP_Select_PurchaseReport N'{0}',N'{1}',N'{2}'",tstSearchWith.Text.Trim().ToString(),"0","0");
                }
                else if (tslLabel.Text == "SupplierName")
                {
                    SPString = string.Format("SP_Select_PurchaseReport N'{0}',N'{1}',N'{2}'", tstSearchWith.Text.Trim().ToString(), "0", "1");
                }
                else if (tslLabel.Text == "UserName")
                {
                    SPString = string.Format("SP_Select_PurchaseReport N'{0}',N'{1}',N'{2}'", tstSearchWith.Text.Trim().ToString(), "0", "2");
                }
                DT = obj_clsMainDB.SelectData(SPString);
                frm_Report frmReport = new frm_Report();
                crpt_Purchase crpt = new crpt_Purchase();
                crpt.SetDataSource(DT);
                frmReport.crystalReportViewer1.ReportSource = crpt;
                frmReport.ShowDialog();

                SPString = string.Format("SP_Select_Purchase N'{0}',N'{1}',N'{2}'", "0", "0", "0");
                dgvPurchase.DataSource = obj_clsMainDB.SelectData(SPString);
                tstSearchWith.Text = "";
            }
            else
            {
                MessageBox.Show("There Is No Data");
            }

        }

        private void dgvPurchase_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (dgvPurchase[e.ColumnIndex, e.RowIndex].Value == null)
                    dgvPurchase[e.ColumnIndex, e.RowIndex].Value ="+";

                if (dgvPurchase[e.ColumnIndex, e.RowIndex].Value.ToString().Trim() == "+")
                {
                    Rectangle cellBounds = dgvPurchase.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                    Point offsetLocation = new Point(cellBounds.X, cellBounds.Y + cellBounds.Height);
                    offsetLocation.Offset(dgvPurchase.Location);
                    PurchaseDetail.Location = offsetLocation;
                    int PurchaseID = (Convert.ToInt32(dgvPurchase.CurrentRow.Cells["PurchaseID"].Value.ToString()));

                    DataGridView DGV = ((POS.Purchase.ctl_PurchaseDetail)(PurchaseDetail)).dgvPurchaseDetail;
                    SPString = string.Format("SP_Select_PurchaseDetail N'{0}',N'{1}',N'{2}'", PurchaseID, "0", "0");
                    DGV.DataSource = obj_clsMainDB.SelectData(SPString);
                    DGV.Columns[0].Visible = false;
                    DGV.Columns[1].Visible = false;
                    DGV.Columns[2].Width = (DGV.Width / 100) * 50;
                    DGV.Columns[3].Width = (DGV.Width / 100) * 20;
                    DGV.Columns[4].Width = (DGV.Width / 100) * 20;

                    PurchaseDetail.Show();
                    dgvPurchase[e.ColumnIndex, e.RowIndex].Value = "-";
                }
                else
                {
                    PurchaseDetail.Hide();
                    dgvPurchase[e.ColumnIndex, e.RowIndex].Value = "+";
                }
            }
                
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            frm_Purchase frm = new frm_Purchase();
            frm.ShowDialog();
            SPString = string.Format("SP_Select_Purchase N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            dgvPurchase.DataSource = obj_clsMainDB.SelectData(SPString);
        }

        private void tsmPurchaseDate_Click(object sender, EventArgs e)
        {
            tslLabel.Text = "PurchaseDate";
            SPString = string.Format("SP_Select_Purchase N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            obj_clsMainDB.ToolStripTextBoxData(ref tstSearchWith, SPString, "PurchaseDate");
        }

        private void tsmSupplierName_Click(object sender, EventArgs e)
        {
            tslLabel.Text = "SupplierName";
            SPString = string.Format("SP_Select_Purchase N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            obj_clsMainDB.ToolStripTextBoxData(ref tstSearchWith, SPString, "SupplierName");
        }

        private void tsmUserName_Click(object sender, EventArgs e)
        {
            tslLabel.Text = "UserName";
            SPString = string.Format("SP_Select_Purchase N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            obj_clsMainDB.ToolStripTextBoxData(ref tstSearchWith, SPString, "UserName");
        }

        private void tstSearchWith_TextChanged(object sender, EventArgs e)
        {
            if(tslLabel.Text=="PurchaseDate")
            {
                SPString = string.Format("SP_Select_Purchase N'{0}',N'{1}',N'{2}'",
                tstSearchWith.Text.Trim().ToString(), "0", "3");
            }
            else if(tslLabel.Text=="SupplierName")
            {
                SPString = string.Format("SP_Select_Purchase N'{0}',N'{1}',N'{2}'",
                tstSearchWith.Text.Trim().ToString(), "0", "4");
            }
            else if(tslLabel.Text=="UserName")
            {
                SPString = string.Format("SP_Select_Purchase N'{0}',N'{1}',N'{2}'",
                tstSearchWith.Text.Trim().ToString(), "0", "5");
            }
            dgvPurchase.DataSource = obj_clsMainDB.SelectData(SPString);

        }
    }
}
