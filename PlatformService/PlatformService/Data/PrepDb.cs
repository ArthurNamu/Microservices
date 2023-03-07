using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app, bool IsProd)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDBContext>()!, IsProd);
            }
        }

        private static void SeedData(AppDBContext context, bool isProd)
        {
            if (isProd)
            {
                Console.WriteLine("-- > Attempting to apply migrations.....");
                try
                {
                     context.Database.Migrate();

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Could not run migrations,{ex}");
                }
            }

            if (!context.Platforms.Any())
            {
                Console.WriteLine("seeding data...");

                context.Platforms.AddRange(
                    new Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "MySQL", Publisher = "Oracle", Cost = "Free" },
                    new Platform() { Name = "Kubernetes", Publisher = "Cloud Native Foundation", Cost = "Free" });

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("-- > We already have data");
            }
        }
    }
}
