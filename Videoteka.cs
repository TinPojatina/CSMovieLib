using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;


namespace MovieLib
{
    public partial class Videoteka : Form
    {
        public Videoteka()
        {
            InitializeComponent();
            GetMovies();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Videoteka_Load(object sender, EventArgs e)
        {

        }

        private async void btnFilter_Click(object sender, EventArgs e)
        {
            lbAllMovies.Items.Clear();

            var iznenadi = await Filmovi.RandomMovie();

            foreach (var film in iznenadi)
            {
                lbAllMovies.Items.Add(film);
            }
        }

        private async void btnReset_Click(object sender, EventArgs e)
        {
            lbAllMovies.Items.Clear();

            var reset = await Filmovi.GetFilmoviAsync();
            foreach (var film in reset)
            {
                lbAllMovies.Items.Add(film);
            }
        }
        private void btnRent_Click(object sender, EventArgs e)
        {
            Korisnik korisnik = new Korisnik(lbAllMovies.Items);
            korisnik.Show();
        }

        private void btnRentOne_Click(object sender, EventArgs e)
        {
            var odabraniFilm = lbAllMovies.Items[lbAllMovies.SelectedIndex];
            Korisnik korisnik = new Korisnik((Film)odabraniFilm);
            korisnik.Show();
        }

        private async void GetMovies()
        {
            lbAllMovies.Items.Clear();

            var filmovi = await Filmovi.GetFilmoviAsync();

            Console.WriteLine($"Ukupan broj dohvaćenih filmova: {filmovi.Count}");

            foreach (var film in filmovi)
            {
                lbAllMovies.Items.Add(film);
                Console.WriteLine($"Dodani film: {film.Naziv} - {film.Zanr}");
            }
        }
    }



}




