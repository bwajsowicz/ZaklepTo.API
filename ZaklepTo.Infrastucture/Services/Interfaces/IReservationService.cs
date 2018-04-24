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
        Task<IEnumerable<ReservationDto>> GetAllAsync();
        Task<IEnumerable<ReservationDto>> GetAllBetweenDatesAsync(TimeInterval timeInterval);
        Task<IEnumerable<ReservationDto>> GetAllActiveAsync();
        Task<IEnumerable<ReservationDto>> GetAllActiveByCustomerAsync(string customersLogin);
        Task<IEnumerable<ReservationDto>> GetAllUncorfirmedReservationsAsync();
        Task<ReservationDto> GetAsync(Guid id);
        Task UpdateAsync(ReservationOnUpdateDto reservationDto);
        Task DeleteAsync(Guid id);
        Task DeactivateReservationAsync(Guid id);
        Task ConfirmReservationAsync(Guid id);
    }
}