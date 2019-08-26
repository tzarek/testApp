using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Models;

namespace TestApp.Services.Abstractions
{
    public interface IFilmsRepository
    {
        IEnumerable<Film> GetFilmsByPages(int pageNumber, int pageSize);

        Film GetFilm(int id);

        void CreateFilm(Film film);

        void Update(Film film);

        void Delete(int id);
    }
}
