using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservroom.DbContexts
{
    public class ReservationDbContextFactory
    {
        public ReservroomDbContext CreateDbContext(string[] args)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite("Data Source=reservroom.db").Options;
            return new ReservroomDbContext(options);
        }
    }
}
