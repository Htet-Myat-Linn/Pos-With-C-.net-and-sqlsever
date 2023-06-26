using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using POS.MasterData;
using POS.Purchase;

using POS.DBA;
using POS.Sale;
using POS.ProfitLoss;


namespace POS
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuItem_Click(object sender, EventArgs e)
        {
            frm_ItemList frm = new frm_ItemList();
            frm.ShowDialog();
        }

        private void mnuSupplier_Click(object sender, EventArgs e)
        {
            frm_SupplierList frm = new frm_SupplierList();
            frm.ShowDialog();
        }
        public void ShowMenu(string UserLevel)
        {
            string[] Arr_UserLevel = UserLevel.Split(',');
            for (int i = 1; i < menuStrip1.Items.Count; i++)
            {
                ToolStripMenuItem MainMenu = (ToolStripMenuItem)menuStrip1.Items[i];
                foreach (ToolStripItem SubMenu in MainMenu.DropDownItems)
                {
                    SubMenu.Enabled = false;
                    foreach (string Menu in Arr_UserLevel)
                    {
                        if (SubMenu.Text.ToString() == Menu.ToString())
                        {
                            SubMenu.Enabled = true;
                            break;
                        }
                    }
                }

            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            ShowMenu("");
        }

      /*  private void munLogIn_Click(object sender, EventArgs e)
        {
            
        }*/

        private void mnuLogIn_Click(object sender, EventArgs e)
        {
            if (mnuLogIn.Text == "LogOut")
            {
                if (MessageBox.Show("Are You Sure You Want To Logout", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    mnuLogIn.Text = "LogIn";
                    ShowMenu("");
                }
                return;
            }
            clsMainDB obj_clsMainDB = new clsMainDB();
            frm_LogIn obj_frmLogIn = new frm_LogIn();
            DataTable DT = new DataTable();
            String UserName = "";
            String Password = "";

            Start:
            obj_frmLogIn.txtUserName.Text = UserName;
            obj_frmLogIn.txtPassword.Text = Password;
            if (obj_frmLogIn.ShowDialog() == DialogResult.OK)
            {
                if (obj_frmLogIn.txtUserName.Text.Trim().ToString() == string.Empty)
                {
                    MessageBox.Show("Please Type User Name");
                    obj_frmLogIn.txtUserName.Focus();
                    goto Start;
                }
                UserName = obj_frmLogIn.txtUserName.Text;

                if (obj_frmLogIn.txtPassword.Text.Trim().ToString() == string.Empty)
                {
                    MessageBox.Show("Please Type Password");
                    obj_frmLogIn.txtPassword.Focus();
                    goto Start;
                }
                Password = obj_frmLogIn.txtPassword.Text;

                string SPString = string.Format("SP_Select_UserSetting N'{0}',N'{1}',N'{2}'", obj_frmLogIn.txtUserName.Text.Trim().ToString(), obj_frmLogIn.txtPassword.Text.Trim().ToString(), "1");

                DT = obj_clsMainDB.SelectData(SPString);
                if (DT.Rows.Count > 0)
                {
                    Program.UserID = Convert.ToInt32(DT.Rows[0]["UserID"].ToString());
                    string UserLevel = DT.Rows[0]["UserLevel"].ToString();
                    mnuLogIn.Text = "LogOut";
                    ShowMenu(UserLevel);
                    MessageBox.Show("Successful", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Invalid UserName And Password");
                    goto Start;
                }

            }
        }

        private void mnuPurchase_Click(object sender, EventArgs e)
        {
            frm_PurchaseList frm = new frm_PurchaseList();
            frm.ShowDialog();
        }

        

        private void mnuSale_Click(object sender, EventArgs e)
        {
            frm_SaleList frm = new frm_SaleList();
            frm.ShowDialog();
            
        }

        private void mnuProfitLoss_Click(object sender, EventArgs e)
        {
            frm_ProfitLoss frm = new frm_ProfitLoss();
            frm.ShowDialog();
        }

        
    }
    
}

