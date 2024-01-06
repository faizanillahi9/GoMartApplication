using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoMartApplication
{
    public partial class Form1 : Form
    {
         DBConnect dbCon = new DBConnect();
      public  static string loginname, logintype;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //  temp set data for not login bar bar
            cmbRole.SelectedIndex= 1;
            txtUserName.Text = "asd";
            txtPass.Text = "123";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbRole.SelectedIndex > 0) {
                    if (txtUserName.Text == String.Empty)
                    {
                        MessageBox.Show("Please Enter valid UserName", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUserName.Focus();
                        return;
                    }
                   else if (txtPass.Text == String.Empty)
                    {
                        MessageBox.Show("Please Enter valid Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPass.Focus();
                        return;
                    }
                    if (cmbRole.SelectedIndex > 0 && txtUserName.Text != String.Empty && txtPass.Text != String.Empty)
                    {
                        // login code
                        if (cmbRole.Text == "Admin")
                        {
                            SqlCommand cmd = new SqlCommand("select top 1 AdminID,Password,FullName from tblAdmin where AdminID=@AdminID and Password=@Password", dbCon.GetCon());
                            cmd.Parameters.AddWithValue("@AdminID", txtUserName.Text);
                            cmd.Parameters.AddWithValue("@Password", txtPass.Text);
                            dbCon.OpenCon();
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            if (dt.Rows.Count>0)
                            {
                                MessageBox.Show("Login Success Welcome to HomePage", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                loginname = txtUserName.Text;
                                logintype = cmbRole.Text;
                                clrValue();
                                this.Hide();
                                frmMain fm = new frmMain();
                                fm.Show();
                            }
                            else
                            {
                                MessageBox.Show("Invalid Login Please Check UserName and Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }

                        }
                        else if(cmbRole.Text == "Seller")
                        {
                            SqlCommand cmd = new SqlCommand("select top 1 SellerName,SellerPass from tblSeller where SellerName=@SellerName and SellerPass=@SellerPass\r\n", dbCon.GetCon());
                            cmd.Parameters.AddWithValue("@SellerName", txtUserName.Text);
                            cmd.Parameters.AddWithValue("@SellerPass", txtPass.Text);
                            dbCon.OpenCon();
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                MessageBox.Show("Login Success Welcome to HomePage", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                loginname = txtUserName.Text;
                                logintype = cmbRole.Text;
                                clrValue();
                                this.Hide();
                                frmMain fm = new frmMain();
                                fm.Show();
                            }
                            else
                            {
                                MessageBox.Show("Invalid Login Please Check UserName and Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Please Enter User Name or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        clrValue();
                    }
                }
                else
                {
                    MessageBox.Show("Please Select any role","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    clrValue();
                }
            }
            catch( Exception ex) 
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);  
            }
        }
        private void clrValue()
        {
            cmbRole.SelectedIndex = 0;
            txtUserName.Clear();
            txtPass.Clear();
        }
    }
}
