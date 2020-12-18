using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domen;

namespace SistemskeOperacije
{
    public class ZapamtiKompaniju : OpstaSistemskaOperacija
    {
        public Kompanija Kompanija;
        protected override void IzvrsiKonkretnuOperaciju(IDomenskiObjekat objekat)
        {
            if (broker.SacuvajObjekat(objekat) != 1)
                Kompanija = null;
            else Kompanija = (Kompanija)objekat;
        }

        protected override void Validacija(IDomenskiObjekat objekat)
        {
            if (!(objekat is Kompanija))
                throw new Exception("Objekat nije kompanija!");
        }
    }
}
