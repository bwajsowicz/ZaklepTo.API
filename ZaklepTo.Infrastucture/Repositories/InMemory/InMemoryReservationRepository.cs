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
        private static readonly ISet<Reservation> Reservations = new HashSet<Reservation>();

        public async Task AddAsync(Reservation reservation)
            => await Task.FromResult(Reservations.Add(reservation));

        public async Task DeleteAsync(Guid id)
        {
            var reservation = await GetAsync(id);
            Reservations.Remove(reservation);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
            => await Task.FromResult(Reservations);

        public async Task<Reservation> GetAsync(Guid id)
            => await Task.FromResult(Reservations.SingleOrDefault(x => x.Id == id));

        public async Task UpdateAsync(Reservation reservation)
        {
            var oldReservation = await GetAsync(reservation.Id);
            Reservations.Remove(oldReservation);
            Reservations.Add(reservation);
            await Task.CompletedTask;
        }
    }
}
