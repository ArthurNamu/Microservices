using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDBContext>()!);
            }
        }

        private static void SeedData(AppDBContext context)
        {
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
