using Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemskeOperacije
{
    public class PrijavaZaposlenog : OpstaSistemskaOperacija
    {
        public Zaposleni Zaposleni { get; set; }
        public AdministratorBazePodataka Administrator { get; set; }
        public MenadzerProdaje Menadzer { get; set; }
        protected override void IzvrsiKonkretnuOperaciju(IDomenskiObjekat objekat)
        {
            Zaposleni = broker.VratiObjekat(objekat) as Zaposleni;
            Administrator = broker.VratiObjekat(objekat) as AdministratorBazePodataka;
            Menadzer = broker.VratiObjekat(objekat) as MenadzerProdaje;
        }

        protected override void Validacija(IDomenskiObjekat objekat)
        {
            if (!(objekat is Zaposleni))
                throw new Exception("Objekat nije zaposleni!");
        }
    }
}
