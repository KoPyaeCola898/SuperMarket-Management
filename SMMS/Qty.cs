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
    public partial class Qty : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBConnect dbcon = new DBConnect();
        SqlDataReader dr;
        private string pcode;
        private double price;
        private string transno;
        private int qty;
        Cashier cashier;

        public Qty(Cashier cash)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
            cashier = cash;
        }

        public void ProductDetails(string pcode, double price, string transno, int qty)
        {
            this.pcode = pcode;
            this.price = price;
            this.transno = transno;
            this.qty = qty;
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) && (txtQty.Text != string.Empty))
            {
                cn.Open();
                cmd = new SqlCommand("INSERT INTO tbCart (transno, pcode, price, qty, sdate, cashier) VALUES (@transno, @pcode, @price, @qty, @sdate, @cashier)", cn);
                cmd.Parameters.AddWithValue("@transno", transno);
                cmd.Parameters.AddWithValue("@pcode", pcode);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@qty", int.Parse(txtQty.Text));
                cmd.Parameters.AddWithValue("@sdate", DateTime.Now);
                cmd.Parameters.AddWithValue("@cashier", cashier.lblUsername.Text);
                cmd.ExecuteNonQuery();
                cn.Close();
                cashier.LoadCart(); // Refresh the cart in the Cashier form
                this.Dispose(); // Close the Qty form
            }    
        }
    }
}
