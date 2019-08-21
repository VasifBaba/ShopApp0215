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
    public partial class Admindashboard1 : Form
    {
        Client activeAdmin;
        private Client allready;

        public Admindashboard1(Client cl)
        {
            activeAdmin = cl;
            InitializeComponent();
        }

        public Admindashboard1(Client allready)
        {
            this.allready = allready;
        }
    }
}
