using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using train_booking.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace train_booking.Data
{
    public class TrainBookingContext : IdentityDbContext<User>
    {
        public TrainBookingContext(DbContextOptions<TrainBookingContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> User { get; set; }
        public DbSet<Train> Train { get; set; }
        public DbSet<TrainDriver> TrainDriver { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Route> Route { get; set; }
        public DbSet<Dispatcher> Dispatcher { get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<Wagon> Wagon { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
