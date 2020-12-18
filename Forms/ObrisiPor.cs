using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domen;

namespace Forms
{
    public partial class ObrisiPor : UserControl
    {
        public ObrisiPor()
        {
            InitializeComponent();
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPorudzbenice.SelectedRows.Count != 0)
                {
                    OdgovorServera odg1 = Komunikacija.Instance.UcitajPorudzbenicu(((Porudzbenica)dgvPorudzbenice.SelectedRows[0].DataBoundItem).Id);
                    Porudzbenica p = odg1.Objekat as Porudzbenica;
                    OdgovorServera odg = Komunikacija.Instance.ObrisiPorudzbenicu(p);
                    if (odg.Signal == Signal.Ok)
                    {
                        FrmPoruka f1 = new FrmPoruka($"Upešno je obrisana porudžbenica kompanije {p.Kompanija.NazivKompanije}");
                        f1.ShowDialog();
                        dgvPorudzbenice.DataSource = Komunikacija.Instance.VratiSvePorudzbenice();
                        return;
                    }
                    FrmPoruka f = new FrmPoruka(odg.Poruka);
                    f.ShowDialog();
                }
                else
                {
                    FrmPoruka f = new FrmPoruka("Odaberite porudzbenicu koju zelite da izbrisete.");
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

        private void ObrisiPor_Load(object sender, EventArgs e)
        {
            dgvPorudzbenice.DataSource = Komunikacija.Instance.VratiSvePorudzbenice();
        }

        private void btnObrisi_MouseHover(object sender, EventArgs e)
        {
            btnObrisi.BackColor = Color.FromArgb(186, 226, 218);
            btnObrisi.ForeColor = Color.FromArgb(255, 255, 255);
        }

        private void btnObrisi_MouseLeave(object sender, EventArgs e)
        {
            btnObrisi.BackColor = Color.FromArgb(255, 255, 255);
            btnObrisi.ForeColor = Color.FromArgb(186, 130, 106);
        }
    }
}
