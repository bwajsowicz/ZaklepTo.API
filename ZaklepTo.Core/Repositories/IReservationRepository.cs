using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZaklepTo.Core.Domain;

namespace ZaklepTo.Core.Repositories
{
    public interface IReservationRepository
    {
        Task<Reservation> GetAsync(Guid id);
        Task<IEnumerable<Reservation>> GetAllAsync();
        Task AddAsync(Reservation reservation);
        Task UpdateAsync(Reservation reservation);
        Task DeleteAsync(Guid id);
    }
}