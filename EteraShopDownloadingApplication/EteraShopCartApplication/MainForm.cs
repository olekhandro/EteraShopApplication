using System;
using System.Collections.Generic;
using System.Windows.Forms;
using EteraShopInterractingLibrary.Domain;

namespace EteraShopCartApplication
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            goodAtCartBindingSource.DataSource = new List<GoodCart>();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            goodCartBindingSource.Add(new GoodCart
            {
                Number = Convert.ToInt32(numberTBox.Text),
                Option = optionTBox.Text,
                Status = "",
                Title = titleTBox.Text
            });
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            if (goodCartBindingSource.Current != null)
            {
                goodCartBindingSource.RemoveCurrent();
            }
        }

        private void addToCartButton_Click(object sender, EventArgs e)
        {
            List<GoodCart> goods = new List<GoodCart>();
            foreach (var goodCart in goodCartBindingSource.List)
            {
                goods.Add(((GoodCart)goodCart));
            }
            EteraShopInterractingLibrary.WebsiteManager.AddGoodsToCart(loginTBox.Text, passwordTBox.Text, goods);
        }
    }
}