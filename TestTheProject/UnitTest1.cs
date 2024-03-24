
using angular_crud.Data;
using angular_crud.Models.Mapping;
using anuglar_crud.Controllers;
using anuglar_crud.Models.Dto;
using anuglar_crud.Models.Domain;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace TestTheProject
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.True(1 == 1);
        }
         [Fact]
        public async Task RoleIntegrationTest()
        {
            // create Db contex

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();
            var optionsBuilder = new DbContextOptionsBuilder<AngularDBContext>();
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

            var context = new AngularDBContext(optionsBuilder.Options);
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile()); //your automapperprofile 
            });
            var mapper = mockMapper.CreateMapper();
            // delete all existing role
            //create new role controller


            var permissionController = new PermissionController(context);
            var permission = await permissionController.PostAsync(new Permissions { Name = "View_lead_Screen" });

            List<Guid> permissionList = new List<Guid>();
            permissionList.Add(permission);

            var roleController = new RolesController(context, mapper);
            //create new roles
            var customerRole = await roleController.PostAsync(new RoleDTO
            {
                RoleName = "Customer",
                PermissionList = permissionList
            });
            //check Role Customer
            Assert.Equal("Customer", customerRole.RoleName);
            if (customerRole.PermissionList != null)
            {
                Assert.NotEmpty(customerRole.PermissionList);
            }
            else
            {
                Assert.Fail("Permission isnt created");
            }
            //Role list check
            var result = (await roleController.GetAsync()).ToArray();
            Assert.Single(result);
            Assert.Equal("Customer", result[0].RoleName);

            //create new roles
            var vendorRole = await roleController.PostAsync(new RoleDTO
            {
                RoleName = "Vendor"
            });
            //check Role Vendor
            Assert.Equal("Vendor", vendorRole.RoleName);
            Assert.True(vendorRole.PermissionList == null || vendorRole.PermissionList!.Count() == 0);



            //check get all return added role or not 


        }


    }

}