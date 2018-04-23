using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using ZaklepTo.Core.Domain;
using ZaklepTo.Core.Repositories;
using ZaklepTo.Infrastructure.DTO;
using ZaklepTo.Infrastructure.DTO.EntryData;
using ZaklepTo.Infrastructure.DTO.OnUpdate;
using ZaklepTo.Infrastructure.Services.Interfaces;

namespace ZaklepTo.Infrastructure.Services.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly IMapper _mapper;
        private readonly IReservationRepository _reservationRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IRestaurantRepository _restaurantRepository;

        public ReservationService(IMapper mapper, IReservationRepository reservationRepository, ICustomerRepository customerRepository, IRestaurantRepository restaurantRepository)
        {
            _mapper = mapper;
            _reservationRepository = reservationRepository;
            _customerRepository = customerRepository;
            _restaurantRepository = restaurantRepository;
        }

        public async Task<IEnumerable<ReservationDTO>> GetAllAsync()
        {
            var reservations = await _reservationRepository.GetAllAsync();
            return reservations.Select(x => _mapper.Map<Reservation, ReservationDTO>(x));
        }

        public async Task<IEnumerable<ReservationDTO>> GetAllBetweenDatesAsync(TimeInterval timeInterval)
        {
            var reservations = await _reservationRepository.GetAllAsync();
            var reservationsBetweenDates = reservations
                .Where(x => x.DateStart > timeInterval.DateStart)
                .Where(x => x.DateEnd < timeInterval.DateEnd);

            return reservationsBetweenDates.Select(x => _mapper.Map<Reservation, ReservationDTO>(x));
        }

        public async Task<IEnumerable<ReservationDTO>> GetAllActiveAsync()
        {
            var reservations = await _reservationRepository.GetAllAsync();
            var activeReservations = reservations.Where(x => x.IsActive);

            return activeReservations.Select(x => _mapper.Map<Reservation, ReservationDTO>(x));
        }

        public async Task<IEnumerable<ReservationDTO>> GetAllActiveByCustomerAsync(string customersLogin)
        {
            var reservations = await _reservationRepository.GetAllAsync();
            var activeReservationsOfCustomer = reservations
                .Where(x => x.Customer.Login == customersLogin)
                .Where(x => x.IsActive);

            return activeReservationsOfCustomer.Select(x => _mapper.Map<Reservation, ReservationDTO>(x));
        }

        public async Task<IEnumerable<ReservationDTO>> GetAllUncorfirmedReservationsAsync()
        {
            var reservations = await _reservationRepository.GetAllAsync();
            var unconfirmedReservations = reservations.Where(x => x.IsConfirmed == false);

            return unconfirmedReservations.Select(x => _mapper.Map<Reservation, ReservationDTO>(x));
        }

        public async Task<ReservationDTO> GetAsync(Guid id)
        {
            var reservation = await _reservationRepository.GetAsync(id);

            return _mapper.Map<Reservation, ReservationDTO>(reservation);
        }

        public async Task UpdateAsync(ReservationOnUpdateDTO reservationDto)
        {
            var reservation = await _reservationRepository.GetAsync(reservationDto.Id);
            var restaurant = await _restaurantRepository.GetAsync(reservationDto.Restaurant.Id);
            var customer = await _customerRepository.GetAsync(reservationDto.Customer.Login);

            var table = new Table(reservationDto.Table.NumberOfSeats,
                reservationDto.Table.Coordinates);

            var updatedReservation = new Reservation(restaurant, reservationDto.DateStart, reservationDto.DateEnd,
                table, customer, reservationDto.IsConfirmed, reservationDto.IsActive);

            await _reservationRepository.UpdateAsync(updatedReservation);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _reservationRepository.DeleteAsync(id);
        }

        public async Task DeactivateReservationAsync(Guid id)
        {
            var reservation = await _reservationRepository.GetAsync(id);
            var deactivatedReservation = new Reservation(reservation.Restaurant, reservation.DateStart, reservation.DateEnd,
                reservation.Table, reservation.Customer, reservation.IsConfirmed, false);

            await _reservationRepository.UpdateAsync(deactivatedReservation);
        }

        public async Task ConfirmReservationAsync(Guid id)
        {
            var reservation = await _reservationRepository.GetAsync(id);
            var deactivatedReservation = new Reservation(reservation.Restaurant, reservation.DateStart, reservation.DateEnd,
                reservation.Table, reservation.Customer, true, reservation.IsActive);

            await _reservationRepository.UpdateAsync(deactivatedReservation);
        }
    }
}