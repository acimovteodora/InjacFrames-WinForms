using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class FrmServer : Form
    {
        Server s;
        public FrmServer()
        {
            InitializeComponent();
            btnStop.Enabled = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            s = new Server();
            if (s.PokreniServer())
            {
                Thread nit = new Thread(s.Osluskuj);
                nit.IsBackground = true;
                nit.Start();
                btnStart.Enabled = false;
                btnStop.Enabled = true;
            }
            else
            {
                MessageBox.Show("Server nije pokrenut!");
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            s.PrekiniServer();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnStart_EnabledChanged(object sender, EventArgs e)
        {
            if (btnStart.Enabled == false)
            {
                btnStart.ForeColor = Color.FromArgb(186, 130, 106);
                btnStart.BackColor = Color.FromArgb(247, 230, 210);
            }
            else
            {
                btnStart.BackColor = Color.White;
                btnStart.ForeColor = Color.FromArgb(186, 130, 106);
            }
        }

        private void btnStop_EnabledChanged(object sender, EventArgs e)
        {
            if (btnStop.Enabled == false)
            {
                btnStop.ForeColor = Color.FromArgb(186, 130, 106);
                btnStop.BackColor = Color.FromArgb(247, 230, 210);
            }
            else
            {
                btnStop.BackColor = Color.White;
                btnStop.ForeColor = Color.FromArgb(186, 130, 106);
            }
        }
    }
}
