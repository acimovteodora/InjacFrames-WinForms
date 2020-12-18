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
    public partial class FrmKompanije : Form
    {
        private List<Kompanija> kompanijeBaza = Komunikacija.Instance.VratiSveKompanije();
        public FrmKompanije()
        {
            InitializeComponent();
        }
        private void SrediFormu()
        {
            if (dgvKompanije.Rows.Count!=0)
            {
                dgvKompanije.Columns["Drzava"].Visible = false;
                dgvKompanije.Columns["Kontakt"].Visible = false;
                dgvKompanije.Columns["Email"].Visible = false;
                dgvKompanije.Columns["Adresa"].Visible = false;
                dgvKompanije.Columns["Grad"].Visible = false;
                dgvKompanije.Columns["Vlasnik"].Visible = false;
            }
        }
        
        private void FrmKompanije_Load(object sender, EventArgs e)
        {
            dgvKompanije.DataSource = Komunikacija.Instance.VratiSveKompanije();
            SrediFormu();
            btnDodajKompaniju.Enabled = true;
            btnIzmeni.Enabled = false;
        }

        private void txtPretraziKompanije_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                UntagTxt();
                if (string.IsNullOrEmpty(txtPretraziKompanije.Text))
                    dgvKompanije.DataSource = kompanijeBaza;
                else
                {
                    OdgovorServera odg = Komunikacija.Instance.VratiKompanijeKriterijum(txtPretraziKompanije.Text);
                    try
                    {
                        if (odg.Signal == Signal.Error) throw new Exception("Nisu pronadjene kompanije po ovom kriterijumu!");
                    }
                    catch (Exception exx)
                    {
                        FrmPoruka f1 = new FrmPoruka(exx.Message);
                        f1.ShowDialog();
                        dgvKompanije.DataSource = kompanijeBaza;
                        txtPretraziKompanije.Clear();
                        return;
                    }
                    dgvKompanije.DataSource = (List<Kompanija>)odg.Objekat;
                    //FrmPoruka f = new FrmPoruka(odg.Poruka);
                    //f.ShowDialog();
                }
                SrediFormu();
            }
            catch (Exception ex)
            {
                FrmPoruka f = new FrmPoruka(ex.Message);
                f.ShowDialog();
                Environment.Exit(Environment.ExitCode);
            }
        }

        private void dgvKompanije_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                btnDodajKompaniju.Enabled = false;
                btnIzmeni.Enabled = true;
                UntagTxt();
                PocistiLabele();
                if (dgvKompanije.SelectedRows.Count != 0)
                {
                    OdgovorServera odg = Komunikacija.Instance.UcitajKompaniju(((Kompanija)dgvKompanije.SelectedRows[0].DataBoundItem).Id);
                    if(odg.Signal == Signal.Ok)
                    {
                        Kompanija k = (Kompanija)odg.Objekat;
                        txtNazivKompanije.Text = k.NazivKompanije;
                        txtDrzava.Text = k.Drzava;
                        txtGrad.Text = k.Grad;
                        txtAdresa.Text = k.Adresa;
                        txtVlasnik.Text = k.Vlasnik;
                        txtTelefon.Text = k.Kontakt;
                        txtEmail.Text = k.Email;
                    }
                    else
                    {
                        odg.Poruka = "Sistem nije uspeo da ucita odabranu kompaniju!";
                    }
                    FrmPoruka f = new FrmPoruka(odg.Poruka);
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                FrmPoruka f = new FrmPoruka(ex.Message);
                f.ShowDialog();
                Environment.Exit(Environment.ExitCode);
            }
        }

        private void OcistiPolja()
        {
            txtAdresa.Text = string.Empty;
            txtDrzava.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtGrad.Text = string.Empty;
            txtNazivKompanije.Text = string.Empty;
            txtTelefon.Text = string.Empty;
            txtVlasnik.Text = string.Empty;
            btnDodajKompaniju.Enabled = true;
            btnIzmeni.Enabled = false;
            txtPretraziKompanije.Clear();
            dgvKompanije.ClearSelection();
        }

        private void PocistiLabele()
        {
            lblVlasnik.Text = string.Empty;
            lblTelefon.Text = string.Empty;
            lblNaziv.Text = string.Empty;
            lblGrad.Text = string.Empty;
            lblEmail.Text = string.Empty;
            lblDrzava.Text = string.Empty;
            lblAdresa.Text = string.Empty;
            lblEmail.ForeColor = Color.White;
        }

        private bool Provera()
        {
            bool provera = true;
            if (string.IsNullOrEmpty(txtNazivKompanije.Text))
            {
                lblNaziv.ForeColor = Color.DarkRed;
                lblNaziv.Text = "Obavezno je da popunite ovo polje!";
                provera = false;
            }
            else
            {
                lblNaziv.ForeColor = Color.White;
                lblNaziv.Text = string.Empty;
            }
            if (string.IsNullOrEmpty(txtDrzava.Text))
            {
                lblDrzava.ForeColor = Color.DarkRed;
                lblDrzava.Text = "Obavezno je da popunite ovo polje!";
                provera = false;
            }
            else
            {
                lblDrzava.ForeColor = Color.White;
                lblDrzava.Text = string.Empty;
            }
            if (string.IsNullOrEmpty(txtGrad.Text))
            {
                lblGrad.ForeColor = Color.DarkRed;
                lblGrad.Text = "Obavezno je da popunite ovo polje!";
                provera = false;
            }
            else
            {
                lblGrad.ForeColor = Color.White;
                lblGrad.Text = string.Empty;
            }
            if (string.IsNullOrEmpty(txtAdresa.Text))
            {
                lblAdresa.ForeColor = Color.DarkRed;
                lblAdresa.Text = "Obavezno je da popunite ovo polje!";
                provera = false;
            }
            else
            {
                lblAdresa.ForeColor = Color.White;
                lblAdresa.Text = string.Empty;
            }
            if (string.IsNullOrEmpty(txtVlasnik.Text))
            {
                lblVlasnik.ForeColor = Color.DarkRed;
                lblVlasnik.Text = "Obavezno je da popunite ovo polje!";
                provera = false;
            }
            else
            {
                lblVlasnik.ForeColor = Color.White;
                lblVlasnik.Text = string.Empty;
            }
            if (string.IsNullOrEmpty(txtTelefon.Text))
            {
                lblTelefon.ForeColor = Color.DarkRed;
                lblTelefon.Text = "Obavezno je da popunite ovo polje!";
                provera = false;
            }
            else
            {
                lblTelefon.ForeColor = Color.White;
                lblTelefon.Text = string.Empty;
            }
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                lblEmail.ForeColor = Color.DarkRed;
                provera = false;
            }
            else
            {
                lblEmail.ForeColor = Color.White;
            }
            return provera;
        }

        private void btnNazad_Click_1(object sender, EventArgs e)
        {
            UntagTxt();
            FrmAdministratorGlavna f = new FrmAdministratorGlavna();
            this.Dispose();
            f.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UntagTxt();
            OcistiPolja();
            PocistiLabele();
            dgvKompanije.ClearSelection();
            btnDodajKompaniju.Enabled = true;
            btnIzmeni.Enabled = false;
            txtPretraziKompanije.Clear();
            dgvKompanije.ClearSelection();
        }

        private void btnDodajLajsnu_Click(object sender, EventArgs e)
        {
            UntagTxt();
            try
            {
                if (Provera())
                {
                    Kompanija k = new Kompanija
                    {
                        NazivKompanije = txtNazivKompanije.Text,
                        Drzava = txtDrzava.Text,
                        Grad = txtGrad.Text,
                        Adresa = txtAdresa.Text,
                        Vlasnik = txtVlasnik.Text,
                        Kontakt = txtTelefon.Text,
                        Email = txtEmail.Text
                    };
                    foreach (Kompanija kompbaza in Komunikacija.Instance.VratiSveKompanije())
                    {
                        if (kompbaza.Equals(k) || kompbaza.NazivKompanije == k.NazivKompanije)
                        {
                            FrmPoruka f = new FrmPoruka($"Vec postoji kompanija {k.NazivKompanije}!");
                            OcistiPolja();
                            f.ShowDialog();
                            return;
                        }
                    }
                    OdgovorServera odg = Komunikacija.Instance.ZapamtiKompaniju(k);
                    if (odg.Signal == Signal.Ok)
                    {
                        FrmPoruka f = new FrmPoruka(odg.Poruka);
                        f.ShowDialog();
                    }
                    else
                    {
                        FrmPoruka f = new FrmPoruka(odg.Poruka);
                        f.ShowDialog();
                        return;
                    }
                    kompanijeBaza = Komunikacija.Instance.VratiSveKompanije();
                    dgvKompanije.DataSource = kompanijeBaza;
                    dgvKompanije.Refresh();
                    OcistiPolja();
                }
            }
            catch (Exception ex)
            {
                FrmPoruka f = new FrmPoruka(ex.Message);
                f.ShowDialog();
                Environment.Exit(Environment.ExitCode);
            }
        }


        private void txtDrzava_Click(object sender, EventArgs e)
        {
            panel4.BackColor = Color.FromArgb(255, 255, 255);
            txtDrzava.BackColor = Color.FromArgb(186, 226, 218);

            panel5.BackColor = Color.FromArgb(186, 226, 218);
            txtVlasnik.BackColor = Color.FromArgb(255, 255, 255);

            panel8.BackColor = Color.FromArgb(186, 226, 218);
            txtAdresa.BackColor = Color.FromArgb(255, 255, 255);

            panel3.BackColor = Color.FromArgb(186, 226, 218);
            txtNazivKompanije.BackColor = Color.FromArgb(255, 255, 255);

            panel9.BackColor = Color.FromArgb(186, 226, 218);
            txtEmail.BackColor = Color.FromArgb(255, 255, 255);

            panel6.BackColor = Color.FromArgb(186, 226, 218);
            txtGrad.BackColor = Color.FromArgb(255, 255, 255);

            panel7.BackColor = Color.FromArgb(186, 226, 218);
            txtTelefon.BackColor = Color.FromArgb(255, 255, 255);
        }
        private void txtNazivKompanije_Click(object sender, EventArgs e)
        {
            panel3.BackColor = Color.FromArgb(255, 255, 255);
            txtNazivKompanije.BackColor = Color.FromArgb(186, 226, 218);

            panel5.BackColor = Color.FromArgb(186, 226, 218);
            txtVlasnik.BackColor = Color.FromArgb(255, 255, 255);

            panel8.BackColor = Color.FromArgb(186, 226, 218);
            txtAdresa.BackColor = Color.FromArgb(255, 255, 255);

            panel4.BackColor = Color.FromArgb(186, 226, 218);
            txtDrzava.BackColor = Color.FromArgb(255, 255, 255);

            panel9.BackColor = Color.FromArgb(186, 226, 218);
            txtEmail.BackColor = Color.FromArgb(255, 255, 255);

            panel6.BackColor = Color.FromArgb(186, 226, 218);
            txtGrad.BackColor = Color.FromArgb(255, 255, 255);

            panel7.BackColor = Color.FromArgb(186, 226, 218);
            txtTelefon.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void txtVlasnik_Click(object sender, EventArgs e)
        {
            panel5.BackColor = Color.FromArgb(255, 255, 255);
            txtVlasnik.BackColor = Color.FromArgb(186, 226, 218);

            panel3.BackColor = Color.FromArgb(186, 226, 218);
            txtNazivKompanije.BackColor = Color.FromArgb(255, 255, 255);

            panel8.BackColor = Color.FromArgb(186, 226, 218);
            txtAdresa.BackColor = Color.FromArgb(255, 255, 255);

            panel4.BackColor = Color.FromArgb(186, 226, 218);
            txtDrzava.BackColor = Color.FromArgb(255, 255, 255);

            panel9.BackColor = Color.FromArgb(186, 226, 218);
            txtEmail.BackColor = Color.FromArgb(255, 255, 255);

            panel6.BackColor = Color.FromArgb(186, 226, 218);
            txtGrad.BackColor = Color.FromArgb(255, 255, 255);

            panel7.BackColor = Color.FromArgb(186, 226, 218);
            txtTelefon.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void txtGrad_Click(object sender, EventArgs e)
        {
            panel6.BackColor = Color.FromArgb(255, 255, 255);
            txtGrad.BackColor = Color.FromArgb(186, 226, 218);

            panel5.BackColor = Color.FromArgb(186, 226, 218);
            txtVlasnik.BackColor = Color.FromArgb(255, 255, 255);

            panel8.BackColor = Color.FromArgb(186, 226, 218);
            txtAdresa.BackColor = Color.FromArgb(255, 255, 255);

            panel4.BackColor = Color.FromArgb(186, 226, 218);
            txtDrzava.BackColor = Color.FromArgb(255, 255, 255);

            panel9.BackColor = Color.FromArgb(186, 226, 218);
            txtEmail.BackColor = Color.FromArgb(255, 255, 255);

            panel3.BackColor = Color.FromArgb(186, 226, 218);
            txtNazivKompanije.BackColor = Color.FromArgb(255, 255, 255);

            panel7.BackColor = Color.FromArgb(186, 226, 218);
            txtTelefon.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void txtTelefon_Click(object sender, EventArgs e)
        {
            panel7.BackColor = Color.FromArgb(255, 255, 255);
            txtTelefon.BackColor = Color.FromArgb(186, 226, 218);

            panel5.BackColor = Color.FromArgb(186, 226, 218);
            txtVlasnik.BackColor = Color.FromArgb(255, 255, 255);

            panel8.BackColor = Color.FromArgb(186, 226, 218);
            txtAdresa.BackColor = Color.FromArgb(255, 255, 255);

            panel4.BackColor = Color.FromArgb(186, 226, 218);
            txtDrzava.BackColor = Color.FromArgb(255, 255, 255);

            panel9.BackColor = Color.FromArgb(186, 226, 218);
            txtEmail.BackColor = Color.FromArgb(255, 255, 255);

            panel6.BackColor = Color.FromArgb(186, 226, 218);
            txtGrad.BackColor = Color.FromArgb(255, 255, 255);

            panel3.BackColor = Color.FromArgb(186, 226, 218);
            txtNazivKompanije.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void txtAdresa_Click(object sender, EventArgs e)
        {
            panel8.BackColor = Color.FromArgb(255, 255, 255);
            txtAdresa.BackColor = Color.FromArgb(186, 226, 218);

            panel5.BackColor = Color.FromArgb(186, 226, 218);
            txtVlasnik.BackColor = Color.FromArgb(255, 255, 255);

            panel3.BackColor = Color.FromArgb(186, 226, 218);
            txtNazivKompanije.BackColor = Color.FromArgb(255, 255, 255);

            panel4.BackColor = Color.FromArgb(186, 226, 218);
            txtDrzava.BackColor = Color.FromArgb(255, 255, 255);

            panel9.BackColor = Color.FromArgb(186, 226, 218);
            txtEmail.BackColor = Color.FromArgb(255, 255, 255);

            panel6.BackColor = Color.FromArgb(186, 226, 218);
            txtGrad.BackColor = Color.FromArgb(255, 255, 255);

            panel7.BackColor = Color.FromArgb(186, 226, 218);
            txtTelefon.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void txtEmail_Click(object sender, EventArgs e)
        {
            panel9.BackColor = Color.FromArgb(255, 255, 255);
            txtEmail.BackColor = Color.FromArgb(186, 226, 218);

            panel5.BackColor = Color.FromArgb(186, 226, 218);
            txtVlasnik.BackColor = Color.FromArgb(255, 255, 255);

            panel8.BackColor = Color.FromArgb(186, 226, 218);
            txtAdresa.BackColor = Color.FromArgb(255, 255, 255);

            panel4.BackColor = Color.FromArgb(186, 226, 218);
            txtDrzava.BackColor = Color.FromArgb(255, 255, 255);

            panel3.BackColor = Color.FromArgb(186, 226, 218);
            txtNazivKompanije.BackColor = Color.FromArgb(255, 255, 255);

            panel6.BackColor = Color.FromArgb(186, 226, 218);
            txtGrad.BackColor = Color.FromArgb(255, 255, 255);

            panel7.BackColor = Color.FromArgb(186, 226, 218);
            txtTelefon.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void UntagTxt()
        {
            panel3.BackColor = Color.FromArgb(186, 226, 218);
            txtNazivKompanije.BackColor = Color.FromArgb(255, 255, 255);

            panel5.BackColor = Color.FromArgb(186, 226, 218);
            txtVlasnik.BackColor = Color.FromArgb(255, 255, 255);

            panel8.BackColor = Color.FromArgb(186, 226, 218);
            txtAdresa.BackColor = Color.FromArgb(255, 255, 255);

            panel4.BackColor = Color.FromArgb(186, 226, 218);
            txtDrzava.BackColor = Color.FromArgb(255, 255, 255);

            panel9.BackColor = Color.FromArgb(186, 226, 218);
            txtEmail.BackColor = Color.FromArgb(255, 255, 255);

            panel6.BackColor = Color.FromArgb(186, 226, 218);
            txtGrad.BackColor = Color.FromArgb(255, 255, 255);

            panel7.BackColor = Color.FromArgb(186, 226, 218);
            txtTelefon.BackColor = Color.FromArgb(255, 255, 255);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmAdministratorGlavna f = new FrmAdministratorGlavna();
            this.Dispose();
            f.ShowDialog();
        }

        private void btnIzmeni_Click(object sender, EventArgs e)
        {
            try
            {
                if (Provera() && dgvKompanije.SelectedRows.Count>0)
                {
                    Kompanija k = new Kompanija
                    {
                        Id = ((Kompanija)dgvKompanije.SelectedRows[0].DataBoundItem).Id,
                        NazivKompanije = txtNazivKompanije.Text,
                        Adresa = txtAdresa.Text,
                        Drzava = txtDrzava.Text,
                        Grad = txtGrad.Text,
                        Vlasnik = txtVlasnik.Text,
                        Kontakt = txtTelefon.Text,
                        Email = txtEmail.Text
                    };
                    FrmPoruka f;
                    if (Izmenjena(k))
                    {
                        OdgovorServera odg = Komunikacija.Instance.IzmeniKompaniju(k);
                        if (odg.Signal == Signal.Ok)
                        {
                            kompanijeBaza = Komunikacija.Instance.VratiSveKompanije();
                            dgvKompanije.DataSource = kompanijeBaza;
                            dgvKompanije.Refresh();
                        }
                        f = new FrmPoruka(odg.Poruka);
                    }
                    else
                    {
                        f = new FrmPoruka($"Niste izmenili ni jedan podatak o kompaniji {k.NazivKompanije}!");

                    }
                    f.ShowDialog();
                    OcistiPolja();
                    PocistiLabele();
                    UntagTxt();
                    btnIzmeni.Enabled = false;
                    btnDodajKompaniju.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                FrmPoruka f = new FrmPoruka(ex.Message);
                f.ShowDialog();
                Environment.Exit(Environment.ExitCode);
            }
        }

        private bool Izmenjena(Kompanija k)
        {
            Kompanija stara = dgvKompanije.SelectedRows[0].DataBoundItem as Kompanija;
            if (stara.Izmenjena(k))
                return true;
            return false;
        }

        private void btnIzmeni_EnabledChanged(object sender, EventArgs e)
        {
            if(btnIzmeni.Enabled == false)
            {
                btnIzmeni.ForeColor = Color.FromArgb(186, 130, 106);
                btnIzmeni.BackColor = Color.FromArgb(247, 230, 210);
            }
            else
            {
                btnIzmeni.BackColor = Color.White;
                btnIzmeni.ForeColor = Color.FromArgb(186, 130, 106);
            }
        }

        private void btnDodajKompaniju_EnabledChanged(object sender, EventArgs e)
        {
            if (btnDodajKompaniju.Enabled == false)
            {
                btnDodajKompaniju.ForeColor = Color.FromArgb(186, 130, 106);
                btnDodajKompaniju.BackColor = Color.FromArgb(247, 230, 210);
            }
            else
            {
                btnDodajKompaniju.BackColor = Color.White;
                btnDodajKompaniju.ForeColor = Color.FromArgb(186, 130, 106);
            }
        }
    }
}
