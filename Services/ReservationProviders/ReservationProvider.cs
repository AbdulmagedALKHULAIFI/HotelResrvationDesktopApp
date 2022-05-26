using HotelResrvationDesktopApp.DbContexts;
using HotelResrvationDesktopApp.DTOs;
using HotelResrvationDesktopApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelResrvationDesktopApp.Services.ReservationProviders
{
    public class ReservationProvider : IReservationProvider
    {
        private readonly ReservoomDbContexFactory _dbContextFactory;

        public ReservationProvider(ReservoomDbContexFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            using(ReservoomDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<ReservationDTO> reservationDTOs = await context.Reservations.ToListAsync();
                await Task.Delay(2000);
                return reservationDTOs.Select(r => ToReservation(r));
            }
        }

        private  Reservation ToReservation(ReservationDTO dto)
        {
            return new Reservation(new Room(dto.FloorNumber, dto.RoomNumber), dto.Username, dto.StartTime, dto.EndTime);
        }
    }
}
