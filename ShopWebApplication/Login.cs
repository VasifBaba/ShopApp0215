using ShopWebApplication.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopWebApplication
{
    public partial class Login : Form
    {
        ShopAppEntities db = new ShopAppEntities();
        public Login()
        {
            InitializeComponent();
        }
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            if (Extensions.CheckInput(new string[] { email, password }, string.Empty))
            {
                Client allready = db.Clients.FirstOrDefault(cl => cl.Email == email);
                if (allready != null)
                {

                    if (allready.Password == password.hashMe())
                    {
                        if (allready.Status == 1)
                        {
                            Admindashboard1 adm = new Admindashboard1(allready);
                            adm.ShowDialog();
                        }
                        else
                        {
                            if (ckRemember.Checked)
                            {
                                Properties.Settings.Default.email = email;
                                Properties.Settings.Default.password = password;
                                Properties.Settings.Default.checkedBox = true;
                                Properties.Settings.Default.Save();
                            }
                            else
                            {
                                Properties.Settings.Default.email = "";
                                Properties.Settings.Default.password = "";
                                Properties.Settings.Default.checkedBox = false;
                                Properties.Settings.Default.Save();
                            }
                            lblerror.Visible = false;
                            Dashboard ds = new Dashboard(allready);
                            ds.ShowDialog();
                        }
                    }

                    else
                    {
                        lblerror.Text = "Password is not Correct";
                        lblerror.Visible = true;
                    }
                }
                else
                {
                    lblerror.Text = "Email is not Correct";
                    lblerror.Visible = true;
                }
            }
            else
            {
                lblerror.Text = "Please all the fill";
                lblerror.Visible = true;
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.checkedBox)
            {
                txtEmail.Text = Properties.Settings.Default.email;
                txtPassword.Text = Properties.Settings.Default.password;
                ckRemember.Checked = true;
            }
        }
    }
}
