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
    public partial class frm_UserSetting : Form
    {
        clsUserSetting obj_clsUserSetting = new clsUserSetting();
        clsMainDB obj_clsMainDB = new clsMainDB();
        frmMain obj_frmMain = new frmMain();
        DataTable DT = new DataTable();
        public bool _IsEdit = false;
        string SPString = "";
        public string UserLevel = "";
        public int UserID = 0;

        public frm_UserSetting()
        {
            InitializeComponent();
        }

        private void frm_UserSetting_Load(object sender, EventArgs e)
        {
            ShowUserLevel();
            string Day = string.Format("{0:D2}", DateTime.Now.Day);
            string Month = string.Format("{0:D2}", DateTime.Now.Month);
            string Year = string.Format("{0:D2}", DateTime.Now.Year);
            lblUpdateDate.Text = Month + "/" + Day + "/" + Year;
            txtUserName.Focus();
        }
        private void ShowUserLevel()
        {
            chkUserLevel.Items.Clear();
            for (int i = 1; i < obj_frmMain.menuStrip1.Items.Count; i++)
            {
                ToolStripMenuItem mnuMain = (ToolStripMenuItem)obj_frmMain.menuStrip1.Items[i];
                foreach (ToolStripItem mnuSub in mnuMain.DropDownItems)
                {
                    chkUserLevel.Items.Add(mnuSub.Text.ToString());
                }
            }
            if (_IsEdit)
            {
                string[] Arr_UserLevel = UserLevel.Split(',');
                for (int i = 0; i < chkUserLevel.Items.Count; i++)
                {
                    for (int j = 0; j < Arr_UserLevel.Length; j++)
                    {
                        if (chkUserLevel.Items[i].ToString() == Arr_UserLevel[j].ToString())
                        {
                            chkUserLevel.SetItemChecked(i, true);
                        }
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UserLevel = string.Empty;
            foreach (object itemChecked in chkUserLevel.CheckedItems)
            {
                UserLevel = UserLevel + itemChecked.ToString() + ",";
            }
            if (txtUserName.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type UserName");
                txtUserName.Focus();
            }
            else if (txtPassword.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type Password");
                txtPassword.Focus();
            }
            else if (txtConfirmPassword.Text.Trim().ToString() == string.Empty)
            {
                MessageBox.Show("Please Type ConfirmPassword");
                txtConfirmPassword.Focus();
            }
            else if (txtPassword.Text.Trim().ToString() != txtConfirmPassword.Text.Trim().ToString())
            {
                MessageBox.Show("Password And Confirm Password Should Be Same");
                txtConfirmPassword.Focus();
                txtConfirmPassword.SelectAll();
            }
            else if (UserLevel.ToString() == string.Empty)
            {
                MessageBox.Show("Please Choose User Level");
            }
            else
            {
                SPString = string.Format("SP_Select_UserSetting N'{0}',N'{1}',N'{2}'",
                txtUserName.Text.Trim().ToString(), txtPassword.Text.Trim().ToString(), "1");
                DT = obj_clsMainDB.SelectData(SPString);
                if (DT.Rows.Count > 0 && UserID != Convert.ToInt32(DT.Rows[0]["UserID"].ToString()))
                {
                    MessageBox.Show("This User Is Already Exist");
                    txtUserName.Focus();
                    txtUserName.SelectAll();
                }
                else
                {
                    obj_clsUserSetting.UNAME = txtUserName.Text.Trim().ToString();
                    obj_clsUserSetting.PASS = txtPassword.Text.Trim().ToString();
                    obj_clsUserSetting.USERLEVEL = UserLevel;
                    obj_clsUserSetting.UPDATE = lblUpdateDate.Text;
                    if (_IsEdit)
                    {
                        obj_clsUserSetting.USERID = UserID;
                        obj_clsUserSetting.ACTION = 1;
                        obj_clsUserSetting.SaveData();
                        MessageBox.Show("Successfully Edit", "Successfully", MessageBoxButtons.OK);
                        this.Close();
                    }
                    else
                    {
                        obj_clsUserSetting.ACTION = 0;
                        obj_clsUserSetting.SaveData();
                        MessageBox.Show("Successfully Save", "Successfully", MessageBoxButtons.OK);
                        this.Close();
                    }
                }

            }


        }

    }
}
