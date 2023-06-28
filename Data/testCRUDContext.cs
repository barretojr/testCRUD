using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using testCRUD.Models;

namespace testCRUD.Data
{
    public class testCRUDContext : DbContext
    {
        public testCRUDContext (DbContextOptions<testCRUDContext> options)
            : base(options)
        {
        }

        public DbSet<testCRUD.Models.Pessoa> Pessoa { get; set; } = default!;
    }
}
