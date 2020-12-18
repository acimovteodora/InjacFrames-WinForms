using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domen;

namespace SistemskeOperacije
{
    public class UcitajListuLajsni : OpstaSistemskaOperacija
    {
        public List<Lajsna> Lajsne { get; set; }
        protected override void IzvrsiKonkretnuOperaciju(IDomenskiObjekat objekat)
        {
            Lajsne = broker.VratiSveObjekte(objekat).OfType<Lajsna>().ToList();
        }

        protected override void Validacija(IDomenskiObjekat objekat)
        {
            if (!(objekat is Lajsna))
                throw new Exception("Objekat nije lajsna!");
        }
    }
}
