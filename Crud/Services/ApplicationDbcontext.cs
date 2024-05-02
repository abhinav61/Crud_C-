using Crud.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Crud.Services
{
    public class ApplicationDbcontext :DbContext
    {    
        //create constructor
        public ApplicationDbcontext(DbContextOptions options) : base(options) //passing to the base class

        {
        
        }
        public DbSet<Product> Products { get; set; }
    }
}
