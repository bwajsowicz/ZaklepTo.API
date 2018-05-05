using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZaklepTo.Infrastructure.DTO;
using ZaklepTo.Infrastructure.DTO.EntryData;
using ZaklepTo.Infrastructure.DTO.OnUpdate;
using ZaklepTo.Infrastructure.DTO.OnCreate;

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
        Task RegisterReservation(ReservationOnCreateDto reservationDto);
        Task UpdateAsync(ReservationOnUpdateDto reservationDto, Guid reservationId);
        Task DeleteAsync(Guid reservationId);
        Task DeactivateReservationAsync(Guid reservationId);
        Task ConfirmReservationAsync(Guid reservationId);
    }
}