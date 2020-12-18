using Domen;
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
    public partial class FrmLajsne : Form
    {
        private List<Lajsna> lajsne = Komunikacija.Instance.VratiSveLajsne();
        private BindingList<Lajsna> ucitaneLajsne = new BindingList<Lajsna>();
        private List<Boja> boje = new List<Boja>
        {
            Boja.bela, Boja.bez, Boja.crna,Boja.crvena, Boja.plava, Boja.siva, Boja.smedja, Boja.srebrna, Boja.zelena, Boja.zlatna, Boja.zuta
        };
        public FrmLajsne()
        {
            InitializeComponent();
        }

        private void btnNazad_Click(object sender, EventArgs e)
        {
            FrmAdministratorGlavna f = new FrmAdministratorGlavna();
            Dispose();
            f.ShowDialog();
        }

        private void dgvLajsne_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                UntagTxt();
                lblRaspolozviostLajsne.Text = string.Empty;
                lblNazivLajsne.Text = string.Empty;
                lblCenaLajsne.Text = string.Empty;
                if (dgvLajsne.SelectedRows.Count == 1)
                {
                    Lajsna selektovana = Komunikacija.Instance.UcitajLajsnu(((Lajsna)dgvLajsne.SelectedRows[0].DataBoundItem).Id);
                    txtNazivLajsne.Text = selektovana.NazivLajsne;
                    txtCenaLajsne.Text = selektovana.CenaMetra.ToString();
                    txtRaspolozivostLajsne.Text = selektovana.UkupnaDuzina.ToString();
                    cmbBoje.SelectedItem = selektovana.Boja;
                }
            }
            catch (Exception ex)
            {
                FrmPoruka f = new FrmPoruka(ex.Message);
                f.ShowDialog();
                Environment.Exit(Environment.ExitCode);
            }
        }

        private void FrmLajsne_Load(object sender, EventArgs e)
        {
            foreach (Lajsna lajsna in lajsne)
            {
                ucitaneLajsne.Add(lajsna);
            }
            dgvLajsne.DataSource = ucitaneLajsne;
            cmbBoje.DataSource = boje;
            cmbBoje.DataSource = boje;
            SrediFormu();
        }

        private void OcistiPolja()
        {
            txtCenaLajsne.Clear();
            txtNazivLajsne.Clear();
            txtRaspolozivostLajsne.Clear();
            lblCenaLajsne.Text = string.Empty;
            lblNazivLajsne.Text = string.Empty;
            lblRaspolozviostLajsne.Text = string.Empty;
            cmbBoje.SelectedIndex = 0;
        }

        private bool PostojiLajsna(string nazivLajsne)
        {
            foreach (Lajsna lajsna in ucitaneLajsne)
            {
                if (lajsna.NazivLajsne == nazivLajsne) return true;
            }
            return false;
        }

        private int DajId()
        {
            int max = 0;
            foreach (Lajsna lajsna in ucitaneLajsne)
            {
                if (lajsna.Id >= max)
                {
                    max = lajsna.Id;
                }
            }
            return max+1;
        }

        private bool Provera()
        {
            bool provera = true;
            if (string.IsNullOrEmpty(txtNazivLajsne.Text))
            {
                lblNazivLajsne.Text = "Popunite!";
                lblNazivLajsne.ForeColor = Color.DarkRed;
                provera = false;
            }
            else
            {
                lblNazivLajsne.Text = string.Empty;
                lblNazivLajsne.ForeColor = Color.White;
            }
            if (string.IsNullOrEmpty(txtCenaLajsne.Text))
            {
                lblCenaLajsne.Text = "Popunite!";
                lblCenaLajsne.ForeColor = Color.DarkRed;
                provera = false;
            }
            else
            {
                if (!double.TryParse(txtCenaLajsne.Text, out double p1))
                {
                    lblCenaLajsne.Text = "Mora biti broj!";
                    lblCenaLajsne.ForeColor = Color.DarkRed;
                    provera = false;
                }
                else
                {
                    lblCenaLajsne.Text = string.Empty;
                    lblCenaLajsne.ForeColor = Color.White;
                }
            }
            if (string.IsNullOrEmpty(txtRaspolozivostLajsne.Text))
            {
                lblRaspolozviostLajsne.Text = "Popunite!";
                lblRaspolozviostLajsne.ForeColor = Color.DarkRed;
                provera = false;
            }
            else
            {
                if (!double.TryParse(txtRaspolozivostLajsne.Text, out double p1))
                {
                    lblRaspolozviostLajsne.Text = "Mora biti broj!";
                    lblRaspolozviostLajsne.ForeColor = Color.DarkRed;
                    provera = false;
                }
                else
                {
                    lblRaspolozviostLajsne.Text = string.Empty;
                    lblRaspolozviostLajsne.ForeColor = Color.White;
                }
            }
            return provera;
        }

        private void SrediFormu()
        { 
            dgvLajsne.Columns["CenaMetra"].Visible = false;
            dgvLajsne.Columns["UkupnaDuzina"].Visible = false;
        }

        private void btnDodajLajsnu_Click_1(object sender, EventArgs e)
        {
            try
            {
                UntagTxt();
                if (Provera())
                {
                    Lajsna lajsna = new Lajsna
                    {
                        Id = DajId(),
                        NazivLajsne = txtNazivLajsne.Text,
                        CenaMetra = double.Parse(txtCenaLajsne.Text),
                        UkupnaDuzina = double.Parse(txtRaspolozivostLajsne.Text),
                        Boja = (Boja)cmbBoje.SelectedItem
                    };
                    if (PostojiLajsna(lajsna.NazivLajsne))
                    {
                        FrmPoruka f2 = new FrmPoruka("Lajsna vec postoji!");
                        f2.ShowDialog();
                        OcistiPolja();
                        return;
                    }
                    OdgovorServera odg =  Komunikacija.Instance.ZapamtiLajsnu(lajsna);
                    if (odg.Signal == Signal.Error)
                    {
                        FrmPoruka f2 = new FrmPoruka("Doslo je do greske pri cuvanju lajsne!");
                        f2.ShowDialog();
                        OcistiPolja();
                        UntagTxt();
                        dgvLajsne.ClearSelection();
                        return;
                    }
                    FrmPoruka f1 = new FrmPoruka(odg.Poruka);
                    f1.ShowDialog();
                    OcistiPolja();
                    dgvLajsne.DataSource = Komunikacija.Instance.VratiSveLajsne();
                    UntagTxt();
                    dgvLajsne.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                FrmPoruka f = new FrmPoruka(ex.Message);
                f.ShowDialog();
                Environment.Exit(Environment.ExitCode);
            }
        }

        private void btnOcisti_Click_1(object sender, EventArgs e)
        {
            OcistiPolja();
            UntagTxt();
            dgvLajsne.ClearSelection();
        }

        private void UntagTxt()
        {
            panel3.BackColor = Color.FromArgb(186, 226, 218);
            txtNazivLajsne.BackColor = Color.FromArgb(255, 255, 255);

            panel4.BackColor = Color.FromArgb(186, 226, 218);
            txtCenaLajsne.BackColor = Color.FromArgb(255, 255, 255);

            panel5.BackColor = Color.FromArgb(186, 226, 218);
            txtRaspolozivostLajsne.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void txtNazivLajsne_Click(object sender, EventArgs e)
        {
            panel3.BackColor = Color.FromArgb(255, 255, 255);
            txtNazivLajsne.BackColor = Color.FromArgb(186, 226, 218);

            panel4.BackColor = Color.FromArgb(186, 226, 218);
            txtCenaLajsne.BackColor = Color.FromArgb(255, 255, 255);

            panel5.BackColor = Color.FromArgb(186, 226, 218);
            txtRaspolozivostLajsne.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void txtCenaLajsne_Click(object sender, EventArgs e)
        {
            panel4.BackColor = Color.FromArgb(255, 255, 255);
            txtCenaLajsne.BackColor = Color.FromArgb(186, 226, 218);

            panel3.BackColor = Color.FromArgb(186, 226, 218);
            txtNazivLajsne.BackColor = Color.FromArgb(255, 255, 255);

            panel5.BackColor = Color.FromArgb(186, 226, 218);
            txtRaspolozivostLajsne.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void txtRaspolozivostLajsne_Click(object sender, EventArgs e)
        {
            panel5.BackColor = Color.FromArgb(255, 255, 255);
            txtRaspolozivostLajsne.BackColor = Color.FromArgb(186, 226, 218);

            panel3.BackColor = Color.FromArgb(186, 226, 218);
            txtNazivLajsne.BackColor = Color.FromArgb(255, 255, 255);

            panel4.BackColor = Color.FromArgb(186, 226, 218);
            txtCenaLajsne.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void btnDodajLajsnu_MouseHover(object sender, EventArgs e)
        {
            btnDodajLajsnu.BackColor = Color.FromArgb(186, 226, 218);
            btnDodajLajsnu.ForeColor = Color.FromArgb(255, 255, 255);
        }
        private void btnOcisti_MouseHover(object sender, EventArgs e)
        {
            btnOcisti.BackColor = Color.FromArgb(186, 226, 218);
            btnOcisti.ForeColor = Color.FromArgb(255, 255, 255);
        }

        private void btnOcisti_MouseLeave(object sender, EventArgs e)
        {
            btnOcisti.BackColor = Color.FromArgb(255, 255, 255);
            btnOcisti.ForeColor = Color.FromArgb(186,130,106);
        }

        private void btnDodajLajsnu_MouseLeave(object sender, EventArgs e)
        {
            btnDodajLajsnu.BackColor = Color.FromArgb(255, 255, 255);
            btnDodajLajsnu.ForeColor = Color.FromArgb(186, 130, 106);
        }

        private void btnNazad_MouseHover(object sender, EventArgs e)
        {
            btnNazad.BackColor = Color.FromArgb(186, 226, 218);
            btnNazad.ForeColor = Color.FromArgb(255, 255, 255);
        }

        private void btnNazad_MouseLeave(object sender, EventArgs e)
        {
            btnNazad.BackColor = Color.FromArgb(255, 255, 255);
            btnNazad.ForeColor = Color.FromArgb(186, 130, 106);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmAdministratorGlavna f = new FrmAdministratorGlavna();
            Dispose();
            f.ShowDialog();
        }
        
    }
}
