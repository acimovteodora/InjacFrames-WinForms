using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domen;

namespace SistemskeOperacije
{
    public class ZapamtiPorudzbenicu : OpstaSistemskaOperacija
    {
        public Porudzbenica Porudzbenica { get; set; }
        public Lajsna Lajsna { get; set; }
        protected override void IzvrsiKonkretnuOperaciju(IDomenskiObjekat objekat)
        {
            Porudzbenica p = (Porudzbenica)objekat;
            p.Id = broker.SacuvajObjekat(objekat);
            foreach(StavkaPorudzbenice sp in p.Stavke)
            {
                sp.Porudzbenica = p;
                Lajsna = broker.VratiObjekat((new Lajsna { Id = sp.Lajsna.Id })) as Lajsna;
                Lajsna.UkupnaDuzina -= sp.DuzinaLajsne;
                broker.IzmeniObjekat(Lajsna);
                broker.SacuvajObjekat(sp);
            }
            Porudzbenica = p;
        }

        protected override void Validacija(IDomenskiObjekat objekat)
        {
            if (!(objekat is Porudzbenica))
                throw new Exception("Objekat nije porudzbenica!");
        }
    }
}
