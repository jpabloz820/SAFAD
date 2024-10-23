﻿Imports Moq
Imports Xunit
Imports Safad.Controllers
Imports Safad.Interfaces
Imports Safad.Models
Imports Microsoft.AspNetCore.Mvc
Imports System.Security.Claims
Imports Microsoft.AspNetCore.Authentication
Imports Microsoft.AspNetCore.Http
Imports System.Threading.Tasks
Imports Microsoft.AspNetCore.Mvc.Routing

Public Class AccountControllerTests

    <Fact>
    Public Async Function Login_WithValidCredentials_ReturnsRedirectToAction() As Task
        ' Arrange
        Dim mockUserRepo As New Mock(Of IUserRepository)()
        Dim mockRoleRepo As New Mock(Of IRoleRepository)()
        Dim mockUserCoachRepo As New Mock(Of IUserCoachRepository)()
        Dim mockUserAthleteRepo As New Mock(Of IUserAthleteRepository)()

        Dim validUser As New User() With {
            .UserId = 2,
            .UserEmail = "juanes2113@gmail.com",
            .Password = "123456789",
            .RoleId = 2,
            .Registration = True
        }

        Dim validRole As New Role() With {
            .RoleId = 2,
            .RoleName = "Entrenador"
        }

        mockUserRepo.Setup(Function(repo) repo.GetUserByEmailAsync("juanes2113@gmail.com")).ReturnsAsync(validUser)
        mockRoleRepo.Setup(Function(repo) repo.GetById(2)).ReturnsAsync(validRole)

        ' Simular el HttpContext con Claims y Authentication
        Dim httpContextMock As New Mock(Of HttpContext)()
        Dim authenticationServiceMock As New Mock(Of IAuthenticationService)()

        ' Configurar la respuesta del SignInAsync
        authenticationServiceMock.Setup(Function(a) a.SignInAsync(It.IsAny(Of HttpContext), It.IsAny(Of String), It.IsAny(Of ClaimsPrincipal), Nothing)).Returns(Task.CompletedTask)

        ' Asignar el servicio de autenticación al HttpContext
        httpContextMock.Setup(Function(c) c.RequestServices.GetService(GetType(IAuthenticationService))).Returns(authenticationServiceMock.Object)

        ' Simular IUrlHelper para manejar la redirección
        Dim urlHelperMock As New Mock(Of IUrlHelper)()
        urlHelperMock.Setup(Function(x) x.Action(It.IsAny(Of UrlActionContext)())).Returns("fake-url")

        ' Crear el controller y asignarle el HttpContext simulado
        Dim controller As New AccountController(mockUserRepo.Object, mockRoleRepo.Object, mockUserCoachRepo.Object, mockUserAthleteRepo.Object) With {
            .ControllerContext = New ControllerContext() With {
                .HttpContext = httpContextMock.Object
            },
            .Url = urlHelperMock.Object ' Asignar el IUrlHelper simulado
        }

        ' Act
        Dim result As IActionResult = Await controller.Login("juanes2113@gmail.com", "123456789")

        ' Assert
        Dim redirectResult = Assert.IsType(Of RedirectToActionResult)(result)
        Assert.Equal("IndexCoach", redirectResult.ActionName)
        Assert.Equal("Coach", redirectResult.ControllerName)
    End Function

End Class


