using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domen;

namespace SistemskeOperacije
{
    public class UcitajListuKompanija : OpstaSistemskaOperacija
    {
        public List<Kompanija> Kompanije { get; set; }
        protected override void IzvrsiKonkretnuOperaciju(IDomenskiObjekat objekat)
        {
            Kompanije = broker.VratiSveObjekte(objekat).OfType<Kompanija>().ToList();
        }

        protected override void Validacija(IDomenskiObjekat objekat)
        {
            if (!(objekat is Kompanija))
                throw new Exception("Objekat nije kompanija!");
        }
    }
}
