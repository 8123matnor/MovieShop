using System;
namespace ApplicationCore.Models
{
    public class CastDetailsModel
    {
        public CastDetailsModel()
        {
            Movies = new List<MovieModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string TmdbUrl { get; set; }
        public string ProfilePath { get; set; }

        public List<MovieModel> Movies { get; set; }
    }
}

