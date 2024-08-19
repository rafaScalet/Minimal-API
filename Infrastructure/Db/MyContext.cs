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

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			var connectionString = _appSettingsConfiguration.GetConnectionString("DefaultConnection")?.ToString();

			if (!string.IsNullOrEmpty(connectionString))
			{
				optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
			}
		}
	}
}