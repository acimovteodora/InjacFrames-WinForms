using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domen;

namespace SistemskeOperacije
{
    public class PretraziKompanije : OpstaSistemskaOperacija
    {
        public List<Kompanija> Kompanije { get; set; }
        public string Kriterijum { get; set; }
        public PretraziKompanije(string kriterijum)
        {
            Kriterijum = kriterijum;
        }
        protected override void IzvrsiKonkretnuOperaciju(IDomenskiObjekat objekat)
        {
            Kompanije = broker.VratiObjekteKriterijum(Kriterijum,objekat).OfType<Kompanija>().ToList();
        }

        protected override void Validacija(IDomenskiObjekat objekat)
        {
            if (!(objekat is Kompanija))
                throw new Exception("Objekat nije kompanija!");
        }
    }
}
