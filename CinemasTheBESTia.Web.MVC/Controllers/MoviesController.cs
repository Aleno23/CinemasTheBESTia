using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemasTheBESTia.Entities.Movies;
using CinemasTheBESTia.Utilities.Abstractions.Interfaces;
using CinemasTheBESTia.Web.MVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CinemasTheBESTia.Web.MVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IAPIClient _apiClient;
        private readonly IConfiguration _configuration;

        public MoviesController(IAPIClient apiClient, IConfiguration configuration)
        {
            _apiClient = apiClient;
            _configuration = configuration;
        }

        // GET: Movies
        public async Task<ActionResult> Index()
        {
            var viewModel = new MoviesViewModel();
            try
            {
                var movies = await _apiClient.GetAsync<IEnumerable<Movie>>(new Uri(_configuration["Movies:BaseUrl"]));
                viewModel.Movies = movies;
            }
            catch (Exception ex)
            {

            }
            return View(viewModel);
        }


        // GET: Movies/Details        

        public ActionResult Details(Movie movie)
        {
            return View(new MovieDetailViewModel() { Movie = movie });
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Movies/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}