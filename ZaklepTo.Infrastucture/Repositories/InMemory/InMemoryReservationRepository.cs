using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZaklepTo.Core.Domain;
using ZaklepTo.Core.Repositories;

namespace ZaklepTo.Infrastructure.Repositories.InMemory
{
    public class InMemoryReservationRepository : IReservationRepository
    {
        //nulls
        private readonly ISet<Reservation> _reservations = new HashSet<Reservation>
        {
            new Reservation(null, DateTime.UtcNow, DateTime.UtcNow, null, null, true),
            new Reservation(null, DateTime.UtcNow, DateTime.UtcNow, null, null, true),
            new Reservation(null, DateTime.UtcNow, DateTime.UtcNow, null, null, true)
        };

        public async Task AddAsync(Reservation reservation)
            => await Task.FromResult(_reservations.Add(reservation));

        public async Task DeleteAsync(Guid id)
        {
            var reservation = await GetAsync(id);
            _reservations.Remove(reservation);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
            => await Task.FromResult(_reservations);

        public async Task<Reservation> GetAsync(Guid id)
            => await Task.FromResult(_reservations.SingleOrDefault(x => x.Id == id));

        public async Task UpdateAsync(Reservation reservation)
        {
            var oldReservation = await GetAsync(reservation.Id);
            _reservations.Remove(oldReservation);
            _reservations.Add(reservation);
            await Task.CompletedTask;
        }
    }
}
