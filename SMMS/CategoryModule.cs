using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMMS
{
    public partial class CategoryModule : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        Category category;

        public  CategoryModule(Category ct)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            category = ct;
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // To insert category name to category table
            try
            {
                if (MessageBox.Show("Are you sure want to save this category?", "Save Category", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cmd = new SqlCommand("INSERT INTO tbCategory(category)VALUES(@category)", cn);
                    cmd.Parameters.AddWithValue("@category", txtCategory.Text);
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record has been successfully saved.", "SuperMarket Management");
                    Clear();
                    category.LoadCategory(); // Refresh the category list in the main form
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Clear()
        {
            txtCategory.Clear();
            txtCategory.Focus();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Update category name 
            if (MessageBox.Show("Are you sure want to update this category?", "Update Category", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cn.Open();
                cmd = new SqlCommand("UPDATE tbCategory SET category = @category WHERE id LIKE'" + lblid.Text + "'", cn);
                cmd.Parameters.AddWithValue("@category", txtCategory.Text);
                cmd.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Record has been successfully updated.", "SuperMarket Management");
                Clear();
                this.Dispose(); // Close the module form after update
            }
        }
    }
}
