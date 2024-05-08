using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace net_ef_videogame
{
    internal class VideoGame
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Overview { get; private set; }
        public DateTime ReleaseDate { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set;}
        public long SoftwareHouseID { get; private set; }

        public VideoGame (string name, string overview, DateTime releaseDate, DateTime createdAt, long softwareHouseID)
        {
            Name = name;
            Overview = overview;
            ReleaseDate = releaseDate;
            CreatedAt = createdAt;
            UpdatedAt = createdAt;
            SoftwareHouseID = softwareHouseID;
        }

        public VideoGame(long id, string name, string overview, DateTime releaseDate, DateTime createdAt, DateTime updatedAt, long softwareHouseID)
        {
            Id = id;
            Name = name;
            Overview = overview;
            ReleaseDate = releaseDate;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            SoftwareHouseID = softwareHouseID;
        }

        public override string ToString()
        {
            return $"Id: {Id}\nName: {Name}\nOverview: {Overview}\nRelease date: {ReleaseDate}\nCreated at: {CreatedAt}\nUpdated at: {UpdatedAt}\nSoftware house id: {SoftwareHouseID}";
        }
    }
}
