using System.Data.Entity;
using Data.Entities;

namespace Data
{
    public class ATMContext : DbContext
    {
        public DbSet<CardInfo> Cards { get; set; }
        public DbSet<LogInfo> Logs { get; set; }
    }
}
