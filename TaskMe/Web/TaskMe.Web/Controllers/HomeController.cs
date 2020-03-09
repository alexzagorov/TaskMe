﻿namespace TaskMe.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using TaskMe.Services.Data.User;
    using TaskMe.Web.ViewModels;

    public class HomeController : BaseController
    {
        private readonly IUserService userService;

        public HomeController(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = await this.userService.GetHomePageInfoAsync(this.User.Identity.Name);
            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
