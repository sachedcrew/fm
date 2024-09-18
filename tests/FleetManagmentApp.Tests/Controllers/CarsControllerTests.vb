Imports NUnit.Framework
Imports Moq
Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.EntityFrameworkCore
Imports FleetManagmentApp.Controllers
Imports FleetManagmentApp.Models
Imports System.Threading.Tasks
Imports Microsoft.EntityFrameworkCore.ChangeTracking
Imports System.Threading

<TestFixture>
Public Class CarsControllerTests
    Private _contextMock As Mock(Of ApplicationDbContext)
    Private _carsController As CarsController

    <SetUp>
    Public Sub SetUp()
        ' Inicjalizacja mocków
        _contextMock = New Mock(Of ApplicationDbContext)()
        _carsController = New CarsController(_contextMock.Object)
    End Sub

    <Test>
    Public Async Function GetCars_ReturnsListOfCars() As Task
        ' Arrange
        Dim cars As New List(Of Car) From {
            New Car() With {.Id = 1, .Make = "Toyota", .Model = "Corolla"},
            New Car() With {.Id = 2, .Make = "Honda", .Model = "Civic"}
        }
        Dim mockDbSet As Mock(Of DbSet(Of Car)) = GetQueryableMockDbSet(cars)
        _contextMock.Setup(Function(c) c.Cars).Returns(mockDbSet.Object)

        ' Act
        Dim result As ActionResult(Of IEnumerable(Of Car)) = Await _carsController.GetCars()

        ' Assert
        Assert.IsInstanceOf(Of ActionResult(Of IEnumerable(Of Car)))(result)
        Dim okResult As OkObjectResult = CType(result.Result, OkObjectResult)
        Assert.IsNotNull(okResult)
        Assert.AreEqual(200, okResult.StatusCode)
        Assert.AreEqual(2, CType(okResult.Value, IEnumerable(Of Car)).Count())
    End Function

    <Test>
    Public Async Function GetCar_ValidId_ReturnsCar() As Task
        ' Arrange
        Dim car As New Car() With {.Id = 1, .Make = "Toyota", .Model = "Corolla"}
        Dim mockDbSet As Mock(Of DbSet(Of Car)) = GetQueryableMockDbSet(New List(Of Car) From {car})
        _contextMock.Setup(Function(c) c.Cars.FindAsync(1)).ReturnsAsync(car)

        ' Act
        Dim result As ActionResult(Of Car) = Await _carsController.GetCar(1)

        ' Assert
        Assert.IsInstanceOf(Of ActionResult(Of Car))(result)
        Dim okResult As OkObjectResult = CType(result.Result, OkObjectResult)
        Assert.IsNotNull(okResult)
        Assert.AreEqual(200, okResult.StatusCode)
        Assert.AreEqual(car, okResult.Value)
    End Function

    <Test>
    Public Async Function GetCar_InvalidId_ReturnsNotFound() As Task
        ' Arrange
        _contextMock.Setup(Function(c) c.Cars.FindAsync(1)).ReturnsAsync(CType(Nothing, Car))

        ' Act
        Dim result As ActionResult(Of Car) = Await _carsController.GetCar(1)

        ' Assert
        Assert.IsInstanceOf(Of NotFoundResult)(result.Result)
    End Function

    <Test>
    Public Async Function PostCar_ValidCar_ReturnsCreatedAtAction() As Task
        ' Arrange
        Dim car As New Car() With {.Id = 1, .Make = "Toyota", .Model = "Corolla"}
        _contextMock.Setup(Function(c) c.Cars.Add(It.IsAny(Of Car)())).Returns(New Mock(Of EntityEntry(Of Car))().Object)
        _contextMock.Setup(Function(c) c.SaveChangesAsync(It.IsAny(Of CancellationToken))).ReturnsAsync(1)

        ' Act
        Dim result As ActionResult(Of Car) = Await _carsController.PostCar(car)

        ' Assert
        Assert.IsInstanceOf(Of CreatedAtActionResult)(result.Result)
        Dim createdAtActionResult As CreatedAtActionResult = CType(result.Result, CreatedAtActionResult)
        Assert.AreEqual(201, createdAtActionResult.StatusCode)
        Assert.AreEqual(car, createdAtActionResult.Value)
    End Function

    <Test>
    Public Async Function PutCar_ValidId_ReturnsNoContent() As Task
        ' Arrange
        Dim car As New Car() With {.Id = 1, .Make = "Toyota", .Model = "Corolla"}
        _contextMock.Setup(Function(c) c.Cars.Any(It.IsAny(Of Func(Of Car, Boolean)))).Returns(True)
        _contextMock.Setup(Function(c) c.Entry(car).State = EntityState.Modified)
        _contextMock.Setup(Function(c) c.SaveChangesAsync(It.IsAny(Of CancellationToken))).ReturnsAsync(1)

        ' Act
        Dim result As IActionResult = Await _carsController.PutCar(1, car)

        ' Assert
        Assert.IsInstanceOf(Of NoContentResult)(result)
    End Function

    <Test>
    Public Async Function DeleteCar_ValidId_ReturnsNoContent() As Task
        ' Arrange
        Dim car As New Car() With {.Id = 1, .Make = "Toyota", .Model = "Corolla"}
        Dim mockDbSet As Mock(Of DbSet(Of Car)) = GetQueryableMockDbSet(New List(Of Car) From {car})
        _contextMock.Setup(Function(c) c.Cars.FindAsync(1)).ReturnsAsync(car)
        _contextMock.Setup(Function(c) c.Cars.Remove(car))
        _contextMock.Setup(Function(c) c.SaveChangesAsync(It.IsAny(Of CancellationToken))).ReturnsAsync(1)

        ' Act
        Dim result As IActionResult = Await _carsController.DeleteCar(1)

        ' Assert
        Assert.IsInstanceOf(Of NoContentResult)(result)
    End Function

    <Test>
    Public Async Function DeleteCar_InvalidId_ReturnsNotFound() As Task
        ' Arrange
        _contextMock.Setup(Function(c) c.Cars.FindAsync(1)).ReturnsAsync(CType(Nothing, Car))

        ' Act
        Dim result As IActionResult = Await _carsController.DeleteCar(1)

        ' Assert
        Assert.IsInstanceOf(Of NotFoundResult)(result)
    End Function

    ' Pomocnicza metoda do mockowania DbSet
    Private Function GetQueryableMockDbSet(Of TEntity As Class)(sourceList As List(Of TEntity)) As Mock(Of DbSet(Of TEntity))
        Dim queryable As IQueryable(Of TEntity) = sourceList.AsQueryable()
        Dim dbSetMock As New Mock(Of DbSet(Of TEntity))()
        dbSetMock.As(Of IQueryable(Of TEntity))().Setup(Function(m) m.Provider).Returns(queryable.Provider)
        dbSetMock.As(Of IQueryable(Of TEntity))().Setup(Function(m) m.Expression).Returns(queryable.Expression)
        dbSetMock.As(Of IQueryable(Of TEntity))().Setup(Function(m) m.ElementType).Returns(queryable.ElementType)
        dbSetMock.As(Of IQueryable(Of TEntity))().Setup(Function(m) m.GetEnumerator()).Returns(queryable.GetEnumerator())
        Return dbSetMock
    End Function
End Class
