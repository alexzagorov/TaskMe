﻿namespace TaskMe.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TaskMe.Web.ViewModels.Administration.Dashboard;

    public class CompanyController : AdministrationController
    {
        public CompanyController()
        {

        }

        public IActionResult Create()
        {
            return this.View();
        }
    }
}