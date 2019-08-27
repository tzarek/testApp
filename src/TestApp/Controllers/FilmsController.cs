﻿using System.Web;
using System.Web.Mvc;
using TestApp.Models;
using TestApp.Services.Abstractions;
using Microsoft.AspNet.Identity;

namespace TestApp.Controllers
{    
    public class FilmsController : Controller
    {
        private readonly IFilmsRepository _filmsRepository;

        public FilmsController(IFilmsRepository filmsRepository)
        {
            _filmsRepository = filmsRepository;
        }

        [HttpGet]
        public ActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            var films = _filmsRepository.GetFilmsByPages(pageNumber, pageSize);
            return View("Index", films);
        }

        
        [HttpGet]
        public ActionResult Get(int id)
        {          
            var film = _filmsRepository.GetFilm(id);
            
            return View("Film", film);
        }


        [HttpGet]
        public ActionResult Create()
        {          
            return View("Create");
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
            
            _filmsRepository.CreateFilm(film);
            return RedirectToAction("Index");            
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var film =  _filmsRepository.GetFilm(id);
            return View("Edit", film);            
        }

        [HttpPost]
        public ActionResult Edit(Film film, HttpPostedFileBase image)
        {            

            if (image != null)
            {
                film.Poster = new byte[image.ContentLength];
                image.InputStream.Read(film.Poster, 0, image.ContentLength);
            }
            else
            {
                film.Poster = _filmsRepository.GetFilm(film.Id).Poster;
            }
            film.UserId = User.Identity.GetUserId();                      
            
            _filmsRepository.Update(film);
            return RedirectToAction("Index", film.Id);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {            
            _filmsRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}