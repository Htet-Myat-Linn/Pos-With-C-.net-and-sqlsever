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

namespace POS.MasterData
{
    public partial class frm_SupplierList : Form
    {   
        clsSupplier obj_clsSupplier = new clsSupplier();
        clsMainDB obj_clsMainDB = new clsMainDB();
        string SPString = "";

        public frm_SupplierList()
        {
            InitializeComponent();
        }

        private void frm_SupplierList_Load(object sender, EventArgs e)
        {
            ShowData();
        }
        private void ShowData()
        {
            SPString = string.Format("SP_Select_Supplier N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            dgvSupplier.DataSource = obj_clsMainDB.SelectData(SPString);
            dgvSupplier.Columns[0].Width = (dgvSupplier.Width / 100) * 10;
            dgvSupplier.Columns[1].Visible = false;
            dgvSupplier.Columns[2].Width = (dgvSupplier.Width / 100) * 20;
            dgvSupplier.Columns[3].Width = (dgvSupplier.Width / 100) * 30;
            dgvSupplier.Columns[4].Width = (dgvSupplier.Width / 100) * 20;
            dgvSupplier.Columns[5].Width = (dgvSupplier.Width / 100) * 20;
            obj_clsMainDB.ToolStripTextBoxData(ref tstSearchWith, SPString, "SupplierName");
            tslLabel.Text = "SupplierName";
        }
        private void ShowEntry()
        {
            if (dgvSupplier.CurrentRow.Cells[0].Value.ToString() == string.Empty)
                {
                    MessageBox.Show("There Is No Data");
                }
            else
                {
                    frm_Supplier frm = new frm_Supplier();
                    frm._SupplierID = Convert.ToInt32(dgvSupplier. CurrentRow.Cells["SupplierID"].Value.ToString());
                    frm.txtName.Text = dgvSupplier.CurrentRow.Cells["SupplierName"].Value.ToString();
                    frm.txtAddress.Text = dgvSupplier.CurrentRow.Cells["Address"].Value.ToString();
                    frm.txtPhone.Text = dgvSupplier.CurrentRow.Cells["Phone"].Value.ToString();
                    frm.lblUpdateDate.Text = dgvSupplier.CurrentRow.Cells["UpdateDate"].Value.ToString();
                    frm._IsEdit = true;
                    frm.ShowDialog();
                    ShowData();
                }
            }

        private void tsbNew_Click(object sender, EventArgs e)
        {
            frm_Supplier frm = new frm_Supplier();
            frm.ShowDialog();
            ShowData();
        }

        private void dgvSupplier_DoubleClick(object sender, EventArgs e)
        {
            ShowEntry();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            ShowEntry();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (dgvSupplier.CurrentRow.Cells[0].Value.ToString() == string.Empty)
            {
                MessageBox.Show("There Is No Data");
            }
            else
            {
                if (MessageBox.Show("Are You Sure You Want To Delete?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    obj_clsSupplier.SID = Convert.ToInt32 (dgvSupplier.CurrentRow.Cells["SupplierID"].Value.ToString());
                    obj_clsSupplier.ACTION = 2;
                    obj_clsSupplier.SaveData();
                    MessageBox.Show("Successfully Delete");
                    ShowData();
                }
            }
        }

        private void tsmName_Click(object sender, EventArgs e)
        {
            tslLabel.Text = "SupplierName";
            SPString = string.Format("SP_Select_Supplier N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            obj_clsMainDB.ToolStripTextBoxData(ref tstSearchWith, SPString, "SupplierName");
        }

        private void tsmPhone_Click(object sender, EventArgs e)
        {
            tslLabel.Text = "Phone";
            SPString = string.Format("SP_Select_Supplier N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            obj_clsMainDB.ToolStripTextBoxData(ref tstSearchWith, SPString, "Phone");
        }

        private void tsmAdddress_Click(object sender, EventArgs e)
        {
            tslLabel.Text = "Address";
            SPString = string.Format("SP_Select_Supplier N'{0}',N'{1}',N'{2}'", "0", "0", "0");
            obj_clsMainDB.ToolStripTextBoxData(ref tstSearchWith, SPString, "Address");
        }

        private void tstSearchWith_TextChanged(object sender, EventArgs e)
        {
                if (tslLabel.Text == "SupplierName")
                {
                SPString = string.Format("SP_Select_Supplier N'{0}',N'{1}',N'{2}'", 
                tstSearchWith.Text.Trim().ToString(), "0", "2");
                }
                else if (tslLabel.Text == "Phone")
                {
                SPString = string.Format("SP_Select_Supplier N'{0}',N'{1}',N'{2}'", 
                tstSearchWith.Text.Trim().ToString(), "0", "3");
                }
                else if (tslLabel.Text == "Address")
                {
                SPString = string.Format("SP_Select_Supplier N'{0}',N'{1}',N'{2}'", 
                tstSearchWith.Text.Trim().ToString(), "0", "4");
                }
                dgvSupplier.DataSource = obj_clsMainDB.SelectData(SPString);
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            if (dgvSupplier.Rows.Count> 0)
            { 
                DataTable DT = new DataTable();
                DT = obj_clsMainDB.SelectData(SPString);
                frm_Report frmReport = new frm_Report();
                crpt_Supplier crpt = new crpt_Supplier();
                crpt.SetDataSource(DT);
                frmReport.crystalReportViewer1.ReportSource = crpt;
                frmReport.ShowDialog();
                ShowData();
            }
            else
            {
                MessageBox.Show("There Is No Data");
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

    }
}
