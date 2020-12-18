using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domen;

namespace SistemskeOperacije
{
    public class PretraziLajsne : OpstaSistemskaOperacija
    {
        public List<Lajsna> Lajsne { get; set; }
        public string Kriterijum { get; set; }
        public PretraziLajsne(string kriterijum)
        {
            Kriterijum = kriterijum;
        }
        protected override void IzvrsiKonkretnuOperaciju(IDomenskiObjekat objekat)
        {
            Lajsne = broker.VratiObjekteKriterijum(Kriterijum, objekat).OfType<Lajsna>().ToList();
        }

        protected override void Validacija(IDomenskiObjekat objekat)
        {
            if (!(objekat is Lajsna))
                throw new Exception("Objekat nije lajsna!");
        }
    }
}
