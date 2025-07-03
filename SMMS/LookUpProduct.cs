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

        public LookUpProduct()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.myConnection());
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
    }
}
