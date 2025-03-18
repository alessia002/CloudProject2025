using CloudProject2025.Models;
using CloudProject2025.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudProject2025.Controllers;

public class CatalogoFilmController : Controller
{
    private readonly OmdbService _omdbService;

    // ?? Il costruttore riceve OmdbService tramite Dependency Injection
    public CatalogoFilmController(OmdbService omdbService)
    {
        _omdbService = omdbService ?? throw new ArgumentNullException(nameof(omdbService));
    }


    public IActionResult CatalogoFilm()
    {
        CatalogoFilmElencoFilmViewModel vm = new CatalogoFilmElencoFilmViewModel();
        vm.ElencoFilm = MemoriaStatica.ElencoFilm;
        return View(vm);
    }


   
    

    [HttpPost]
    public async Task<IActionResult> Search(string title, int year)
    {
        var film = await _omdbService.GetFilmAsync(title, year);

        if (film == null)
        {
            return NotFound("Film non trovato");
        }

        var viewModel = new CatalogoFilmRicercaFilmViewModel
        {
            ElencoFilm = new ElencoFilm
            {
                NomeElenco = "Risultati ricerca",
                ListaFilm = new List<Film> { film }
            }
        };

        return View("RicercaFilm", viewModel);
    }

}