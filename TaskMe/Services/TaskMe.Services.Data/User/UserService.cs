﻿namespace TaskMe.Services.Data.User
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using TaskMe.Common;
    using TaskMe.Data.Common.Repositories;
    using TaskMe.Data.Models;
    using TaskMe.Services.Data.Picture;
    using TaskMe.Services.Mapping;
    using TaskMe.Web.InputModels.Common.User;

    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IPictureService pictureService;
        private readonly IDeletableEntityRepository<ApplicationUser> users;
        private readonly IDeletableEntityRepository<Company> companies;

        public UserService(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            ICloudinaryService cloudinaryService,
            IPictureService pictureService,
            IDeletableEntityRepository<ApplicationUser> users,
            IDeletableEntityRepository<Company> companies)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.cloudinaryService = cloudinaryService;
            this.pictureService = pictureService;
            this.users = users;
            this.companies = companies;
        }

        public async Task RegisterUserForCompanyAsync(RegisterUserInputModel inputModel, string roleName = null)
        {
            string profilePictureId = string.Empty;

            if (inputModel.Picture == null)
            {
                string pictureUrl = "https://i2.wp.com/molddrsusa.com/wp-content/uploads/2015/11/profile-empty.png.250x250_q85_crop.jpg?ssl=1";
                profilePictureId = await this.pictureService.AddPictureAsync(pictureUrl);
            }
            else
            {
                string cloudinaryPicName = $"{inputModel.FirstName.ToLower()}_{inputModel.LastName.ToLower()}";
                string folderName = "profile_pictures";

                var url = await this.cloudinaryService.UploadPhotoAsync(inputModel.Picture, cloudinaryPicName, folderName);
                profilePictureId = await this.pictureService.AddPictureAsync(url);
            }

            var user = new ApplicationUser
            {
                UserName = inputModel.Email,
                Email = inputModel.Email,
                FirstName = inputModel.FirstName,
                LastName = inputModel.LastName,
                PhoneNumber = inputModel.PhoneNumber,
                CompanyId = inputModel.CompanyId,
                PictureId = profilePictureId,
            };

            var result = await this.userManager.CreateAsync(user, inputModel.Password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Problem occured while creating user in UserSevice");
            }
            else if (roleName != null)
            {
                await this.userManager.AddToRoleAsync(user, roleName);
            }
        }

        public async Task<IEnumerable<T>> GetUsersInCompanyInViewModelAsync<T>(string companyId)
        {
            var managerRoleId = await this.roleManager.FindByNameAsync(GlobalConstants.ManagerRoleName);
            return this.users.All().Where(x => x.CompanyId == companyId && !x.Roles.Any(ur => ur.RoleId == managerRoleId.Id))
                .To<T>()
                .ToList();
        }

        public async Task<IEnumerable<T>> GetSupervisorsInCompanyInViewModelAsync<T>(string companyId)
        {
            var supervisorRole = await this.roleManager.FindByNameAsync(GlobalConstants.SupervisorRoleName);
            var companySupervisors = this.users.All().Where(x => x.CompanyId == companyId && x.Roles.Any(ur => ur.RoleId == supervisorRole.Id))
                .To<T>()
                .ToList();

            return companySupervisors;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = this.users.All().FirstOrDefault(x => x.Id == userId);
            this.users.Delete(user);
            int result = await this.users.SaveChangesAsync();
            if (result < 0)
            {
                return false;
            }

            return true;
        }

        public T GetUserInViewModel<T>(string name)
        {
            var currentUser = this.users.All().Where(x => x.UserName == name)
               .To<T>()
               .FirstOrDefault();

            return currentUser;
        }
    }
}
