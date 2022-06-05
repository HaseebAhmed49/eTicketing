using System;
using eTicketing.Models;

namespace eTicketing.Data.Services
{
	public interface IActorInterface
	{
		Task<IEnumerable<Actor>> GetAllASync();


		Task<Actor> GetByIdASync(int id);

		Task AddASync(Actor actor);

		Task<Actor> UpdateASync(int id,Actor newActor);

		Task DeleteASync(int id);
	}
}

