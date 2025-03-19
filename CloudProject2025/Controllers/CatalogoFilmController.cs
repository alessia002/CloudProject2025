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



        var viewModel = new CatalogoFilmRicercaFilmViewModel
        {
            ElencoFilm = new ElencoFilm
            {
                NomeElenco = "Risultati ricerca"

            }
        };

        if (film != null)
        {
            viewModel.ElencoFilm.ListaFilm = new List<Film> { film };
        }


        return View("RicercaFilm", viewModel);
    }

        [HttpPost]

        public async Task<IActionResult> AddFilm(int id,string title,string plot,int year,string genre,string director,float myRate)
        {
            var nuovoFilm = new Film
            {
                Id = id,
                Title = title,
                Plot = plot,
                Year = year,
                Genre = genre,
                Director = director,
                MyRate = myRate,

            };

        MemoriaStatica.ElencoFilm.ListaFilm.Add(nuovoFilm);
        return Redirect("/CatalogoFilm/CatalogoFilm");


        }



    }

