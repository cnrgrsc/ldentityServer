using Microsoft.EntityFrameworkCore;

namespace SampleIdentityServer.UI.Models
{
    public class MyDbContext:DbContext
    {
        public MyDbContext(DbContextOptions opts):base(opts)
        {

        }

        public DbSet<CustomUser> customUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CustomUser>().HasData(
                new CustomUser() 
                { 
                    Id = 1, 
                    Email = "cnrgrsc@gmail.com", 
                    Password = "password", 
                    City = "İstanbul", 
                    UserName = "cnrgrsc" 
                },
                new CustomUser()
                {
                    Id = 2,
                    Email = "ogzgrsc@gmail.com",
                    Password = "password",
                    City = "Erzurum",
                    UserName = "cnrgrsc"
                },
                new CustomUser()
                {
                    Id = 3,
                    Email = "hsngrsc@gmail.com",
                    Password = "password",
                    City = "İzmir",
                    UserName = "cnrgrsc"
                }
                );




            base.OnModelCreating(modelBuilder);
        }

    }
}
