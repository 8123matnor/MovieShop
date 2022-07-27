using ApplicationCore.Entities;
using ApplicationCore.RepositoryContracts;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly MovieShopDbContext _movieShopDbContext;

    public MovieRepository(MovieShopDbContext dbContext)
    {
        _movieShopDbContext = dbContext;
    }

    public async Task<Movie> GetById(int id)
    {
        // select * from movie where id = 1 join genre, cast, moviegerne, moviecast
        var movieDetails = await _movieShopDbContext.Movies
            .Include(m => m.GenresOfMovie).ThenInclude(m => m.Genre)
            .Include(m => m.CastsOfMovie).ThenInclude(m => m.Cast)
            .Include(m => m.Trailers)
            .FirstOrDefaultAsync(m => m.Id == id);
        return movieDetails;
    }

    public async Task<List<Movie>> GetTop30HighestRevenueMovies()
    {
        // call the database with EF Core and get the data
        // use MovieShopDbContext and Movies DbSet
        // select top 30 * from Movies order by Revenue
        // corresponding LINQ Query

        var movies = await _movieShopDbContext.Movies.OrderByDescending(m => m.Revenue)
            .Select(m => new Movie { Id = m.Id, Title = m.Title, PosterUrl = m.PosterUrl})
            .Take(30).ToListAsync();
        return movies;
    }

    public Task<List<Movie>> GetTop30RatedMovies()
    {
        throw new NotImplementedException();
    }
}