using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SMMS
{
    public partial class LookUpProduct : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        Cashier cashier;

        public LookUpProduct(Cashier cash)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            cashier = cash;
            LoadProduct(); // Load products when the form is initialized
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cn.Open();
            cmd = new SqlCommand("SELECT p.pcode, p.barcode, p.item, c.category, p.price, p.qty FROM tbProduct AS p INNER JOIN tbCategory AS c on c.id = p.cid WHERE CONCAT(p.item, c.category) LIKE '%" + txtSearch.Text + "%'", cn);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProduct.Columns[e.ColumnIndex].Name;
            if (colName == "Select")
            {
                Qty qty = new Qty(cashier);
                qty.ProductDetails(dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString(), double.Parse(dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString()), cashier.lblTransNo.Text, int.Parse(dgvProduct.Rows[e.RowIndex].Cells[6].Value.ToString()));
                qty.ShowDialog(); // Show the Qty form to enter quantity
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProduct(); // Reload products when the search text changes
        }

        private void LookUpProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose(); // Close the form when Escape key is pressed
            }
        }
    }
}
