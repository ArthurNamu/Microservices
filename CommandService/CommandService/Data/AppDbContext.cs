using CommandService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandService.Data;

public class AppDbContext:DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
	{}

	public DbSet<Platform> Platforms { get; set; }
	public DbSet<Command> Command { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder
			.Entity<Platform>()
			.HasMany(prop => prop.Commands)
			.WithOne(prop => prop.Platform!)
			.HasForeignKey(prop => prop.PlatformId);

		modelBuilder
			.Entity<Command>()
			.HasOne(prop => prop.Platform)
			.WithMany(prop => prop.Commands)
			.HasForeignKey(prop => prop.PlatformId);
	}
}
