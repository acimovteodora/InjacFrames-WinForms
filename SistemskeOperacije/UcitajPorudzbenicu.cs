using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domen;

namespace SistemskeOperacije
{
    public class UcitajPorudzbenicu : OpstaSistemskaOperacija
    {
        public Porudzbenica Porudzbenica { get; set; }
        private string Kriterijum;
        protected override void IzvrsiKonkretnuOperaciju(IDomenskiObjekat objekat)
        {
            Porudzbenica = (Porudzbenica)broker.VratiObjekat(objekat);
            Kriterijum = $" where IdPorudzbenice = {Porudzbenica.Id}";
            Porudzbenica.Kompanija = (Kompanija)broker.VratiObjekat(new Kompanija { Id = Porudzbenica.Kompanija.Id });
            Porudzbenica.Stavke = broker.VratiObjekteKriterijum(Kriterijum, new StavkaPorudzbenice()).OfType<StavkaPorudzbenice>().ToList();
            foreach (StavkaPorudzbenice sp in Porudzbenica.Stavke)
            {
                sp.Lajsna = broker.VratiObjekat(new Lajsna { Id = sp.Lajsna.Id }) as Lajsna;
            }
        }

        protected override void Validacija(IDomenskiObjekat objekat)
        {
            if (!(objekat is Porudzbenica))
                throw new Exception("Objekat nije porudzbenica!");
        }
    }
}
