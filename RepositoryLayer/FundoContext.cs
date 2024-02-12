using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entities;

namespace RepositoryLayer
{
    public class FundoContext : DbContext
    {
        public FundoContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Notes> Notes { get; set; }

        public DbSet<Image> Image { get; set; }

        public DbSet<Collaborator> Collaborators { get; set; }


    }
}
