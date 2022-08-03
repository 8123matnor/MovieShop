using System;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryContracts;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly MovieShopDbContext _movieShopDbContext;
        public PurchaseRepository(MovieShopDbContext dbContext)
        {
            _movieShopDbContext = dbContext;
        }

        public async Task<Purchase> AddPurchase(Purchase purchase)
        {
            _movieShopDbContext.Purchases.Add(purchase);
            await _movieShopDbContext.SaveChangesAsync();
            return purchase;
        }

        public async Task<List<Purchase>> GetAllById(int userId)
        {
            var purchases = await _movieShopDbContext.Purchases.Where(p => p.UserId == userId)
            .Include(p => p.Movie)
            .ToListAsync();
            return purchases;
        }

        public async Task<Purchase> GetByUserMovie(int userId, int movieId)
        {
            var purchase = await _movieShopDbContext.Purchases
            .Include(p => p.Movie)
            .FirstOrDefaultAsync(p => p.UserId == userId && p.MovieId == movieId);
            return purchase;
        }
    }
}

