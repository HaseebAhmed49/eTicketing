using System;
using eTicketing.Models;
using Microsoft.EntityFrameworkCore;

namespace eTicketing.Data.Services
{
    public class ActorService : IActorInterface
    {
        private readonly AppDbContext _context;

        public ActorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddASync(Actor actor)
        {
            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Actor>> GetAllASync()
        {
            var result = await _context.Actors.ToListAsync();
            return result;
        }

        //public async Task<IEnumerable<Actor>> GetAll()
        //{
        //    var result = await _context.Actors.ToListAsync();
        //    return result;
        //}

        public async Task<Actor> GetByIdASync(int id)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(a=> a.Id==id);
            return actor;
        }

        public Actor Update(int id, Actor newActor)
        {
            throw new NotImplementedException();
        }
    }
}

