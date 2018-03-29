using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZaklepTo.Infrastucture.DTO;
using ZaklepTo.Infrastucture.DTO.OnUpdate;

namespace ZaklepTo.Infrastucture.Services.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDTO>> GetAllAsync();
        Task<IEnumerable<ReservationDTO>> GetAllActiveAsync();
        Task<ReservationDTO> GetAsync(Guid id);
        Task UpdateAsync(ReservationOnUpdateDTO reservationDto);
        Task DeleteAsync(Guid id);
        //TODO finish interface
    }
}