using Moq;
using Xunit;
using System.Security.Claims;
using Safad.Interfaces;
using Safad.Models;
using Safad.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


public class AdministratorControllerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUserAdministrativeRepositor> _userAdministrativeRepositoryMock;
    private readonly Mock<IRoleRepository> _roleRepositoryMock;
    private readonly AdministratorController _controller;

    public AdministratorControllerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userAdministrativeRepositoryMock = new Mock<IUserAdministrativeRepositor>();
        _roleRepositoryMock = new Mock<IRoleRepository>();
        _controller = new AdministratorController(_userRepositoryMock.Object, 
            _userAdministrativeRepositoryMock.Object, _roleRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateUserAdministrative_Post_ValidModel_ReturnsRedirectToAction()
    {
        // Arrange
        var userAdministrative = new UserAdministrative
        {
            NameAdministrative = "Juan Pablo",
            DniAdministrative = "12345678",
            Cellphone = "987654321",
            Address = "123 Calle Medellin"
        };

        var userClaim = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "1")
        }));

        _controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = userClaim }
        };

        _userAdministrativeRepositoryMock.Setup(repo => repo.GetSequence(It.IsAny<UserAdministrative>()))
            .ReturnsAsync(new UserAdministrative { UserAdministrativeId = 1 });

        _userRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>()))
            .ReturnsAsync(new User { Registration = false });

        // Act
        var result = await _controller.CreateUserAdministrative(userAdministrative);

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("IndexAdministrative", redirectResult.ActionName);
        _userAdministrativeRepositoryMock.Verify(repo => repo.Add(It.IsAny<UserAdministrative>()), Times.Once);
        _userRepositoryMock.Verify(repo => repo.Update(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task ListUserAdministrative_UserExists_ReturnsViewWithModel()
    {
        // Arrange
        var userIdClaim = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "1")
        }));

        _controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = userIdClaim }
        };

        var userAdministrative = new UserAdministrative { UserAdministrativeId = 1, NameAdministrative = "Administrative Test" };

        _userAdministrativeRepositoryMock.Setup(repo => repo.GetUserByIdUserAsync(It.IsAny<int>()))
            .ReturnsAsync(userAdministrative);

        // Act
        var result = await _controller.ListUserAdministrative();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<UserAdministrative>(viewResult.ViewData.Model);
        Assert.Equal("Administrative Test", model.NameAdministrative);
    }

    [Fact]
    public async Task ListUserAdministrative_UserNotFound_ReturnsNotFound()
    {
        // Arrange
        var userIdClaim = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "1")
        }));

        _controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = userIdClaim }
        };

        _userAdministrativeRepositoryMock.Setup(repo => repo.GetUserByIdUserAsync(It.IsAny<int>()))
            .ReturnsAsync((UserAdministrative)null);

        // Act
        var result = await _controller.ListUserAdministrative();

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task EditUserAdministrative_ValidModel_ReturnsRedirectToAction()
    {
        // Arrange
        var userIdClaim = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "1")
        }));

        _controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = userIdClaim }
        };

        var userAdministrative = new UserAdministrative
        {
            UserAdministrativeId = 1,
            NameAdministrative = "Juan Pablo 1",
            DniAdministrative = "87654321"
        };

        _userAdministrativeRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>()))
            .ReturnsAsync(userAdministrative);

        _userRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>()))
            .ReturnsAsync(new User { UserId = 1 });

        // Act
        var result = await _controller.EditUserAdministrative(1, userAdministrative);

        // Assert
        var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("ListUserAdministrative", redirectResult.ActionName);
        _userAdministrativeRepositoryMock.Verify(repo => repo.Update(It.IsAny<UserAdministrative>()), Times.Once);
    }


}
