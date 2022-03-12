using System.Threading.Tasks;
using Udemy.Course.Models.TokenAuth;
using Udemy.Course.Web.Controllers;
using Shouldly;
using Xunit;

namespace Udemy.Course.Web.Tests.Controllers
{
    public class HomeController_Tests: CourseWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}