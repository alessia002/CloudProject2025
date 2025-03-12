using CloudProject2025.Models;
using Microsoft.AspNetCore.Mvc;

namespace CloudProject2025.Controllers;

public class CatalogoFilmController : Controller
{
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
}