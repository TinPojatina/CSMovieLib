using System;

namespace MovieLib
{
    public class Film : MediaItem
    {
        public string Zanr { get; set; }

        public Film() : base() { }

        public Film(int id, string naziv, int trajanje, int godina, string zanr)
            : base(id, naziv, trajanje, godina)
        {
            Zanr = zanr;
        }

        public Film(string naziv, int trajanje, int godina, string zanr)
            : base(0, naziv, trajanje, godina)
        {
            Zanr = zanr;
        }

        public override string GetInfo()
        {
            return $"{base.GetInfo()}, Žanr: {Zanr}";
        }

        public override string ToString()
        {
            return this.Naziv;
        }
    }
}
