using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLib
{
    public abstract class MediaItem
    {
        private int _id;
        private string _naziv;
        private int _trajanje;
        private int _godina;

        public int Id
        {
            get { return _id; }
            set
            {
                if (value >= 0)
                {
                    _id = value; 
                }
                else
                { 
                    throw new ArgumentException("ID mora biti pozitivan."); 
                } 
            }
        }

        public string Naziv
        {
            get { return _naziv; }
            set 
            { 
                if (!string.IsNullOrWhiteSpace(value)) 
                { 
                    _naziv = value; 
                } 
                else 
                { 
                    throw new ArgumentException("Naziv ne može biti prazan.");  
                }
            }
        }

        public int Trajanje 
        { 
            get { return _trajanje; } 
            set 
            { 
                if (value > 0) 
                { 
                    _trajanje = value; 
                } 
                else 
                { 
                    throw new ArgumentException("Trajanje mora biti veće od 0."); 
                }
            }
        }

        public int Godina 
        { 
            get { return _godina; } 
            set 
            { 
                if (value >= 1800 && value <= DateTime.Now.Year) 
                {
                    _godina = value; 
                }
                else
                {
                    throw new ArgumentException($"Godina mora biti između 1800 i {DateTime.Now.Year}.");
                }
            }
        }

        public MediaItem() { }


        public MediaItem(int id, string naziv, int trajanje, int godina)
        {
            Id = id;
            Naziv = naziv;
            Trajanje = trajanje;
            Godina = godina;
        }

        public virtual string GetInfo()
        {
            return $"{Naziv}, {Godina} ({Trajanje} min)";
        }
    }
}

