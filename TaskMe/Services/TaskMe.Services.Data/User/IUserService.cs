﻿namespace TaskMe.Services.Data.User
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TaskMe.Web.InputModels.Common.User;

    public interface IUserService
    {
        Task RegisterUserForCompanyAsync(RegisterUserInputModel inputModel, string roleName = null);

        Task<IEnumerable<T>> GetUsersInCompanyInViewModelAsync<T>(string companyId);

        Task<IEnumerable<T>> GetSupervisorsInCompanyInViewModelAsync<T>(string companyId);

        Task<bool> DeleteUserAsync(string userId);

        T GetUserInViewModel<T>(string name);
    }
}
