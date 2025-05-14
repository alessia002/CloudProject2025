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
    public async Task<IActionResult> AddFilm(string title, string plot, string year, string genre,
        string director, float myRate)
    {
        var nuovoFilm = new Film
        {
            Id = MemoriaStatica.GetNewId(),
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

    [HttpPost]
    public async Task<IActionResult> UpdateFilm(List<Film> films)
    {
        foreach (var updatedFilm in films)
        {
            var film = MemoriaStatica.ElencoFilm.ListaFilm.FirstOrDefault(f => f.Id == updatedFilm.Id);

            if (film == null)
            {
                return NotFound(new { message = "Film non trovato" });
            }

            // Aggiorno i dettagli del film
            film.Title = updatedFilm.Title;
            film.Plot = updatedFilm.Plot;
            film.Year = updatedFilm.Year;
            film.Genre = updatedFilm.Genre;
            film.Director = updatedFilm.Director;
            film.MyRate = updatedFilm.MyRate;
        }

        return Redirect("/Home/Index");
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteFilm(List<int> selectedFilms)
    {
        if (selectedFilms == null || !selectedFilms.Any())
        {
            return BadRequest(new { message = "Nessun film selezionato per l'eliminazione" });
        }

        bool allDeleted = true;

        foreach (var filmId in selectedFilms)
        {
            if (!MemoriaStatica.ElencoFilm.EliminaFilm(filmId))
            {
                allDeleted = false;
            }
        }

        if (!allDeleted)
        {
            return NotFound(new { message = "Alcuni film non sono stati trovati e non sono stati eliminati." });
        }

        return RedirectToAction("CatalogoFilm");
    }

    [HttpPost]
    public IActionResult GestisciFilm(string actionType, List<int> selectedFilms, List<Film> films)
    {
        if (actionType.Equals("update"))
        {
            UpdateFilm(films);
            return Redirect("/Home/Index");
        }
        else if (actionType.Equals("delete"))
        {
            DeleteFilm(selectedFilms);
            return RedirectToAction("CatalogoFilm");
        }

        return BadRequest("Azione non valida.");
    }


    }