using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using POS.DBA;

namespace POS.DBA
{
    class clsMainDB
    {
        public SqlConnection con;
        DataSet DS = new DataSet();

        public void DatatBaseConn()
        {
            try
            {
                con = new SqlConnection(POS.Properties.Settings.Default.POSCon);
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(),"Error In DataBaseConn");
            }
        }

        public DataTable SelectData(string SPString)
        {
            DataTable DT = new DataTable();
            try
            {
                DatatBaseConn();
                SqlDataAdapter Adpt = new SqlDataAdapter(SPString, con);
                Adpt.Fill(DT);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error In SelectData");
            }
            finally
            {
                con.Close();
            }
            return DT;
        }
        public void ToolStripTextBoxData(ref ToolStripTextBox tstToolStrip,string SPString,string FieldName)
        {
            DataTable DT=new DataTable();
            AutoCompleteStringCollection Source=new AutoCompleteStringCollection();

            try
            {
                DatatBaseConn();
                SqlDataAdapter Adpt = new SqlDataAdapter(SPString, con);
                Adpt.Fill(DT);
                if (DT.Rows.Count > 0)
                {
                    tstToolStrip.AutoCompleteCustomSource.Clear();
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        Source.Add(DT.Rows[i][FieldName].ToString());
                    }
                    tstToolStrip.AutoCompleteCustomSource = Source;
                    tstToolStrip.Text = "";
                    tstToolStrip.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error In TooStripTextBoxData");
            }
            finally
            {
                con.Close();
            }
                
        }
        public void AddCombo(ref ComboBox cboCombo,string SPString,string Display,string Value)
        {
            DataTable DT=new DataTable();
            DataTable DTCombo=new DataTable();
            DataRow Dr;

            DTCombo.Columns.Add(Display);
            DTCombo.Columns.Add(Value);

            Dr = DTCombo.NewRow();
            Dr[Display] = "---Select---";
            Dr[Value] = 0;
            DTCombo.Rows.Add(Dr);


            try
            {
                DatatBaseConn();
                SqlDataAdapter Adpt = new SqlDataAdapter(SPString, con);
                Adpt.Fill(DT);
                for (int i=0;i<DT.Rows.Count;i++)
                {
                   
                   Dr=DTCombo.NewRow();
                   Dr[Display] = DT.Rows[i][Display];
                   Dr[Value] = DT.Rows[i][Value];
                   DTCombo.Rows.Add(Dr);
                }
                cboCombo.DisplayMember=Display;
                cboCombo.ValueMember=Value;
                cboCombo.DataSource=DTCombo;


                    /* tstToolStrip.AutoCompleteCustomSource.Clear();
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        Source.Add(DT.Rows[i][FieldName].ToString());
                    }
                    tstToolStrip.AutoCompleteCustomSource = Source;
                    tstToolStrip.Text = "";
                    tstToolStrip.Focus();*/
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error In TooStripTextBoxData");
            }
            finally
            {
                con.Close();
            }
        }
        public void TextBoxData(ref TextBox txtTextBox, string SPString, string FieldName)
        {
            DataTable DT = new DataTable();
            AutoCompleteStringCollection Source = new AutoCompleteStringCollection();

            txtTextBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;

            try
            {
                DatatBaseConn();
                SqlDataAdapter Adpt = new SqlDataAdapter(SPString, con);
                Adpt.Fill(DT);
                if (DT.Rows.Count > 0)
                {
                    txtTextBox.AutoCompleteCustomSource.Clear();
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        Source.Add(DT.Rows[i][FieldName].ToString());
                    }
                    txtTextBox.AutoCompleteCustomSource = Source;
                    txtTextBox.Text = "";
                    txtTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error In TextBoxData");
            }
            finally
            {
                con.Close();
            }

        }
        public string GetVoucher(string SPString, string Date)
        {
            DataTable DT = new DataTable();
            try
            {
                DatatBaseConn();
                SqlDataAdapter Adpt = new SqlDataAdapter(SPString, con);
                Adpt.Fill(DT);
                if(DT.Rows.Count > 0 && DT.Rows[0]["Voucher"].ToString() != string.Empty)
                {
                    int No = Convert.ToInt32(DT.Rows[0]["Voucher"]) + 1;
                    return (string.Format("S{0:D3}", No) + "-" + Date);
                }
                else
                {
                    return ("S001-" + Date);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error In GetVoucher");
                return null;
            }
            finally
            {
                con.Close();
            }

 
        }

    }
}


