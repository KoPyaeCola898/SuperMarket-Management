using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMMS
{
    public partial class Settle : Form
    {
        public Settle()
        {
            InitializeComponent();
        }

        private void btnOne_Click(object sender, EventArgs e)
        {
            txtCash.Text += 1;
        }

        private void btnTwo_Click(object sender, EventArgs e)
        {
            txtCash.Text += 2;
        }

        private void btnThree_Click(object sender, EventArgs e)
        {
            txtCash.Text += 3;
        }

        private void btnFour_Click(object sender, EventArgs e)
        {
            txtCash.Text += 4;
        }

        private void btnFive_Click(object sender, EventArgs e)
        {
            txtCash.Text += 5;
        }

        private void btnSix_Click(object sender, EventArgs e)
        {
            txtCash.Text += 6;
        }

        private void btnSeven_Click(object sender, EventArgs e)
        {
            txtCash.Text += 7;
        }

        private void btnEight_Click(object sender, EventArgs e)
        {
            txtCash.Text += 8;
        }

        private void btnNine_Click(object sender, EventArgs e)
        {
            txtCash.Text += 9;
        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            txtCash.Text += 0;
        }

        private void btnDZero_Click(object sender, EventArgs e)
        {
            txtCash.Text += 00;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCash.Clear();
            txtCash.Focus();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {

        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
