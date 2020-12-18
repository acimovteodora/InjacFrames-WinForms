using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class MenadzerGlavna : UserControl
    {
        private KreirajPor kreirajUC;
        public MenadzerGlavna()
        {
            InitializeComponent();
        }

        private void MenadzerGlavna_Load(object sender, EventArgs e)
        {
            kreirajUC = new KreirajPor();
            panel1.Controls.Add(kreirajUC);
            kreirajUC.Visible = true;
        }
    }
}
