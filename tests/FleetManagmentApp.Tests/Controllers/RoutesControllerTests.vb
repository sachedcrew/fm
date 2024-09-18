Imports NUnit.Framework
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.AspNetCore.Mvc
Imports FleetManagmentApp.Controllers
Imports FleetManagmentApp.Models
Imports System.Collections.Generic
Imports System.Linq
Imports System.Threading.Tasks

Namespace FleetManagmentApp.Tests
    <TestFixture>
    Public Class RoutesControllerTests
        Private _context As ApplicationDbContext
        Private _controller As RoutesController

        <SetUp>
        Public Sub SetUp()
            ' Configure InMemory database for testing
            Dim options = New DbContextOptionsBuilder(Of ApplicationDbContext)() _
                .UseInMemoryDatabase(databaseName:="TestDatabase_" & Guid.NewGuid().ToString()) _
                .Options

            ' Initialize the database and the controller
            _context = New ApplicationDbContext(options)

            ' Seed the database with initial data
            _context.Routes.AddRange(
                New Route() With {
                    .Id = 1,
                    .StartLocation = "A",
                    .EndLocation = "B",
                    .Distance = 10.0,
                    .FuelUsed = 5.0,
                    .StartTime = DateTime.Now,
                    .EndTime = DateTime.Now.AddHours(1),
                    .CarId = 1
                },
                New Route() With {
                    .Id = 2,
                    .StartLocation = "C",
                    .EndLocation = "D",
                    .Distance = 20.0,
                    .FuelUsed = 10.0,
                    .StartTime = DateTime.Now,
                    .EndTime = DateTime.Now.AddHours(2),
                    .CarId = 2
                }
            )
            _context.SaveChanges()

            _controller = New RoutesController(_context)
        End Sub

        <Test>
        Public Async Function GetRoute_InvalidId_ReturnsNotFound() As Task
            ' Act
            Dim result = Await _controller.GetRoute(999)

            ' Assert
            Dim actionResult = CType(result, ActionResult(Of Route))
            Dim notFoundResult = CType(actionResult.Result, NotFoundResult)
            Assert.IsNotNull(notFoundResult)
        End Function

        <Test>
        Public Async Function PostRoute_ValidRoute_ReturnsCreatedAtAction() As Task
            ' Arrange
            Dim newRoute = New Route() With {
                .Id = 3,
                .StartLocation = "E",
                .EndLocation = "F",
                .Distance = 30.0,
                .FuelUsed = 15.0,
                .StartTime = DateTime.Now,
                .EndTime = DateTime.Now.AddHours(3),
                .CarId = 1
            }

            ' Act
            Dim result = Await _controller.PostRoute(newRoute)

            ' Assert
            Dim actionResult = CType(result, ActionResult(Of Route))
            Dim createdAtActionResult = CType(actionResult.Result, CreatedAtActionResult)
            Assert.IsNotNull(createdAtActionResult)
            Assert.AreEqual("GetRoute", createdAtActionResult.ActionName)
            Assert.AreEqual(3, CType(createdAtActionResult.RouteValues("id"), Integer))
        End Function


        <Test>
        Public Async Function PutRoute_InvalidId_ReturnsBadRequest() As Task
            ' Arrange
            Dim route = New Route() With {
                .Id = 1,
                .StartLocation = "G",
                .EndLocation = "H",
                .Distance = 40.0,
                .FuelUsed = 20.0,
                .StartTime = DateTime.Now,
                .EndTime = DateTime.Now.AddHours(4),
                .CarId = 2
            }

            ' Act
            Dim result = Await _controller.PutRoute(999, route)

            ' Assert
            Assert.IsInstanceOf(Of BadRequestResult)(result)
        End Function

        <Test>
        Public Async Function DeleteRoute_ValidId_ReturnsNoContent() As Task
            ' Act
            Dim result = Await _controller.DeleteRoute(1)

            ' Assert
            Assert.IsInstanceOf(Of NoContentResult)(result)
        End Function

        <Test>
        Public Async Function DeleteRoute_InvalidId_ReturnsNotFound() As Task
            ' Act
            Dim result = Await _controller.DeleteRoute(999)

            ' Assert
            Assert.IsInstanceOf(Of NotFoundResult)(result)
        End Function
    End Class
End Namespace
