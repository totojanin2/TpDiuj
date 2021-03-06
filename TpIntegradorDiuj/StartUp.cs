﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using TpIntegradorDiuj.Models;
using Owin;
using System.Security.Claims;
using Hangfire;
using TpIntegradorDiuj.Services;
using System.Collections.Generic;
using Hangfire.Dashboard;
using System.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage.Blob;

[assembly: OwinStartupAttribute(typeof(TpIntegradorDiuj.Startup))]
namespace TpIntegradorDiuj
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection");
            app.UseHangfireDashboard("/hg", new DashboardOptions
            {
                Authorization = Enumerable.Empty<IDashboardAuthorizationFilter>()
            });
            app.UseHangfireDashboard();
            app.UseHangfireServer();
            //createRolesandUsers();

            RecurringJob.AddOrUpdate(()=>BalancesService.BatchArchivosBalances(),Cron.Daily);

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting("diujstorage_AzureStorageConnectionString"));

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("contenedor");

            container.CreateIfNotExists();
        }


        // In this method we will create default User roles and Admin user for login 
        private void createRolesandUsers()
        {
            TpIntegradorDbContext context = new TpIntegradorDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User  
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool 
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                 

                var user = new ApplicationUser();
                user.UserName = "shanu";
                user.Email = "syedshanumcain@gmail.com";

                string userPWD = "A@Z200711";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin 
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            }

            // creating Creating Manager role  
            if (!roleManager.RoleExists("Manager"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Manager";
                roleManager.Create(role);

            }

            // creating Creating Employee role  
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);

            }
        }
    }
}

