using System;
using eTicketing.Data.Base;
using eTicketing.Models;

namespace eTicketing.Data.Services
{
	public class MovieService:EntityBaseRepository<Movie>,IMovieService
	{
		public MovieService(AppDbContext context) : base(context) { }
	}
}

