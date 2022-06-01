using System;
using eTicketing.Models;

namespace eTicketing.Data.Services
{
	public interface IActorInterface
	{
		IEnumerable<Actor> GetAll();

		Actor GetById(int id);

		void Add(Actor actor);

		Actor Update(int id,Actor newActor);

		void Delete(int id);
	}
}

