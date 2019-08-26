using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApp.Models;
using TestApp.Repositories;
using TestApp.Services.Abstractions;
using Microsoft.AspNet.Identity;
using PagedList;
using PagedList.Mvc;

namespace TestApp.Controllers
{    
    public class FilmsController : Controller
    {                       
        public ActionResult Index(int? page)

        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            List<Film> films = new List<Film>();
            using (FilmContext db = new FilmContext())
            {
                films = db.Films.ToList();
            }
                return View(films.ToPagedList(pageNumber, pageSize));
        }

             
        public ActionResult Get(int id)
        {
            Film film = new Film();
            using (FilmContext db = new FilmContext())
            {
                film = db.Films.Where(f => f.Id == id).SingleOrDefault();
            }
            return View("Film", film);
        }


        [HttpGet]
        public ActionResult Create()
        {          
            return View();
        }

        [HttpPost]
        public ActionResult Create(Film film, HttpPostedFileBase image)
        {
            if (image != null)
            { 
                film.Poster = new byte[image.ContentLength];
                image.InputStream.Read(film.Poster, 0, image.ContentLength);
            }

            film.UserId = User.Identity.GetUserId();

            IFilmsRepository filmsRepository = new FilmsRepository();
            filmsRepository.CreateFilm(film);
            return RedirectToAction("Index");            
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Film film = new Film();
            IFilmsRepository filmsRepository = new FilmsRepository();
            film = filmsRepository.GetFilm(id);
            return View("Edit", film);            
        }

        [HttpPost]
        public ActionResult Edit(Film film, HttpPostedFileBase image)
        {
            IFilmsRepository filmsRepository = new FilmsRepository();

            if (image != null)
            {
                film.Poster = new byte[image.ContentLength];
                image.InputStream.Read(film.Poster, 0, image.ContentLength);
            }
            else
            {
                film.Poster = filmsRepository.GetFilm(film.Id).Poster;
            }
            film.UserId = User.Identity.GetUserId();                      
            
            filmsRepository.Update(film);
            return RedirectToAction("Index", film.Id);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            IFilmsRepository filmsRepository = new FilmsRepository();
            filmsRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}