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
    public partial class Cashier : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;

        public Cashier()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            GetTransNo(); // Get the transaction number when the form is initialized
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to close the application?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit(); // Close the application if user confirms
            }
        }

        public void slide(Button button)
        {
            panelSlide.BackColor = Color.White;
            panelSlide.Height = button.Height;
            panelSlide.Top = button.Top;
        }
        #region buttons
        private void btnNTran_Click(object sender, EventArgs e)
        {
            slide(btnNTran); // Slide the panel to the New Transaction button
            GetTransNo();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            slide(btnSearch); // Slide the panel to the Search button
            LookUpProduct lookUp = new LookUpProduct(this); // Open the product lookup form
            lookUp.LoadProduct(); // Load products into the lookup form
            lookUp.ShowDialog(); // Show the lookup form as a dialog
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            slide(btnDiscount); // Slide the panel to the Discount button
        }

        private void btnSettle_Click(object sender, EventArgs e)
        {
            slide(btnSettle); // Slide the panel to the Settle button
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            slide(btnClear); // Slide the panel to the Clear button
        }

        private void btnDSales_Click(object sender, EventArgs e)
        {
            slide(btnDSales); // Slide the panel to the Daily Sales button
        }

        private void btnPass_Click(object sender, EventArgs e)
        {
            slide(btnPass); // Slide the panel to the Password button
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            slide(btnLogout); // Slide the panel to the Logout button
        }
        #endregion buttons

        public void LoadCart()
        {
            int i = 0;
            double total = 0;
            double discount = 0;
            dgvCash.Rows.Clear(); // Clear the DataGridView before loading new data
            cn.Open();
            cmd = new SqlCommand("SELECT c.id, c.pcode, p.item, c.price, c.qty, c.disc, c.total FROM tbCart AS c INNER JOIN tbProduct AS p ON c.pcode = p.pcode WHERE c.transno LIKE @transno and c.status LIKE 'Pending'", cn);
            cmd.Parameters.AddWithValue("@transno", lblTransNo.Text);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                total += Convert.ToDouble(dr["total"].ToString());
                discount += Convert.ToDouble(dr["disc"].ToString());
                dgvCash.Rows.Add(i, dr["id"].ToString(), dr["pcode"].ToString(), dr["item"].ToString(), dr["price"].ToString(), dr["qty"].ToString(), dr["disc"].ToString(), double.Parse(dr["total"].ToString()).ToString("#,##0.00")); // Add rows to the DataGridView with formatted total
            }
            dr.Close();
            cn.Close();
            lblSaleTotal.Text = total.ToString("#,##0.00");
            lblDiscount.Text = discount.ToString("#,##0.00");
            GetCartTotal(); // Calculate and display the total after discount
        }

        public void GetCartTotal()
        {
            double discount = double.Parse(lblDiscount.Text);
            double sales = double.Parse(lblSaleTotal.Text) - discount;

            lblDisplayTotal.Text = sales.ToString("#,##0.00"); // Display the total after discount
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimer.Text = DateTime.Now.ToString("hh:mm:ss tt"); // Update the timer label with the current time
        }

        public void GetTransNo()
        {
            try
            {
                string sdate = DateTime.Now.ToString("yyyyMMdd");
                int count;
                string transno;
                cn.Open();
                cmd = new SqlCommand("SELECT TOP 1 transno FROM tbCart WHERE transno LIKE '" + sdate + "%' ORDER BY id", cn);
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    transno = dr[0].ToString(); // Get the last transaction number
                    count = int.Parse(transno.Substring(8, 4));
                    lblTransNo.Text = sdate + (count + 1);
                }
                else
                {
                    transno = sdate + "1001"; // Initialize the transaction number with a base value
                    lblTransNo.Text = transno;
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                cn.Close(); // Ensure the connection is closed in case of an error
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        }
    }
}
