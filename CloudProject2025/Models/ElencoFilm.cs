using System.Data;

namespace CloudProject2025.Models
{
    public class ElencoFilm
    {
        public string NomeElenco { get; set; }

        public List<Film> ListaFilm { get; set; }

        public ElencoFilm()
        {
            NomeElenco = "Test ElencoFilm MVC";
            ListaFilm = new List<Film>();
        }

        public void AggiungiFilm(Film film)
        {
            if (ListaFilm.Any(s => s.Id == film.Id))
                throw new Exception("Film già esistente");

            ListaFilm.Add(film);
        }

        public void EliminaFilm(Film film)
        {
            ListaFilm.Remove(film);
        }


        public Film[] Ricerca(string parametroRicerca)
        {
            List<Film> filmTrovati;

            filmTrovati = ListaFilm.Where(s => s.Name.Contains(parametroRicerca)
                                                || s.Genre.Contains(parametroRicerca)
                                                || s.Plot.Contains(parametroRicerca)
                                                || s.Director.Contains(parametroRicerca))
                    .ToList();

            return filmTrovati.ToArray();
        }

        public Film[] FilmPerData(String released)
        {
            return ListaFilm.Where(s => s.Released == released).ToArray();
        }



    }
}
