using System;
using ApplicationCore.Entities;

namespace ApplicationCore.RepositoryContracts
{
    public interface IPurchaseRepository
    {
        Task<List<Purchase>> GetAllById(int userId);
        Task<Purchase> GetByUserMovie(int userId, int movieId);
        Task<Purchase> AddPurchase(Purchase purchase);
    }
}

