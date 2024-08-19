using Microsoft.EntityFrameworkCore;
using MinimalAPI.Entities.Admin;

namespace MinimalAPI.Infrastructure.Db;

public class MyContext : DbContext
{
	private readonly IConfiguration _appSettingsConfiguration = default!;

	public MyContext(IConfiguration appSettingsConfiguration)
	{
		_appSettingsConfiguration = appSettingsConfiguration;
	}

	DbSet<Admin> Admin { get; set; } = default!;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Admin>().HasData(
			new Admin {
				Id = 1,
				Email = "admin@email.com",
				PWD = "1234",
				Profile = "adm"
			}
		);
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

			var connectionString = _appSettingsConfiguration.GetConnectionString("DefaultConnection")?.ToString();

			connectionString = connectionString?.Replace("{DB_PASSWORD}", dbPassword);

			if (!string.IsNullOrEmpty(connectionString))
			{
				optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
			}
		}
	}
}