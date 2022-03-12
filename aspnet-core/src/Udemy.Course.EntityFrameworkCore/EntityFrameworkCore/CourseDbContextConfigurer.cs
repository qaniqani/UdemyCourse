using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Udemy.Course.EntityFrameworkCore
{
    public static class CourseDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<CourseDbContext> builder, string connectionString)
        {
            // builder.UseSqlServer(connectionString);
            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public static void Configure(DbContextOptionsBuilder<CourseDbContext> builder, DbConnection connection)
        {
            // builder.UseSqlServer(connection);
            builder.UseMySql(connection, ServerVersion.AutoDetect(connection.ConnectionString));
        }
    }
}
