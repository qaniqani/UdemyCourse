using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Udemy.Course.Authorization.Roles;
using Udemy.Course.Authorization.Users;
using Udemy.Course.Customers;
using Udemy.Course.MultiTenancy;
using Udemy.Course.Products;

namespace Udemy.Course.EntityFrameworkCore
{
    public class CourseDbContext : AbpZeroDbContext<Tenant, Role, User, CourseDbContext>
    {
        /* Define a DbSet for each entity of the application */

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductMapping> ProductMappings { get; set; }

        public CourseDbContext(DbContextOptions<CourseDbContext> options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
    
            modelBuilder.ChangeAbpTablePrefix<Tenant, Role, User>(""); //Removes table prefixes. You can specify another prefix.
        }
    }
}
