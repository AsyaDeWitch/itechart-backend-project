using Microsoft.EntityFrameworkCore;
using RIL.Models;
using System;

namespace DAL.Data
{
    public class TestDataProvider
    {
        private readonly ModelBuilder _builder;

        public TestDataProvider (ModelBuilder builder)
        {
            _builder = builder;
        }

        public void AddTestData()
        {
            _builder.Entity<Product>()
                .HasData(
                new { Id = 1, Name = "The Witcher 3: Wild Hunt", Platform = (int)Platform.Windows, DateCreated = new DateTime(2015, 5, 18), TotalRating = 9.3 },
                new { Id = 2, Name = "The Witcher 3: Wild Hunt", Platform = (int)Platform.Playstation_4, DateCreated = new DateTime(2015, 5, 18), TotalRating = 9.2 },
                new { Id = 3, Name = "The Witcher 3: Wild Hunt", Platform = (int)Platform.Xbox_One, DateCreated = new DateTime(2015, 5, 18), TotalRating = 9.1 },
                new { Id = 4, Name = "The Witcher 3: Wild Hunt", Platform = (int)Platform.Nintendo_Switch, DateCreated = new DateTime(2019, 10, 15), TotalRating = 8.5 },
                new { Id = 5, Name = "The Sims 3", Platform = (int)Platform.Windows, DateCreated = new DateTime(2009, 6, 2), TotalRating = 8.6 },
                new { Id = 6, Name = "The Sims 3", Platform = (int)Platform.MacOS, DateCreated = new DateTime(2009, 6, 2), TotalRating = 8.6 },
                new { Id = 7, Name = "Heroes of Might and Magic III", Platform = (int)Platform.Windows, DateCreated = new DateTime(1999, 2, 28), TotalRating = 9.2 },
                new { Id = 8, Name = "Heroes of Might and Magic III", Platform = (int)Platform.MacOS, DateCreated = new DateTime(1999, 12, 20), TotalRating = 9.2 },
                new { Id = 9, Name = "Heroes of Might and Magic III", Platform = (int)Platform.Linux, DateCreated = new DateTime(1999, 12, 21), TotalRating = 9.2 },
                new { Id = 10, Name = "Grand Theft Auto V", Platform = (int)Platform.PlayStation_3, DateCreated = new DateTime(2013, 9, 17), TotalRating = 9.7 },
                new { Id = 11, Name = "Grand Theft Auto V", Platform = (int)Platform.Xbox_360, DateCreated = new DateTime(2013, 9, 17), TotalRating = 9.7 },
                new { Id = 12, Name = "Grand Theft Auto V", Platform = (int)Platform.Playstation_4, DateCreated = new DateTime(2014, 11, 18), TotalRating = 9.7 },
                new { Id = 13, Name = "Grand Theft Auto V", Platform = (int)Platform.Xbox_One, DateCreated = new DateTime(2014, 11, 18), TotalRating = 9.7 },
                new { Id = 14, Name = "Grand Theft Auto V", Platform = (int)Platform.Windows, DateCreated = new DateTime(2015, 4, 14), TotalRating = 9.6 },
                new { Id = 15, Name = "World of Warcraft: Wrath of the Lich King", Platform = (int)Platform.Windows , DateCreated = new DateTime(2008, 11, 18), TotalRating = 9.1 },
                new { Id = 16, Name = "World of Warcraft: Wrath of the Lich King", Platform = (int)Platform.MacOS, DateCreated = new DateTime(2008, 11, 18), TotalRating = 9.1 },
                new { Id = 17, Name = "Red Dead Redemption 2", Platform = (int)Platform.Playstation_4, DateCreated = new DateTime(2018, 10, 26), TotalRating = 9.7 },
                new { Id = 18, Name = "Red Dead Redemption 2", Platform = (int)Platform.Xbox_One, DateCreated = new DateTime(2018, 10, 26), TotalRating = 9.7 },
                new { Id = 19, Name = "Red Dead Redemption 2", Platform = (int)Platform.Windows, DateCreated = new DateTime(2018, 11, 5), TotalRating = 9.3 },
                new { Id = 20, Name = "The Elder Scrolls V: Skyrim", Platform = (int)Platform.Windows, DateCreated = new DateTime(2011, 11, 11), TotalRating = 9.4 },
                new { Id = 21, Name = "The Elder Scrolls V: Skyrim", Platform = (int)Platform.PlayStation_3, DateCreated = new DateTime(2011, 11, 11), TotalRating = 9.2 },
                new { Id = 22, Name = "The Elder Scrolls V: Skyrim", Platform = (int)Platform.Xbox_360, DateCreated = new DateTime(2011, 11, 11), TotalRating = 9.6 },
                new { Id = 23, Name = "The Elder Scrolls V: Skyrim", Platform = (int)Platform.Playstation_4, DateCreated = new DateTime(2016, 10, 28), TotalRating = 7.7 },
                new { Id = 24, Name = "The Elder Scrolls V: Skyrim", Platform = (int)Platform.Xbox_One, DateCreated = new DateTime(2016, 10, 28), TotalRating = 8.2 },
                new { Id = 25, Name = "The Elder Scrolls V: Skyrim", Platform = (int)Platform.Nintendo_Switch, DateCreated = new DateTime(2017, 11, 17), TotalRating = 8.4 }
                );
        }
    }
}
