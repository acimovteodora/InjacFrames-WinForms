using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domen;

namespace SistemskeOperacije
{
    public class ZapamtiLajsnu : OpstaSistemskaOperacija
    {
        public Lajsna Lajsna { get; set; }
        protected override void IzvrsiKonkretnuOperaciju(IDomenskiObjekat objekat)
        {
            Lajsna = objekat as Lajsna;
            if (broker.SacuvajObjekat(objekat) != 1) Lajsna = null;
        }

        protected override void Validacija(IDomenskiObjekat objekat)
        {
            if (!(objekat is Lajsna))
                throw new Exception("Objekat nije lajsna!");
        }
    }
}
