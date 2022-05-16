using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelResrvationDesktopApp.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HotelResrvationDesktopApp.DbContexts
{
    public class ReservoomDbContext : DbContext
    {
        public ReservoomDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ReservationDTO> Reservations { get; set; }
    }
}
