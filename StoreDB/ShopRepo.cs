using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;

namespace StoreDB
{
    /// <summary>
    /// Repository for accessing shop data
    /// </summary>
    public class ShopRepo : IShopRepo
    {
        const string testData = @"StoreDB\TestData\auto.JSON";
        private List<Location> locations;

        /// <summary>
        /// Returns a list of all locations available
        /// </summary>
        /// <returns></returns>
        public async Task<List<Location>> GetLocationsFromFile()
        {
            //get Location objects from JSON
            List<Location> locations;
            using (FileStream fs = File.OpenRead(testData))
            {
                locations = await JsonSerializer.DeserializeAsync<List<Location>>(fs);
            }

            //convert List<Location> to List<ILocation>
            List<Location> result = new List<Location>(); 
            foreach (var loc in locations)
            {
                result.Add(loc);
            }

            return result;
        }

        public List<Location> GetLocations()
        {
            CreateLocations();
            List<Location> result = new List<Location>(); 
            foreach (var loc in locations)
            {
                result.Add(loc);
            }
            return result;
        }

        public void CreateLocations()
        {
            //dummy data
            locations = new List<Location>();
            locations.Add(new Location("Washington, DC", new Address("123 Main St", "Washington", 12345, "DC", "USA")));
            locations.Add(new Location("Sterling, VA", new Address("123 Main St", "Sterling", 12345, "VA", "USA")));
            locations.Add(new Location("Frederick, MD", new Address("123 Main St", "Frederick", 12345, "MD", "USA")));            
        }

        public async void WriteLocationsToFile()
        {
            CreateLocations();
            using(FileStream fs = File.Create(@"StoreDB\TestData\auto.JSON"))
            {
                await JsonSerializer.SerializeAsync(fs, locations);
            }
        }
    }
}
