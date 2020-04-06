﻿namespace TaskMe.Web.ViewModels.Manager.Task
{
    using TaskMe.Data.Models;
    using TaskMe.Services.Mapping;

    public class UsersForDetailsInnerViewModel : IMapFrom<UserTask>
    {
        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public string UserPictureUrl { get; set; }
    }
}