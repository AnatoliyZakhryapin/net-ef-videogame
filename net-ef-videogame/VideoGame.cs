using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace net_ef_videogame
{

    [Table("videogames")]
    public class VideoGame
    {
        [Key] public long Id { get; private set; }
        public string Name { get; private set; }
        public string Overview { get; private set; }
        public DateTime ReleaseDate { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set;}

        //[ForeignKey("SoftwareHouse")]
        public long SoftwareHouseId { get; set; }
        public SoftwareHouse SoftwareHouse { get; set; }

        public VideoGame() { }

        public VideoGame(string name, string overview, DateTime releaseDate, DateTime? createdAt, DateTime? updatedAt, long softwareHouseID)
        {
            //Id = id;
            Name = name;
            Overview = overview;
            ReleaseDate = releaseDate;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            SoftwareHouseId = softwareHouseID;
        }

        public override string ToString()
        {
            return $"Id: {Id}\nName: {Name}\nOverview: {Overview}\nRelease date: {ReleaseDate}\nCreated at: {CreatedAt}\nUpdated at: {UpdatedAt}\nSoftware house id: {SoftwareHouseId}";
        }
    }
}
