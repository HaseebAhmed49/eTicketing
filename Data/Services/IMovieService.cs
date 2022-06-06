using System;
using eTicketing.Data.Base;
using eTicketing.Models;

namespace eTicketing.Data.Services
{
	public interface IMovieService:IEntityBaseRepository<Movie>
	{
		Task<Movie> GetMovieByIdASync(int id);
	}
}

