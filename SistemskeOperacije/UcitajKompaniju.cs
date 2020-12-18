using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domen;

namespace SistemskeOperacije
{
    public class UcitajKompaniju : OpstaSistemskaOperacija
    {
        public Kompanija Kompanija { get; set; }
        protected override void IzvrsiKonkretnuOperaciju(IDomenskiObjekat objekat)
        {
            Kompanija = (Kompanija)broker.VratiObjekat(objekat);
        }

        protected override void Validacija(IDomenskiObjekat objekat)
        {
            if (!(objekat is Kompanija))
                throw new Exception("Objekat nije kompanija!");
        }
    }
}
