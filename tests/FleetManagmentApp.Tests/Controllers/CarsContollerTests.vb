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
    Public Class CarsControllerTests
        Private _context As ApplicationDbContext
        Private _controller As CarsController

        <SetUp>
        Public Sub SetUp()
            ' Configure InMemory database for testing
            Dim options = New DbContextOptionsBuilder(Of ApplicationDbContext)() _
                .UseInMemoryDatabase(databaseName:="TestDatabase_" & Guid.NewGuid().ToString()) _
                .Options

            ' Initialize the database and the controller
            _context = New ApplicationDbContext(options)

            ' Seed the database with initial data
            _context.Cars.AddRange(
                New Car() With {.Id = 1, .Make = "Toyota", .Model = "Corolla", .VIN = "123456", .Year = 2020},
                New Car() With {.Id = 2, .Make = "Honda", .Model = "Civic", .VIN = "654321", .Year = 2021}
            )
            _context.SaveChanges()

            _controller = New CarsController(_context)
        End Sub



        <Test>
        Public Async Function PostCar_ValidCar_ReturnsCreatedAtAction() As Task
            ' Arrange
            Dim newCar = New Car() With {.Id = 3, .Make = "Ford", .Model = "Focus", .VIN = "789012", .Year = 2022}

            ' Act
            Dim result = Await _controller.PostCar(newCar)

            ' Assert
            Dim actionResult = CType(result, ActionResult(Of Car))
            Dim createdAtActionResult = CType(actionResult.Result, CreatedAtActionResult)

            Assert.AreEqual("GetCar", createdAtActionResult.ActionName)
            Assert.AreEqual(3, CType(createdAtActionResult.RouteValues("id"), Integer))
        End Function


        <Test>
        Public Async Function PutCar_InvalidId_ReturnsBadRequest() As Task
            ' Arrange
            Dim car = New Car() With {.Id = 1, .Make = "Toyota", .Model = "Corolla", .VIN = "123456", .Year = 2023}

            ' Act
            Dim result = Await _controller.PutCar(999, car)

            ' Assert
            Assert.IsInstanceOf(Of BadRequestResult)(result)
        End Function

        <Test>
        Public Async Function DeleteCar_ValidId_ReturnsNoContent() As Task
            ' Act
            Dim result = Await _controller.DeleteCar(1)

            ' Assert
            Assert.IsInstanceOf(Of NoContentResult)(result)
        End Function

        <Test>
        Public Async Function DeleteCar_InvalidId_ReturnsNotFound() As Task
            ' Act
            Dim result = Await _controller.DeleteCar(999)

            ' Assert
            Assert.IsInstanceOf(Of NotFoundResult)(result)
        End Function
    End Class
End Namespace
