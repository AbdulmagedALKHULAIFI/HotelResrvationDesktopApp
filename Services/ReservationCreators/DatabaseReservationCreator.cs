using HotelResrvationDesktopApp.DbContexts;
using HotelResrvationDesktopApp.DTOs;
using HotelResrvationDesktopApp.Models;
using HotelResrvationDesktopApp.Services.ReservationProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelResrvationDesktopApp.Services.ReservationCreators
{
    public class DatabaseReservationCreator : IReservationCreator
    {
        private readonly ReservoomDbContexFactory _dbContextFactory;

        public DatabaseReservationCreator(ReservoomDbContexFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task  CreateReservation(Reservation reservation)
        {
            using (ReservoomDbContext context = _dbContextFactory.CreateDbContext())
            {
                ReservationDTO reservationDTO = ToReservationDTO(reservation);

                context.Reservations.Add(reservationDTO);
                await context.SaveChangesAsync();
            }
        }

        private ReservationDTO ToReservationDTO(Reservation reservation)
        {
            return new ReservationDTO()
            {
                FloorNumber = reservation.Room?.FloorNumber ?? 0,
                RoomNumber = reservation.Room?.RoomNumber ?? 0,
                Username = reservation.Username,
                StartTime = reservation.StartTime,
                EndTime = reservation.EndTime
            };
        }
    }
}
