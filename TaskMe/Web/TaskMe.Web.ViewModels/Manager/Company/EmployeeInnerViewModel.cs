﻿namespace TaskMe.Web.ViewModels.Manager.Company
{
    using System;

    using TaskMe.Data.Models;
    using TaskMe.Services.Mapping;

    public class EmployeeInnerViewModel : IMapFrom<ApplicationUser>
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PictureUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedOnShort { get => this.CreatedOn.ToShortDateString(); }
    }
}