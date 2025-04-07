using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class WebApplication1Context : DbContext
    {
        public WebApplication1Context (DbContextOptions<WebApplication1Context> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customer { get; set; } = default!;
    }
}
