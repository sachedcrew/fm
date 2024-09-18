Imports NUnit.Framework
Imports Moq
Imports Microsoft.AspNetCore.Mvc
Imports FleetManagmentApp.Controllers
Imports FleetManagmentApp.Models
Imports Microsoft.AspNetCore.Identity
Imports System.Threading.Tasks
Imports Microsoft.AspNetCore.Http

<TestFixture>
Public Class AuthControllerTests
    Private _controller As AuthController
    Private _userManagerMock As Mock(Of UserManager(Of ApplicationUser))
    Private _signInManagerMock As Mock(Of SignInManager(Of ApplicationUser))
    Private _contextAccessorMock As Mock(Of IHttpContextAccessor)
    Private _userPrincipalFactoryMock As Mock(Of IUserClaimsPrincipalFactory(Of ApplicationUser))

    <SetUp>
    Public Sub Setup()
        ' Mock HttpContextAccessor and other dependencies
        _contextAccessorMock = New Mock(Of IHttpContextAccessor)()
        _userPrincipalFactoryMock = New Mock(Of IUserClaimsPrincipalFactory(Of ApplicationUser))()

        _userManagerMock = New Mock(Of UserManager(Of ApplicationUser))(
            New Mock(Of IUserStore(Of ApplicationUser))().Object,
            Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        _signInManagerMock = New Mock(Of SignInManager(Of ApplicationUser))(
            _userManagerMock.Object,
            _contextAccessorMock.Object,
            _userPrincipalFactoryMock.Object,
            Nothing, Nothing, Nothing, Nothing)

        _controller = New AuthController(_userManagerMock.Object, _signInManagerMock.Object)
    End Sub

    <Test>
    Public Async Function Register_ValidModel_ReturnsOk() As Task
        ' Arrange
        Dim model As New RegisterModel() With {
            .Email = "test@example.com",
            .Password = "Password123"
        }
        _userManagerMock.Setup(Function(x) x.FindByEmailAsync(It.IsAny(Of String)())).ReturnsAsync(CType(Nothing, ApplicationUser))
        _userManagerMock.Setup(Function(x) x.CreateAsync(It.IsAny(Of ApplicationUser), It.IsAny(Of String)())).ReturnsAsync(IdentityResult.Success)

        ' Act
        Dim result As IActionResult = Await _controller.Register(model)

        ' Assert
        Assert.IsInstanceOf(Of OkObjectResult)(result)
        Dim okResult As OkObjectResult = CType(result, OkObjectResult)
        Assert.AreEqual(200, okResult.StatusCode)
    End Function

    <Test>
    Public Async Function Register_InvalidEmail_ReturnsBadRequest() As Task
        ' Arrange
        Dim model As New RegisterModel() With {
            .Email = "invalid-email",
            .Password = "Password123"
        }

        ' Act
        Dim result As IActionResult = Await _controller.Register(model)

        ' Assert
        Assert.IsInstanceOf(Of BadRequestObjectResult)(result)
        Dim badRequestResult As BadRequestObjectResult = CType(result, BadRequestObjectResult)
        Assert.AreEqual(400, badRequestResult.StatusCode)
    End Function

    <Test>
    Public Async Function Login_ValidCredentials_ReturnsOk() As Task
        ' Arrange
        Dim model As New LoginModel() With {
            .Email = "test@example.com",
            .Password = "Password123"
        }
        _userManagerMock.Setup(Function(x) x.FindByEmailAsync(It.IsAny(Of String)())).ReturnsAsync(New ApplicationUser())
        _signInManagerMock.Setup(Function(x) x.PasswordSignInAsync(It.IsAny(Of String), It.IsAny(Of String), It.IsAny(Of Boolean), It.IsAny(Of Boolean))).ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success)

        ' Act
        Dim result As IActionResult = Await _controller.Login(model)

        ' Assert
        Assert.IsInstanceOf(Of OkObjectResult)(result)
        Dim okResult As OkObjectResult = CType(result, OkObjectResult)
        Assert.AreEqual(200, okResult.StatusCode)
    End Function

    <Test>
    Public Async Function Login_InvalidCredentials_ReturnsUnauthorized() As Task
        ' Arrange
        Dim model As New LoginModel() With {
            .Email = "test@example.com",
            .Password = "WrongPassword"
        }
        _userManagerMock.Setup(Function(x) x.FindByEmailAsync(It.IsAny(Of String)())).ReturnsAsync(New ApplicationUser())
        _signInManagerMock.Setup(Function(x) x.PasswordSignInAsync(It.IsAny(Of String), It.IsAny(Of String), It.IsAny(Of Boolean), It.IsAny(Of Boolean))).ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed)

        ' Act
        Dim result As IActionResult = Await _controller.Login(model)

        ' Assert
        Assert.IsInstanceOf(Of UnauthorizedObjectResult)(result)
        Dim unauthorizedResult As UnauthorizedObjectResult = CType(result, UnauthorizedObjectResult)
        Assert.AreEqual(401, unauthorizedResult.StatusCode)
    End Function

    <Test>
    Public Async Function Logout_ReturnsOk() As Task
        ' Act
        Dim result As IActionResult = Await _controller.Logout()

        ' Assert
        Assert.IsInstanceOf(Of OkResult)(result)
        Dim okResult As OkResult = CType(result, OkResult)
        Assert.AreEqual(200, okResult.StatusCode)
    End Function

End Class
