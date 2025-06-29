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
    public partial class Cashier : Form
    {
        public Cashier()
        {
            InitializeComponent();
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

        private void btnNTran_Click(object sender, EventArgs e)
        {
            slide(btnNTran); // Slide the panel to the New Transaction button
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            slide(btnSearch); // Slide the panel to the Search button
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimer.Text = DateTime.Now.ToString("hh:mm:ss tt"); // Update the timer label with the current time
        }
    }
}
