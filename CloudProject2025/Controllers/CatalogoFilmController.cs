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


    [HttpGet]
    public IActionResult SalvaFilm(int Id)
    {
        CatalogoFilmSalvaFilmViewModel vm = new CatalogoFilmSalvaFilmViewModel();
            
        if (Id == 0)
        {
            vm.FilmDaSalvare = new Models.Film();
            vm.NuovoFilm = true;
        }
        else
        {
            //vm.FilmDaSalvare = MemoriaStatica.ElencoFilm.DaiPersonaDaNumeroDiTelefono(NumeroDiTelefono);
            vm.NuovoFilm = false;
        }
            
        return View(vm);
    }
    
    [HttpPost]
    public IActionResult SalvaFilm(Film FilmDaSalvare, bool NuovoFilm)
    {
        if (NuovoFilm == true)
        {
            MemoriaStatica.ElencoFilm.AggiungiFilm(FilmDaSalvare);
        }
        else
        {
            MemoriaStatica.ElencoFilm.EliminaFilm(FilmDaSalvare);
            MemoriaStatica.ElencoFilm.AggiungiFilm(FilmDaSalvare);
        }

        return Redirect("/CatalogoFilm/CatalogoFilm");
    }

    [HttpPost]
    public async Task<IActionResult> Search(string title)
    {
        var film = await _omdbService.GetFilmAsync(title);

        if (film == null)
        {
            return NotFound("Film non trovato");
        }

        var viewModel = new CatalogoFilmElencoFilmViewModel
        {
            ElencoFilm = new ElencoFilm
            {
                NomeElenco = "Risultati ricerca",
                ListaFilm = new List<Film> { film }
            }
        };

        return View("CatalogoFilm", viewModel);
    }

}