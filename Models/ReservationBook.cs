using HotelResrvationDesktopApp.Exceptions;
using HotelResrvationDesktopApp.Services.ReservationConflictValidators;
using HotelResrvationDesktopApp.Services.ReservationCreators;
using HotelResrvationDesktopApp.Services.ReservationProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelResrvationDesktopApp.Models
{
    public class ReservationBook
    {
        private readonly IReservationProvider _reservationProvider;
        private readonly IReservationCreator _reservationCreator;
        private readonly IReservationConflictValidator _reservationConflictValidator;

        public ReservationBook(IReservationProvider reservationProvider, IReservationCreator reservationCreator, IReservationConflictValidator reservationConflictValidator)
        {
            _reservationProvider = reservationProvider;
            _reservationCreator = reservationCreator;
            _reservationConflictValidator = reservationConflictValidator;
        }

        public async Task<IEnumerable<Reservation>> GetallReservations()
        {
            return await _reservationProvider.GetAllReservations();
        }

        public async Task AddReservation(Reservation reservation)
        {
            Reservation conflictingReservation = await _reservationConflictValidator.GetConflictionReservation(reservation);


            if(conflictingReservation != null)
                throw new ReservationConflictException(conflictingReservation, reservation);

            await _reservationCreator.CreateReservation(reservation);
        }
    }
}
