Imports Moq
Imports Xunit
Imports Microsoft.AspNetCore.Mvc
Imports Safad.Controllers
Imports Safad.Interfaces
Imports Safad.Models
Imports System.Security.Claims
Imports System.IO
Imports Microsoft.AspNetCore.Http

Public Class CoachControllerTests
    <Fact>
    Public Async Function CreateUserCoach_ValidModel_ReturnsRedirectToAction() As Task
        ' Arrange
        Dim mockUserRepo = New Mock(Of IUserRepository)()
        Dim mockUserCoachRepo = New Mock(Of IUserCoachRepository)()

        ' Datos de prueba
        Dim validUserCoach As New UserCoach With {
            .NameCoach = "juan estiven eusse ramirez",
            .DniCoach = "12345667777",
            .Cellphone = "3016196996",
            .Address = "CARRERA 49B 94 37",
            .UserCoachId = 1,
            .UserId = 2
        }

        ' Configurar los repositorios simulados
        mockUserCoachRepo.Setup(Function(repo) repo.GetSequence(It.IsAny(Of UserCoach))).ReturnsAsync(validUserCoach)
        mockUserCoachRepo.Setup(Function(repo) repo.Add(It.IsAny(Of UserCoach))).Returns(Task.CompletedTask)
        mockUserRepo.Setup(Function(repo) repo.GetById(It.IsAny(Of Integer))).ReturnsAsync(New User With {
            .UserId = 2,
            .Registration = False
        })
        mockUserRepo.Setup(Function(repo) repo.Update(It.IsAny(Of User))).Returns(Task.CompletedTask)

        ' Simular el HttpContext con el Claim del userId
        Dim userIdClaim = New Claim(ClaimTypes.NameIdentifier, "2")
        Dim identity = New ClaimsIdentity(New Claim() {userIdClaim}, "mock")
        Dim claimsPrincipal = New ClaimsPrincipal(identity)

        Dim mockHttpContext = New Mock(Of HttpContext)()
        mockHttpContext.Setup(Function(c) c.User).Returns(claimsPrincipal)

        ' Crear una simulación de IFormFile (la foto)
        Dim mockPhoto = New Mock(Of IFormFile)()
        Dim ms = New MemoryStream()
        Dim writer = New StreamWriter(ms)
        writer.Write("dummy image content")
        writer.Flush()
        ms.Position = 0
        mockPhoto.Setup(Function(f) f.OpenReadStream()).Returns(ms)
        mockPhoto.Setup(Function(f) f.FileName).Returns("Carlitos.jpg")
        mockPhoto.Setup(Function(f) f.Length).Returns(ms.Length)

        ' Configurar el controlador
        Dim controller = New CoachController(mockUserRepo.Object, mockUserCoachRepo.Object) With {
            .ControllerContext = New ControllerContext With {
                .HttpContext = mockHttpContext.Object
            }
        }

        ' Act
        Dim result = Await controller.CreateUserCoach(validUserCoach, mockPhoto.Object)

        ' Assert
        Dim redirectResult = Assert.IsType(Of RedirectToActionResult)(result)
        Assert.Equal("Login", redirectResult.ActionName)
        Assert.Equal("Account", redirectResult.ControllerName)

        ' Verificar que se llamó a los métodos esperados
        mockUserCoachRepo.Verify(Function(repo) repo.Add(It.IsAny(Of UserCoach)), Times.Once)
        mockUserRepo.Verify(Function(repo) repo.Update(It.IsAny(Of User)), Times.Once)
    End Function
End Class

