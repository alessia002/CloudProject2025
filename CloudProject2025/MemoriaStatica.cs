using CloudProject2025.Models;

namespace CloudProject2025;

public class MemoriaStatica
{
    private static int idFilm = 0;
    
    public static List<Film> ListaFilmStatica = new List<Film>();

    public static Models.ElencoFilm ElencoFilm = new Models.ElencoFilm();

    public static int GetNewId()
    {
        return ++idFilm;
    }

}