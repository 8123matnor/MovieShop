using ApplicationCore.Models;
using ApplicationCore.RepositoryContracts;
using ApplicationCore.ServiceContracts;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CastService : ICastService
    {
        private readonly ICastRepository _castRepository;
        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }

        public async Task<CastDetailsModel> GetCastDetails(int castId)
        {
            var castDetails = await _castRepository.GetById(castId);

            var castDetailsModel = new CastDetailsModel
            {
                Id = castDetails.Id,
                Name = castDetails.Name,
                Gender = castDetails.Gender,
                TmdbUrl = castDetails.TmdbUrl,
                ProfilePath = castDetails.ProfilePath
    };

            foreach (var cast in castDetails.MoviesOfCast)
            {
                castDetailsModel.Movies.Add(new MovieModel { Id = cast.CastId, Title = cast.Movie.Title });
            }


            return castDetailsModel;
        }

       
    }
}
