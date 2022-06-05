using System;
using eTicketing.Models;

namespace eTicketing.Data.Services
{
	public interface IActorInterface
	{
		Task<IEnumerable<Actor>> GetAllASync();


		Task<Actor> GetByIdASync(int id);

		Task AddASync(Actor actor);

		Actor Update(int id,Actor newActor);

		void Delete(int id);
	}
}

