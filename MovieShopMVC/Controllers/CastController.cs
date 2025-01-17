﻿using ApplicationCore.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class CastController : Controller
    {
        private readonly ICastService _castService;

        public CastController(ICastService castService)
        {
            _castService = castService;
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            // T1
            // go to movie service -> movie repository and get movie details from Movies Table

            // We are making an I/O bound operation //
            // database call, File call, stream, network

            // CPU bound operations
            // calculating a loan processing interat rate,
            // Image processing
            // pi number digits
            // 10 ms, 100 ms, 2 sec 10 sec
            // T1 just waiting for I/O bound operation to finish
            // Queued up and will not threads, 503 - Thread starvation, server scalibilty
            // async await  2012 C# 5
            var castDetails = await _castService.GetCastDetails(id);
            return View(castDetails);
        }
    }
}
