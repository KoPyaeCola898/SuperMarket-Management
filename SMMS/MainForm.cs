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
    public partial class MainForm : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DBConnect dbcon = new DBConnect();

        public MainForm()
        {
            InitializeComponent();
            CustomizeDesing();
            cn = new SqlConnection(dbcon.myConnection());
            cn.Open();
            //MessageBox.Show("Database is connected!");
        }

        #region panelSlide
        private void CustomizeDesing()
        {
            panelSubSetting.Visible = false;
        }

        private void HideSubMenu()
        {
            if (panelSubSetting.Visible == true)
                panelSubSetting.Visible = false;
        }

        private void ShowSubMenu(Panel submenu)
        {
            if (submenu.Visible == false)
            {
                HideSubMenu();
                submenu.Visible = true;
            }
            else
            {
                submenu.Visible = false;
            }
        }
        #endregion panelSlide

        private Form activeForm = null;
        public void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            lblTitle.Text = childForm.Text; // Set the title label to the child form's title
            panelMain.Controls.Add(childForm);
            panelMain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            HideSubMenu();
        }

        private void btnProductList_Click(object sender, EventArgs e)
        {
            openChildForm(new Product());
            HideSubMenu();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            openChildForm(new Category());
            HideSubMenu();
        }

        private void btnSaleHistory_Click(object sender, EventArgs e)
        {
            HideSubMenu();
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            ShowSubMenu(panelSubSetting);
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            openChildForm(new UserAccount());
            HideSubMenu();
        }

        private void btnStore_Click(object sender, EventArgs e)
        {
            HideSubMenu();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            HideSubMenu();
        }

        private void btnInStock_Click(object sender, EventArgs e)
        {
            HideSubMenu();
        }

    }
}
