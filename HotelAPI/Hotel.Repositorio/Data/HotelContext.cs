using Hotel.Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Repositorio.Data
{
    public class HotelContext : DbContext
    {
        public HotelContext(DbContextOptions<HotelContext> options) : base(options) { }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Quarto> Quarto { get; set; }
        public DbSet<Reserva> Reserva { get; set; }
    }
}
