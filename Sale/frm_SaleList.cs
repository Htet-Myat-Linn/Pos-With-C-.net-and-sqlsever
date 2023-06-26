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


namespace POS.Sale
{
    public partial class frm_SaleList : Form
    {
        clsSale obj_clsSale = new clsSale();
        clsSaleDetail obj_clsSaleDetail = new clsSaleDetail();
        clsMainDB obj_clsMainDB = new clsMainDB();

        UserControl SaleDetail;

        string SPString = "";
        public frm_SaleList()
        {
            InitializeComponent();
        }
        private void ShowSale()
        {
            DataGridViewTextBoxColumn DGCol = new DataGridViewTextBoxColumn();
            DGCol.DefaultCellStyle.NullValue = "+";
            DGCol.HeaderText = "";
            DGCol.Width = 30;
            DGCol.ReadOnly = true;
            DGCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSale.Columns.Add(DGCol);

            SPString = string.Format("SP_Select_Sale N'{0}',N'{1}',N'{2}'", "0", "0", "0");

            dgvSale.DataSource = obj_clsMainDB.SelectData(SPString);

            dgvSale.Columns[1].Width = (dgvSale.Width / 100) * 10;
            dgvSale.Columns[2].Visible = false;
            dgvSale.Columns[3].Width = (dgvSale.Width / 100) * 20;
            dgvSale.Columns[4].Width = (dgvSale.Width / 100) * 20;
            dgvSale.Columns[5].Visible = false;
            dgvSale.Columns[6].Width = (dgvSale.Width / 100) * 20;
            dgvSale.Columns[7].Width = (dgvSale.Width / 100) * 10;
            dgvSale.Columns[8].Width = (dgvSale.Width / 100) * 10;
            dgvSale.Columns[9].Width = (dgvSale.Width / 100) * 10;

            obj_clsMainDB.ToolStripTextBoxData(ref tstSearchWith, SPString, "SaleDate");
        }
        public void ShowSaleDetail()
        {
            SaleDetail = new ctl_SaleDetail();
            SaleDetail.Hide();
            Controls.Add(SaleDetail);
            Controls.SetChildIndex(SaleDetail, 0);
        }

        private void frm_SaleList_Load(object sender, EventArgs e)
        {
            ShowSale();
            ShowSaleDetail();
        }

        private void dgvSale_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (dgvSale[e.ColumnIndex, e.RowIndex].Value == null)
                    dgvSale[e.ColumnIndex, e.RowIndex].Value = "+";

                if (dgvSale[e.ColumnIndex, e.RowIndex].Value.ToString().Trim() == "+")
                {
                    Rectangle cellBounds = dgvSale.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                    Point offsetLocation = new Point(cellBounds.X, cellBounds.Y + cellBounds.Height);
                    offsetLocation.Offset(dgvSale.Location);
                    SaleDetail.Location = offsetLocation;

                    int SaleID = (Convert.ToInt32(dgvSale.CurrentRow.Cells["SaleID"].Value.ToString()));

                    DataGridView DGV = ((POS.Sale.ctl_SaleDetail)(SaleDetail)).dgvSaleDetail;
                    SPString = string.Format("SP_Select_SaleDetail N'{0}',N'{1}',N'{2}'", SaleID, "0", "0");
                    DGV.DataSource = obj_clsMainDB.SelectData(SPString);
                    DGV.Columns[0].Visible = false;
                    DGV.Columns[1].Visible = false;
                    DGV.Columns[2].Width = (DGV.Width / 100) * 50;
                    DGV.Columns[3].Width = (DGV.Width / 100) * 20;
                    DGV.Columns[4].Width = (DGV.Width / 100) * 20;

                    SaleDetail.Show();
                    dgvSale[e.ColumnIndex, e.RowIndex].Value = "-";
                }
                else
                {
                    SaleDetail.Hide();
                    dgvSale[e.ColumnIndex, e.RowIndex].Value = "+";
                }
            }
        }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            frm_Sale frm = new frm_Sale();
            frm.ShowDialog();
            SPString = string.Format("SP_Select_Sale N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            dgvSale.DataSource = obj_clsMainDB.SelectData(SPString);
        }

        private void tsmDate_Click(object sender, EventArgs e)
        {
            tslLabel.Text = "Date";
            SPString = string.Format("SP_Select_Sale N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            obj_clsMainDB.ToolStripTextBoxData(ref tstSearchWith, SPString, "SaleDate");
        }

        private void tsmUserName_Click(object sender, EventArgs e)
        {
            tslLabel.Text = "UserName";
            SPString = string.Format("SP_Select_Sale N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            obj_clsMainDB.ToolStripTextBoxData(ref tstSearchWith, SPString, "UserName");
        }

        private void tstSearchWith_TextChanged(object sender, EventArgs e)
        {
            if (tslLabel.Text == "SaleDate")
            {
                SPString = string.Format("SP_Select_Sale N'{0}',N'{1}',N'{2}'",
                tstSearchWith.Text.Trim().ToString(), "0", "4");
            }
            else if (tslLabel.Text == "UserName")
            {
                SPString = string.Format("SP_Select_Sale N'{0}',N'{1}',N'{2}'",
                tstSearchWith.Text.Trim().ToString(), "0", "5");
            }
            dgvSale.DataSource = obj_clsMainDB.SelectData(SPString);
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            if (dgvSale.Rows.Count > 0)
            {
                DataTable DT = new DataTable();
                if (tslLabel.Text == "SaleDate")
                {
                    SPString = string.Format("SP_Select_SaleReport N'{0}',N'{1}',N'{2}'", tstSearchWith.Text.Trim().ToString(), "0", "1");
                }
                else if (tslLabel.Text == "UserName")
                {
                    SPString = string.Format("SP_Select_SaleReport N'{0}',N'{1}',N'{2}'", tstSearchWith.Text.Trim().ToString(), "0", "2");
                }
                DT = obj_clsMainDB.SelectData(SPString);
                frm_Report frmReport = new frm_Report();
                crpt_Sale crpt = new crpt_Sale();
                crpt.SetDataSource(DT);
                frmReport.crystalReportViewer1.ReportSource = crpt;
                frmReport.ShowDialog();

                SPString = string.Format("SP_Select_Sale N'{0}',N'{1}',N'{2}'", "0", "0", "0");
                dgvSale.DataSource = obj_clsMainDB.SelectData(SPString);
                tstSearchWith.Text = "";
            }
            else
            {
                MessageBox.Show("There Is No Data");
            }
        }
    }
}
