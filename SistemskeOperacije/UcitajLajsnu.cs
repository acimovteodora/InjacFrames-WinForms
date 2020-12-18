using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domen;

namespace SistemskeOperacije
{
    public class UcitajLajsnu : OpstaSistemskaOperacija
    {
        public Lajsna Lajsna { get; set; }
        protected override void IzvrsiKonkretnuOperaciju(IDomenskiObjekat objekat)
        {
            Lajsna = (Lajsna)broker.VratiObjekat(objekat);
        }

        protected override void Validacija(IDomenskiObjekat objekat)
        {
            if (!(objekat is Lajsna))
                throw new Exception("Objekat nije lajsna!");
        }
    }
}
