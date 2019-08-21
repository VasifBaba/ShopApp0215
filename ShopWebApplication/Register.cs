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
    public partial class Register : Form
    {
        ShopAppEntities db = new ShopAppEntities();
        public  Register()
        {
            InitializeComponent();
        }
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            string fullname = txtFul.Text;
            string email = txtEmail.Text;
            string phone = s.Text;
            string password = txtPassword.Text;
            string confirmpassword = txtConfirmPassword.Text;
            string[] checkInput = new string[]
            {
                fullname, email, phone, password, confirmpassword
            };
            if (Extensions.CheckInput(checkInput, string.Empty))
            {
                lblerror.Visible = false;
                if (phone.Length < 15)
                {
                    if (password.Length > 7)
                    {
                        if (password == confirmpassword)
                        {
                            Client cs = db.Clients.FirstOrDefault(c => c.Email == email);
                            if (cs == null)
                            {
                                db.Clients.Add(new Client()
                                {
                                    Fullname = fullname,
                                    Email = email,
                                    Phone = phone,
                                    Password = password.hashMe()
                                });
                                db.SaveChanges();
                                MessageBox.Show("Client was add succsessfull !", "Succsess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                lblerror.Text = "Email already this";
                                lblerror.Visible = true;
                            }
                        }
                        else
                        {
                            lblerror.Text = "Password and Confirm Password is not equal";
                            lblerror.Visible = true;
                        }
                    }
                    else
                    {
                        lblerror.Text = "Password minimum length 8 charachter";
                        lblerror.Visible = true;
                    }
                }
                else
                {
                    lblerror.Text = "Phone number length 15 charachter";
                    lblerror.Visible = true;
                }
            }
            else
            {
                lblerror.Text = "Please all the fill";
                lblerror.Visible = true;
            }

        }

        private void TxtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
    }
}
