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
    public partial class ProductModule : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        //string stitle = "Product Module";
        Product product; // Reference to the Product form

        public ProductModule(Product pd)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            product = pd; // Initialize the product reference
            LoadCategory(); // Load categories when the form is initialized
        }

        private void ProductModule_Load(object sender, EventArgs e)
        {

        }

        public void LoadCategory()
        {
            cboCategory.Items.Clear();
            cboCategory.DataSource = dbcon.GetTable("SELECT * FROM tbCategory");
            cboCategory.DisplayMember = "category"; // Display the category name
            cboCategory.ValueMember = "id"; // Use the ID as the value
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void Clear()
        {
            txtPCode.Clear();
            txtBarCode.Clear();
            txtItem.Clear();
            cboCategory.SelectedIndex = 0; // Reset the category selection
            txtPrice.Clear();
            UDReOrder.Value = 1; // Reset the reorder value

            txtPCode.Focus(); // Set focus back to the product code field
            txtPCode.Enabled = true; // Enable the product code field for new entries
            btnSave.Enabled = true; // Enable the save button
            btnUpdate.Enabled = false; // Disable the update button
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("Are you sure want to save this product?", "Save Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {       
                    cmd = new SqlCommand("INSERT INTO tbProduct(pcode, barcode, item, cid, price, reorder) VALUES(@pcode, @barcode, @item, @cid, @price, @reorder)", cn);
                    cmd.Parameters.AddWithValue("@pcode", txtPCode.Text);
                    cmd.Parameters.AddWithValue("@barcode", txtBarCode.Text);
                    cmd.Parameters.AddWithValue("@item", txtItem.Text);
                    cmd.Parameters.AddWithValue("@cid", cboCategory.SelectedValue);
                    cmd.Parameters.AddWithValue("@price", double.Parse(txtPrice.Text));
                    cmd.Parameters.AddWithValue("@reorder", UDReOrder.Value);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Product has been successfully saved.", "SuperMarket Management");
                    Clear();
                    product.LoadProduct();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
