using System.Data;

namespace CloudProject2025.Models
{
    public class ElencoFilm
    {
        public string NomeElenco { get; set; }

        public List<Film> ListaFilm { get; set; }

        public ElencoFilm()
        {
            
            ListaFilm = new List<Film>();
        }

        public void AggiungiFilm(Film film)
        {
            if (ListaFilm.Any(s => s.Id == film.Id))
                throw new Exception("Film già esistente");

            ListaFilm.Add(film);
        }

        public bool EliminaFilm(int filmId)
        {
            var film = ListaFilm.FirstOrDefault(f => f.Id == filmId);
            if (film != null)
            {
                ListaFilm.Remove(film);
                return true;
            }
            return false;
        }



        public Film[] Ricerca(string parametroRicerca)
        {
            List<Film> filmTrovati;

            filmTrovati = ListaFilm.Where(s => s.Title.Contains(parametroRicerca)
                                                || s.Genre.Contains(parametroRicerca)
                                                || s.Plot.Contains(parametroRicerca)
                                                || s.Director.Contains(parametroRicerca))
                    .ToList();

            return filmTrovati.ToArray();
        }

        



    }
}
