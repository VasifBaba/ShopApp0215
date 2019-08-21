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
    public partial class Dashboard : Form
    {
        ShopAppEntities db = new ShopAppEntities();
        Client activeClient;

        public Dashboard(Client cs)
        {
            activeClient = cs;
            InitializeComponent();
        }

        public partial class Dashboard : Form
        {

            private void OrderListFull()
            {
                dtgorderlist.DataSource = db.Orders.Where(o => o.ClientID == activeClient.ID).Select(ord => (
                    ord.Product.Name,
                    ord.Product.Price,
                    ord.Amount,
                    ord.BuyDate
                )).ToList();
            }
            private void Dashboard_Load(object sender, EventArgs e)
            {
                lblWelcome.Text = "Welcome" + activeClient.Fullname;
                OrderListFull();
                FillCategoryCombo();

            }
            private void FillCategoryCombo()
            {
                cmbCategory.Items.AddRange(db.Categories.Select(c => c.Name).ToArray());
            }

            private void CmbCategory_SelectedIndexChanged(object sender, EventArgs e)
            {
                FillComboProduct();
            }
            private void FillComboProduct()
            {
                string catName = cmbCategory.Text;
                int CatId = db.Categories.FirstOrDefault(c => c.Name == catName).ID;
                cmbProducts.Items.Clear();
                cmbProducts.Items.AddRange(db.Products.Where(p => p.CategoryID == CatId).Select(p => p.Name).ToArray());
            }
            private void CmbProducts_SelectedIndexChanged(object sender, EventArgs e)
            {
                string productname = cmbProducts.Text;
                if (productname != "")
                {
                    Product selectProduct = db.Products.FirstOrDefault(p => p.Name == productname);
                    if (selectProduct.Count == 0)
                    {
                        lblstock.Text = "Bu məhsuldan qalmayıb";
                        lblstock.Visible = true;
                        btnAddProduct.Enabled = false;
                    }
                    else
                    {
                        lblPrice.Text = ((double)nmCount.Value * selectProduct.Price + "Azn").ToString();
                        lblstock.Text = string.Format("Məhsuldan {0} ədəd qalıb", selectProduct.Count);
                        lblstock.Visible = true;
                        lblPrice.Visible = true;
                        btnAddProduct.Enabled = true;
                        nmCount.Visible = true;
                    }
                }
                else
                {
                    nmCount.Visible = true;
                }
            }

            private void NmCount_ValueChanged(object sender, EventArgs e)
            {
                string productname = cmbCategory.Text;
                Product selectProduct = db.Products.FirstOrDefault(p => p.Name == productname);
                lblPrice.Text = ((double)nmCount.Value * selectProduct.Price + "Azn").ToString();
                lblstock.Visible = true;
                lblPrice.Visible = true;
            }
            private void clearAll()
            {
                cmbCategory.Text = "";
                cmbProducts.Text = "";
                lblPrice.Visible = false;
                lblstock.Visible = false;
                nmCount.Visible = false;

            }
            private void BtnAddProduct_Click(object sender, EventArgs e)
            {
                string categoryName = cmbCategory.Text;
                string productName = cmbProducts.Text;
                int count = (int)nmCount.Value;
                if (Extensions.CheckInput(new string[] {
                ProductName, categoryName
            }, string.Empty))
                {
                    Product selectedPro = db.Products.FirstOrDefault(p => p.Name == productName);
                    if (nmCount.Value <= selectedPro.Count)
                    {
                        Order ord = new Order();
                        ord.ClientID = activeClient.ID;
                        ord.ProductID = selectedPro.ID;
                        ord.Amount = count;
                        ord.Price = selectedPro.Price * count;
                        ord.BuyDate = DateTime.Now;
                        db.Orders.Add(ord);
                        selectedPro.Count = selectedPro.Count - count;
                        db.SaveChanges();
                        MessageBox.Show("Məhsul alındı !", "Uğurlu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clearAll();
                        OrderListFull();
                    }
                    else
                    {
                        lblError.Text = "Istediyiniz sayda mehsul qalmayib";
                        lblError.Visible = true;
                    }
                }
                else
                {
                    lblError.Text = "Please all the fill";
                    lblError.Visible = true;
                }
            }

        }
    }

}
