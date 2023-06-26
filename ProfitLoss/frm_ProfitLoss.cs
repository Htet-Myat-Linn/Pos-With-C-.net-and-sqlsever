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

using POS.ProfitLoss;

namespace POS.ProfitLoss
{
    public partial class frm_ProfitLoss : Form
    {
        public frm_ProfitLoss()
        {
            InitializeComponent();

        }
        clsMainDB obj_clsMainDB= new clsMainDB();

        DataTable DTData = new DataTable();
        DataTable DTPurchase = new DataTable();
        DataTable DTSale = new DataTable();
        DataTable DTItem = new DataTable();
        DataTable DT = new DataTable();
        DataRow DRData;

        string SPString = "";
        int GrandPurchaseTotal, GrandSaleTotal, GrandProfitLoss;


        private void ShowData()
        {
            GrandPurchaseTotal = GrandSaleTotal = GrandProfitLoss = 0;

            SPString = string.Format("SP_Select_ProfitLoss N'{0}',N'{1}',N'{2}',N'{3}'",dtpStartDate.Value.ToShortDateString(),dtpEndDate.Value.ToShortDateString(),"0","2");
            DT = obj_clsMainDB.SelectData(SPString);
            int DateDiff = Convert.ToInt32(DT.Rows[0]["No"]);
            if (DateDiff < 0)
            {
                MessageBox.Show("Please Check StartDate And EndDate");
                dtpStartDate.Text = DateTime.Now.ToShortDateString();
                dtpEndDate.Text = DateTime.Now.ToShortDateString();
                return;
            }
            SPString = string.Format("SP_Select_ProfitLoss N'{0}',N'{1}',N'{2}',N'{3}'",txtItemName.Text,dtpStartDate.Value.ToShortDateString(), dtpEndDate.Value.ToShortDateString(), "0");
            DTPurchase = obj_clsMainDB.SelectData(SPString);

            SPString = string.Format("SP_Select_ProfitLoss N'{0}',N'{1}',N'{2}',N'{3}'", txtItemName.Text, dtpStartDate.Value.ToShortDateString(), dtpEndDate.Value.ToShortDateString(), "1");
            DTSale = obj_clsMainDB.SelectData(SPString);

            SPString = string.Format("SP_Select_Item N'{0}',N'{1}',N'{2}'", txtItemName.Text, "0", "2");
            DTItem = obj_clsMainDB.SelectData(SPString);

            DTData.Rows.Clear();
            DTData.Columns.Clear();
            DTData.Columns.Add("No");  
            DTData.Columns.Add("ItemName");
            DTData.Columns.Add("OnHandQty");
            DTData.Columns.Add("PurchaseTotal");
            DTData.Columns.Add("SaleTotal");
            DTData.Columns.Add("Profit/Loss");


            foreach (DataRow DRItem in DTItem.Rows)
            {
                DRData = DTData.NewRow();
                DRData["No"]=DRItem["No"];
                DRData["ItemName"]=DRItem["ItemName"];
                DRData["OnHandQty"]=DRItem["Qty"];

                DataRow[] DRPurchase = DTPurchase.Select("ItemName='"+DRData["ItemName"]+"'");
                if (DRPurchase.Length > 0)  
                    DRData["PurchaseTotal"] = DRPurchase[0]["PurchaseTotal"];
                
                else
                    DRData["PurchaseTotal"] = "0";


                DataRow[] DRSale = DTSale.Select("ItemName='" + DRData["ItemName"] + "'");
                if (DRSale.Length > 0)
                    DRData["SaleTotal"] = DRSale[0]["SaleTotal"];
                
                else
                    DRData["SaleTotal"] = "0";

                int PurchaseTotal = Convert.ToInt32(DRData["PurchaseTotal"]);
                int SaleTotal = Convert.ToInt32(DRData["SaleTotal"]);
                int Profitloss = SaleTotal - PurchaseTotal;


                GrandPurchaseTotal += PurchaseTotal;
                GrandSaleTotal += SaleTotal;
                GrandProfitLoss += Profitloss;

                DRData["Profit/Loss"] = Profitloss.ToString();

                DTData.Rows.Add(DRData);
            }

            DRData = DTData.NewRow();
            DRData["OnHandQty"] = "Grand Total";
            DRData["PurchaseTotal"] = GrandPurchaseTotal.ToString();
            DRData["SaleTotal"] = GrandSaleTotal.ToString();
            DRData["Profit/Loss"] = GrandProfitLoss.ToString();
            DTData.Rows.Add(DRData);

            dgvProfitLoss.DataSource = DTData;

            dgvProfitLoss.Columns[0].Width=(dgvProfitLoss.Width/100)*10;
            dgvProfitLoss.Columns[1].Width=(dgvProfitLoss.Width/100)*30;
            dgvProfitLoss.Columns[2].Width=(dgvProfitLoss.Width/100)*10;
            dgvProfitLoss.Columns[3].Width=(dgvProfitLoss.Width/100)*10;
            dgvProfitLoss.Columns[4].Width=(dgvProfitLoss.Width/100)*10;
            dgvProfitLoss.Columns[5].Width=(dgvProfitLoss.Width/100)*10;


            int lastIndex = dgvProfitLoss.Rows.Count - 1;
            dgvProfitLoss.Rows[lastIndex].DefaultCellStyle.BackColor = Color.Yellow;

        }

        private void frm_ProfitLoss_Load(object sender, EventArgs e)
        {
            ShowData();

            SPString = string.Format("SP_Select_Item N'{0}',N'{1}',N'{2}'","0","0","0");
            obj_clsMainDB.TextBoxData(ref txtItemName,SPString,"ItemName");
        }

        private void txtItemName_TextChanged(object sender, EventArgs e)
        {
            ShowData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ShowData();
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            txtItemName.Text = "";
            dtpStartDate.Text = DateTime.Now.ToShortDateString();
            dtpEndDate.Text = DateTime.Now.ToShortDateString();
            ShowData();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
           
        }
    }
}
