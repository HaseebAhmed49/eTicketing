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

        public async Task DeleteASync(int id)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(a => a.Id == id);
            if (actor != null)
            {
                _context.Actors.Remove(actor);
                await _context.SaveChangesAsync();
            }
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

        public async Task<Actor> UpdateASync(int id, Actor newActor)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(a => a.Id == id);
            if(actor!=null)
            {
                actor.FullName = newActor.FullName;
                actor.ProfilePictureUrl = newActor.ProfilePictureUrl;
                actor.Bio = newActor.Bio;

                _context.Update(actor);
                await _context.SaveChangesAsync();
                return newActor;
            }
            return actor;
        }
    }
}

