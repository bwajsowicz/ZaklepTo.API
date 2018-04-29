using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZaklepTo.Core.Domain;
using ZaklepTo.Core.Repositories;
using ZaklepTo.Infrastructure.EntityFramwerork;

namespace ZaklepTo.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ZaklepToContext _context;

        public ReservationRepository(ZaklepToContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid reservationId)
        {
            var reservationToRemove = await _context.Reservations.FindAsync(reservationId);
            _context.Reservations.Remove(reservationToRemove);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
            => await _context.Reservations.ToListAsync();

        public async Task<Reservation> GetAsync(Guid reservationId)
            => await _context.Reservations.FindAsync(reservationId);

        public async Task UpdateAsync(Reservation reservation)
        {
            _context.Update(reservation);
            await _context.SaveChangesAsync();
        }
    }
}
