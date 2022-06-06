using System;
using eTicketing.Data.Base;
using eTicketing.Data.ViewModels;
using eTicketing.Models;
using Microsoft.EntityFrameworkCore;

namespace eTicketing.Data.Services
{
	public class MovieService:EntityBaseRepository<Movie>,IMovieService
	{
        private readonly AppDbContext _context;

		public MovieService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Movie> GetMovieByIdASync(int id)
        {
            var movieDetails = await _context.Movies
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n=> n.Id == id);

            return movieDetails;

        }

        public async Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues()
        {
            var response = new NewMovieDropdownsVM()
            {
                Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync(),
            };
            return response;
        }
    }
}

