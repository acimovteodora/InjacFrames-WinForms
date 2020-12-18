using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domen;

namespace SistemskeOperacije
{
    public class ObrisiPorudzbenicu : OpstaSistemskaOperacija
    {
        public int Obrisana;
        public Porudzbenica Porudzbenica;
        protected override void IzvrsiKonkretnuOperaciju(IDomenskiObjekat objekat)
        {
            Porudzbenica = (Porudzbenica)objekat;
            Obrisana = broker.ObrisiObjekat(objekat);
            if (Obrisana != 1) Porudzbenica = null;
            else
                Porudzbenica = (Porudzbenica)objekat;
        }

        protected override void Validacija(IDomenskiObjekat objekat)
        {
            if (!(objekat is Porudzbenica))
                throw new Exception("Objekat nije porudzbenica!");
        }
    }
}
