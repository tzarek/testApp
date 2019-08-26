using System;
using System.Collections.Generic;
using System.Linq;
using TestApp.Models;
using TestApp.Services.Abstractions;
using PagedList;

namespace TestApp.Repositories
{
    public class FilmsRepository : IFilmsRepository
    {
        public void CreateFilm(Film film)
        {           
            using (FilmContext db = new FilmContext())
            {
                db.Films.Add(film);
                db.SaveChanges();
            }            
        }

        public IEnumerable<Film> GetFilmsByPages(int pageNumber, int pageSize)
        {
            IPagedList<Film> films;
            using (FilmContext db = new FilmContext())
            {
                films = db.Films.OrderBy(f => f.Id).ToPagedList(pageNumber, pageSize);
            }
            return films;
        }

        public Film GetFilm(int id)
        {
            Film film;
            using (FilmContext db = new FilmContext())
            {
                film = db.Films.Where(f => f.Id == id).SingleOrDefault();
            }
            return film;
        }

        public void Update(Film film)
        {
            using (FilmContext db = new FilmContext())
            {
                db.Entry(film).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (FilmContext db = new FilmContext())
            {
                var film = db.Films.Where(f => f.Id == id).SingleOrDefault();
                db.Films.Remove(film);
                db.SaveChanges();
            }
        }
    }
}