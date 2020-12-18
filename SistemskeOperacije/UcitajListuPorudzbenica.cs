using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domen;

namespace SistemskeOperacije
{
    public class UcitajListuPorudzbenica : OpstaSistemskaOperacija
    {
        public List<Porudzbenica> Porudzbenice = new List<Porudzbenica>();
        private string Kriterijum;
        public UcitajListuPorudzbenica(string kriterijum)
        {
            Kriterijum = kriterijum;
        }
        protected override void IzvrsiKonkretnuOperaciju(IDomenskiObjekat objekat)
        {
            Porudzbenice = broker.VratiObjekteKriterijum(Kriterijum, objekat).OfType<Porudzbenica>().ToList();
            foreach (Porudzbenica p in Porudzbenice)
            {
                Kriterijum = p.UslovZaPretragu;
                p.Kompanija = (Kompanija)broker.VratiObjekat(new Kompanija { Id = p.Kompanija.Id});
                p.Stavke = broker.VratiObjekteKriterijum(Kriterijum, new StavkaPorudzbenice()).OfType<StavkaPorudzbenice>().ToList();
                foreach (StavkaPorudzbenice sp in p.Stavke)
                {
                    sp.Lajsna = broker.VratiObjekat(new Lajsna { Id = sp.Lajsna.Id }) as Lajsna;
                }
            }
        }

        protected override void Validacija(IDomenskiObjekat objekat)
        {
            if (!(objekat is Porudzbenica))
                throw new Exception("Objekat nije porudzbenica!");
        }
    }
}
