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
                try
                {
                    string id = "";
                    int cart_qty = 0;
                    bool found = false;
                    cn.Open();
                    cmd = new SqlCommand("SELECT * from tbCart Where transno = @transno and pcode = @pcode", cn);
                    cmd.Parameters.AddWithValue("@transno", transno);
                    cmd.Parameters.AddWithValue("@pcode", pcode);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        id = dr["id"].ToString();
                        cart_qty = int.Parse(dr["qty"].ToString());
                        found = true;
                    }
                    else found = false;
                    dr.Close();
                    cn.Close();

                    if (found)
                    {
                        if (qty < (int.Parse(txtQty.Text) + cart_qty))
                        {
                            MessageBox.Show("Unable to proceed. Remaining qty on hand is " + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        cn.Open();
                        cmd = new SqlCommand("Update tbCart set qty = (qty + " + int.Parse(txtQty.Text) + ")Where id = '" + id + "'", cn);
                        cmd.ExecuteReader();
                        cn.Close();
                        cashier.txtBarCode.Clear();
                        cashier.txtBarCode.Focus();
                        cashier.LoadCart();
                        this.Dispose();
                    }
                    else
                    {
                        if (qty < (int.Parse(txtQty.Text) + cart_qty))
                        {
                            MessageBox.Show("Unable to proceed. Remaining qty on hand is " + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        cn.Open();
                        cmd = new SqlCommand("INSERT INTO tbCart(transno, pcode, price, qty, sdate, cashier)VALUES(@transno, @pcode, @price, @qty, @sdate, @cashier)", cn);
                        cmd.Parameters.AddWithValue("@transno", transno);
                        cmd.Parameters.AddWithValue("@pcode", pcode);
                        cmd.Parameters.AddWithValue("@price", price);
                        cmd.Parameters.AddWithValue("@qty", int.Parse(txtQty.Text));
                        cmd.Parameters.AddWithValue("@sdate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@cashier", cashier.lblUsername.Text);
                        cmd.ExecuteNonQuery();
                        cn.Close();
                        cashier.txtBarCode.Clear();
                        cashier.txtBarCode.Focus();
                        cashier.LoadCart();
                        this.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }    
        }
    }
}
