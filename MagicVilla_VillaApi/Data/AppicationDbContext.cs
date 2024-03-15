using MagicVilla_VillaApi.Model;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaApi.Data
{
    public class AppicationDbContext : DbContext
    {
        public AppicationDbContext(DbContextOptions<AppicationDbContext> options) : base(options)
        {

        }
        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Name = "Kanwal villa",
                    Details = "I love kawal",
                    ImageUrl = "",
                    Occupancy = 5,
                    Rate = 700,
                    Sqft = 550,
                    Amenity = "",
                    CreatedDate = DateTime.Now
                },
                new Villa()
                {
                    Id = 2,
                    Name = "Aisha villa",
                    Details = "I love Aisha",
                    ImageUrl = "",
                    Occupancy = 7,
                    Rate = 900,
                    Sqft = 950,
                    Amenity = "",
                    CreatedDate = DateTime.Now
                },
                new Villa()
                {
                    Id = 3,
                    Name = "myra villa",
                    Details = "I love myra",
                    ImageUrl = "",
                    Occupancy = 9,
                    Rate = 800,
                    Sqft = 590,
                    Amenity = "",
                    CreatedDate = DateTime.Now
                },
                new Villa()
                {
                    Id = 4,
                    Name = "areej villa",
                    Details = "I love areej",
                    ImageUrl = "",
                    Occupancy = 5,
                    Rate = 900,
                    Sqft = 250,
                    Amenity = "",
                    CreatedDate = DateTime.Now
                },
                new Villa()
                {
                    Id = 5,
                    Name = "maryam villa",
                    Details = "I love maryam",
                    ImageUrl = "",
                    Occupancy = 4,
                    Rate = 760,
                    Sqft = 850,
                    Amenity = "",
                    CreatedDate = DateTime.Now
                });
            
        }
    }
}
