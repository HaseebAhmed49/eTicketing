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

        public async Task AddNewMovieASync(NewMovieVM newMovieVM)
        {
            Movie movie = new Movie()
            {
                Name = newMovieVM.Name,
                Description = newMovieVM.Description,
                Price = newMovieVM.Price,
                ImageUrl = newMovieVM.ImageUrl,
                CinemaId = newMovieVM.CinemaId,
                StartDate = newMovieVM.StartDate,
                EndTime = newMovieVM.EndTime,
                ProducerId = newMovieVM.ProducerId,
                MovieCategory = newMovieVM.MovieCategory
            };
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            // Add Movie Actors
            foreach(var actorId in newMovieVM.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    ActorId = actorId,
                    MovieId = movie.Id
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
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

        public async Task UpdateMovieASync(NewMovieVM newMovieVM)
        {
            var dbMovie = await _context.Movies.FirstOrDefaultAsync(n=>n.Id == newMovieVM.Id);
            if(dbMovie!=null)
            {
                dbMovie.Name = newMovieVM.Name;
                dbMovie.Description = newMovieVM.Description;
                dbMovie.Price = newMovieVM.Price;
                dbMovie.ImageUrl = newMovieVM.ImageUrl;
                dbMovie.CinemaId = newMovieVM.CinemaId;
                dbMovie.StartDate = newMovieVM.StartDate;
                dbMovie.EndTime = newMovieVM.EndTime;
                dbMovie.ProducerId = newMovieVM.ProducerId;
                dbMovie.MovieCategory = newMovieVM.MovieCategory;
                await _context.SaveChangesAsync();

                // Add Movie Actors

                var existingActorsDb = _context.Actors_Movies.Where(m => m.MovieId == newMovieVM.Id).ToList();
                _context.Actors_Movies.RemoveRange(existingActorsDb);
                await _context.SaveChangesAsync();

                foreach (var actorId in newMovieVM.ActorIds)
                {
                    var newActorMovie = new Actor_Movie()
                    {
                        ActorId = actorId,
                        MovieId = newMovieVM.Id
                    };
                    await _context.Actors_Movies.AddAsync(newActorMovie);
                }
                await _context.SaveChangesAsync();
            }

        }
    }
}

