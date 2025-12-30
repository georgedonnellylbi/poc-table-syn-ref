using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace POCTest
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var footageOptions = new DbContextOptionsBuilder<Data.Footage.FootageDbContext>()
				.UseSqlServer("Server=localhost,14332;Database=footage;User Id=sa;Password=YourStrongPassw0rd;TrustServerCertificate=True;")
				.Options;
			var commonOptions = new DbContextOptionsBuilder<Data.Common.CommonDbContext>()
				.UseSqlServer("Server=localhost,14331;Database=common;User Id=sa;Password=YourStrongPassw0rd;TrustServerCertificate=True;")
				.Options;

			using var footageContext = new Data.Footage.FootageDbContext(footageOptions);
			using var commonContext = new Data.Common.CommonDbContext(commonOptions);

			var filmsWithClubs = await footageContext
				.Films
				.Include(c => c.Club)
				.ToListAsync();

			Console.WriteLine("Films with Clubs:");

			foreach (var item in filmsWithClubs)
			{
				Console.WriteLine($"Film: {item.Title}, Club: {item.Club.CanonicalName}");
			}

			// Insert a new Film record
			var newFilm = new Models.Footage.Film
			{
				Title = "Test Film" + DateTime.Now.Ticks,
				ClubId = 1 // Assumes ClubId 1 exists
			};
			footageContext.Films.Add(newFilm);
			await footageContext.SaveChangesAsync();

			Console.WriteLine($"Inserted Film: {newFilm.Title}, ClubId: {newFilm.ClubId}");
		}
	}
}
