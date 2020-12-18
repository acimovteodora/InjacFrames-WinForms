using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domen;

namespace SistemskeOperacije
{
    public class IzmeniKompaniju : OpstaSistemskaOperacija
    {
        public Kompanija Kompanija;
        protected override void IzvrsiKonkretnuOperaciju(IDomenskiObjekat objekat)
        {
            if (broker.IzmeniObjekat(objekat)) Kompanija = objekat as Kompanija;
            else Kompanija = null;
        }

        protected override void Validacija(IDomenskiObjekat objekat)
        {
            if (!(objekat is Kompanija))
                throw new Exception("Objekat nije kompanija!");
        }
    }
}
