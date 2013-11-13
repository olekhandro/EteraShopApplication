using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using EteraShopInterractingLibrary;

namespace EterashopWinFormsDownloader
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ko-KR");
            saveFileDialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
        }

        private void setFilenameBtn_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                outputFilenameTBox.Text = saveFileDialog.FileName;
            }
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(loginTBox.Text))
            {
                MessageBox.Show("Login field cannot be empty");
                return;
            }
            if (string.IsNullOrEmpty(passwordTBox.Text))
            {
                MessageBox.Show("Password field cannot be empty");
                return;
            }
            if (string.IsNullOrEmpty(outputFilenameTBox.Text))
            {
                MessageBox.Show("Output filename field cannot be empty");
                return;
            }
            if (string.IsNullOrEmpty(categoriesTBox.Text))
            {
                MessageBox.Show("Categories field cannot be empty");
                return;
            }
            var categories = new List<string>();
            foreach (var category in categoriesTBox.Lines)
            {
                categories.Add(category);
            }

            var goods = ChromeWebsiteManager.GetGoodsFromWebsite(loginTBox.Text, passwordTBox.Text, categories);
            CSVWriter.MakeCSVFile(outputFilenameTBox.Text, goods);
        }
    }
}
