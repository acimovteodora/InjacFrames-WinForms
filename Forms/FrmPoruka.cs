using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class FrmPoruka : Form
    {
        public FrmPoruka(string text)
        {
            InitializeComponent();
            label1.Text = text;
            if (label1.Enabled == false)
            {
                label1.BackColor = Color.White;
            }
        }

        private void btnPrijava_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
