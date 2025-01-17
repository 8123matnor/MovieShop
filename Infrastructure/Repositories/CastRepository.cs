﻿using ApplicationCore.Entities;
using ApplicationCore.RepositoryContracts;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CastRepository : ICastRepository
{
    private readonly MovieShopDbContext _movieShopDbContext;

    public CastRepository(MovieShopDbContext dbContext)
    {
        _movieShopDbContext = dbContext;
    }

    public async Task<Cast> GetById(int id)
    {
        // select * from movie where id = 1 join genre, cast, moviegerne, moviecast
        var castDetails = await _movieShopDbContext.Casts
            .Include(m => m.MoviesOfCast).ThenInclude(m => m.Movie)
            .FirstOrDefaultAsync(m => m.Id == id);
#pragma warning disable CS8603 // Possible null reference return.
        return castDetails;
#pragma warning restore CS8603 // Possible null reference return.
    }
}