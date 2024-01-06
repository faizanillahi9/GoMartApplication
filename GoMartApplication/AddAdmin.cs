﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoMartApplication
{
    public partial class AddAdmin : Form
    {
        DBConnect dbCon = new DBConnect();

        public AddAdmin()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAdminName.Text == String.Empty || txtAdminID.Text == String.Empty || txtPass.Text == String.Empty)
                {
                    MessageBox.Show("Please enter valid Admin Name , Admin User Id and Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    clrbtn();
                }
                else
                {
                    // chk duplicte records
                    SqlCommand cmd = new SqlCommand("select AdminId from tblAdmin where AdminId=@Id", dbCon.GetCon());
                    cmd.Parameters.AddWithValue("@Id", txtAdminID.Text);
                    dbCon.OpenCon();
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        MessageBox.Show("Admin Id already exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        clrbtn();
                    }
                    else
                    {
                        cmd = new SqlCommand("spAddAdmin", dbCon.GetCon());
                        cmd.Parameters.AddWithValue("@AdminId", txtAdminID.Text);
                        cmd.Parameters.AddWithValue("@Password", txtPass.Text);
                        cmd.Parameters.AddWithValue("@FullName", txtAdminName.Text);
                        cmd.CommandType = CommandType.StoredProcedure;
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Admin Inserted Sucessfully...", "Sucess");
                            clrbtn();
                            BindAdmin();
                        }
                    }
                    dbCon.CloseCon();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);    
            }


          
        }

        private void BindAdmin()
        {

            SqlCommand cmd = new SqlCommand("select * from tblAdmin", dbCon.GetCon());
            dbCon.OpenCon();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dbCon.CloseCon();
        }

        private void AddAdmin_Load(object sender, EventArgs e)
        {
            lblAdminID.Visible = false;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            btnAdd.Visible = true;
            BindAdmin();
        }
        private void clrbtn()
        {
            txtAdminName.Clear();
            txtAdminID.Clear();
            txtPass.Clear();
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAdminName.Text == String.Empty || txtAdminID.Text == String.Empty || txtPass.Text == String.Empty || lblAdminID.Text == String.Empty)
                {
                    MessageBox.Show("Please enter valid Admin Name , Admin User Id and Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    clrbtn();
                }
                else
                {
                   
                    SqlCommand cmd = new SqlCommand("spUpdateAdmin", dbCon.GetCon());
                    dbCon.OpenCon();
                    cmd.Parameters.AddWithValue("@AdminId", lblAdminID.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPass.Text);
                    cmd.Parameters.AddWithValue("@FullName", txtAdminName.Text);
                    cmd.CommandType = CommandType.StoredProcedure;
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Admin record updated Sucessfully...", "Sucess", MessageBoxButtons.OK);
                        clrbtn();
                        BindAdmin();
                    }
                    dbCon.CloseCon();
                }   

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if(lblAdminID.Text== String.Empty)
                {
                    MessageBox.Show("Please Select Admin ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("spDeleteAdmin", dbCon.GetCon());
                    cmd.Parameters.AddWithValue("@AdminID", lblAdminID.Text);

                    cmd.CommandType = CommandType.StoredProcedure;
                    dbCon.OpenCon();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Seller Deleted Sucessfully...", "Sucess");
                        clrbtn();
                        BindAdmin();
                        btnUpdate.Visible = false;
                        btnDelete.Visible = false;
                        btnAdd.Visible = true;
                        lblAdminID.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show(" Delete Failed...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        clrbtn();
                    }
                    dbCon.CloseCon();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            try
            {
                btnUpdate.Visible = true;
                btnDelete.Visible = true;
                lblAdminID.Visible = true;
                btnAdd.Visible = false;
                lblAdminID.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                txtAdminID.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                txtPass.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                txtAdminName.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
