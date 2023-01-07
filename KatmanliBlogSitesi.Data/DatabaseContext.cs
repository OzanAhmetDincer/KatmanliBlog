using KatmanliBlogSitesi.Entites;
using Microsoft.EntityFrameworkCore;

namespace KatmanliBlogSitesi.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }//Veritabanımızdaki tabloyu simgeler
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // OnConfiguring metodu entity framework core içerisinden gelir ve veritabanı ayarlarını yapabilmemizi sağlar.
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; database=KatmanliProjeOdev; integrated security=true"); // Burada uygulamamızda sql server kullanacağımızı entity framework core a belirttik. UseSqlServer metoduna () içerisine connection string ile veritabanı bilgilerimizi parametre olarak gönderebiliyoruz.
            // "integrated security=true" şifre sormaması içim
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // aşağıdaki metot uygulama ilk defa çalıştığında veritabanı oluştuktan sonra admin paneline giriş yapabilmek için veritabanındaki users tablosuna 1 tane kayıt ekler.
            // Bu kayıtı eklemezsek veritabanına kendimiz eklememiz lazım
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    CreateDate = DateTime.Now,
                    Email = "admin@blogsitesi.com",
                    IsActive = true,
                    IsAdmin = true,
                    Name = "admin",
                    SurName = "admin",
                    Password = "123"
                }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}