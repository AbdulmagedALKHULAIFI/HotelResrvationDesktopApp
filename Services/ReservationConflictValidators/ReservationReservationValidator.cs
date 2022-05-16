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
                return await context.Reservations.Select(r => ToReservation(r)).FirstOrDefaultAsync(r => r.Conflicts(reservation));
            }
        }

        private Reservation ToReservation(ReservationDTO dto)
        {
            return new Reservation(new Room(dto.FloorNumber, dto.RoomNumber), dto.Username, dto.StartTime, dto.EndTime);
        }
    }
}
