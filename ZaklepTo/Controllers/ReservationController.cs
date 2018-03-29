using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZaklepTo.Infrastucture.DTO;
using ZaklepTo.Infrastucture.DTO.OnUpdate;
using ZaklepTo.Infrastucture.Services.Interfaces;

namespace ZaklepTo.API.Controllers
{
    [Route("api/reservations")]
    public class ReservationController : Controller
    {
        private IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _reservationService.GetAllAsync();

            return Ok(reservations);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetAllActiveReservations()
        {
            var activeReservations = await _reservationService.GetAllActiveAsync();

            return Ok(activeReservations);

        }

        [HttpGet("{reservationId}")]
        public async Task<IActionResult> GetSingleReservation(Guid reservationId)
        {
            var reservation = await _reservationService.GetAsync(reservationId);

            if (reservation == null)
                return NotFound();

            return Ok(reservation);
        }

        [HttpPut("{reservationId}/update")]
        public async Task<IActionResult> UpdateReservation([FromBody] ReservationOnUpdateDTO updatedReservation)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reservationToUpdate = await _reservationService.GetAsync(updatedReservation.Id);

            if (reservationToUpdate == null)
                return NotFound();

            await _reservationService.UpdateAsync(updatedReservation);

            return Ok();
        }

        [HttpDelete("reservationId/remove")]
        public async Task<IActionResult> RemoveReservation(Guid reservationId)
        {
            var reservationToRemove = await _reservationService.GetAsync(reservationId);

            if (reservationToRemove == null)
                return NotFound();

            await _reservationService.DeleteAsync(reservationId);

            return Ok();
        }
    }
}