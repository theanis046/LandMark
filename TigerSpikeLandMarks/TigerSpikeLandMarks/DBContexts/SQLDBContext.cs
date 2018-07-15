using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TigerSpikeLandMarks.Entities;

namespace TigerSpikeLandMarks.DBContexts
{
    public class SQLDBContext : DbContext
    {
        public SQLDBContext(DbContextOptions<SQLDBContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<LandMarkNote> LandMarks { get; set; }
    }
}
