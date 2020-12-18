using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domen
{
    [Serializable]
    public class ZahtevKlijenta
    {
        public Operacija Operacija { get; set; }
        public object Objekat { get; set; }
    }

    public enum Operacija
    {
        PrijavaZaposlenog,
        PrijavaAdministratora,
        PrijavaMenadzera,
        ZapamtiLajsnu,
        PretraziLajsne,
        UcitajLajsnu,
        ZapamtiKompaniju,
        IzmeniKompaniju,
        PretraziKompanije,
        UcitajKompaniju,
        UcitajLisuKompanija,
        UcitajListuLajsni,
        ZapamtiPorudzbenicu,
        PretraziPorudzbenice,
        UcitajPorudzbenicu,
        UcitajListuPorudzbenica,
        ObrisiPorudzbenicu,
        IzmeniPorudzbenicu,
        Odjava
    }
}
