using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZaklepTo.Infrastructure.DTO;
using ZaklepTo.Infrastructure.DTO.EntryData;
using ZaklepTo.Infrastructure.DTO.OnUpdate;

namespace ZaklepTo.Infrastructure.Services.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDTO>> GetAllAsync();
        Task<IEnumerable<ReservationDTO>> GetAllBetweenDatesAsync(TimeInterval timeInterval);
        Task<IEnumerable<ReservationDTO>> GetAllActiveAsync();
        Task<IEnumerable<ReservationDTO>> GetAllActiveByCustomerAsync(string customersLogin);
        Task<IEnumerable<ReservationDTO>> GetAllUncorfirmedReservationsAsync();
        Task<ReservationDTO> GetAsync(Guid id);
        Task UpdateAsync(ReservationOnUpdateDTO reservationDto);
        Task DeleteAsync(Guid id);
        Task DeactivateReservationAsync(Guid id);
        Task ConfirmReservationAsync(Guid id);
    }
}