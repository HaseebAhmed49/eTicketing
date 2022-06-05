using System;
using eTicketing.Data.Base;
using eTicketing.Models;

namespace eTicketing.Data.Services
{
	public interface IActorInterface: IEntityBaseRepository<Actor>
	{
		//Task<IEnumerable<Actor>> GetAllASync();

		//Task<Actor> GetByIdASync(int id);

		//Task AddASync(Actor actor);

		//Task<Actor> UpdateASync(int id,Actor newActor);

		//Task DeleteASync(int id);
	}
}

