using HotelResrvationDesktopApp.DbContexts;
using HotelResrvationDesktopApp.DTOs;
using HotelResrvationDesktopApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelResrvationDesktopApp.Services.ReservationConflictValidators
{
    public class ReservationReservationValidator : IReservationConflictValidator
    {
        private readonly ReservoomDbContexFactory _dbContextFactory;

        public ReservationReservationValidator(ReservoomDbContexFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Reservation> GetConflictionReservation(Reservation reservation)
        {
            using (ReservoomDbContext context = _dbContextFactory.CreateDbContext())
            {
                ReservationDTO reservationDTO =  await context.Reservations
                    .Where(r => r.FloorNumber == reservation.Room.FloorNumber)
                    .Where(r => r.RoomNumber== reservation.Room.RoomNumber)
                    .Where(r => r.EndTime > reservation.StartTime)
                    .Where(r => r.StartTime < reservation.EndTime)
                    .FirstOrDefaultAsync();

                if (reservationDTO == null)
                    return null;

                return ToReservation(reservationDTO);
            }
        }

        private Reservation ToReservation(ReservationDTO dto)
        {
            return new Reservation(new Room(dto.FloorNumber, dto.RoomNumber), dto.Username, dto.StartTime, dto.EndTime);
        }
    }
}
