Imports NUnit.Framework
Imports FleetManagmentApp.Models
Imports System

<TestFixture>
Public Class RouteTests

    <Test>
    Public Sub Route_Properties_Are_Set_Correctly()
        ' Arrange
        Dim route As New Route With {
            .Id = 1,
            .CarId = 101,
            .Car = New Car With {
                .Id = 1,
                .Make = "Toyota",
                .Model = "Camry",
                .Year = 2020,
                .VIN = "1234567890ABCDEFG"
            },
            .StartLocation = "Start City",
            .EndLocation = "End City",
            .StartTime = New DateTime(2024, 1, 1, 8, 0, 0),
            .EndTime = New DateTime(2024, 1, 1, 10, 0, 0),
            .Distance = 150.5,
            .FuelUsed = 12.3
        }

        ' Act & Assert
        Assert.AreEqual(1, route.Id)
        Assert.AreEqual(101, route.CarId)
        Assert.IsNotNull(route.Car)
        Assert.AreEqual("Toyota", route.Car.Make)
        Assert.AreEqual("Camry", route.Car.Model)
        Assert.AreEqual(2020, route.Car.Year)
        Assert.AreEqual("1234567890ABCDEFG", route.Car.VIN)
        Assert.AreEqual("Start City", route.StartLocation)
        Assert.AreEqual("End City", route.EndLocation)
        Assert.AreEqual(New DateTime(2024, 1, 1, 8, 0, 0), route.StartTime)
        Assert.AreEqual(New DateTime(2024, 1, 1, 10, 0, 0), route.EndTime)
        Assert.AreEqual(150.5, route.Distance)
        Assert.AreEqual(12.3, route.FuelUsed)
    End Sub

    <Test>
    Public Sub Route_Default_Constructor_Sets_Properties_To_Default_Values()
        ' Arrange
        Dim route As New Route()

        ' Act & Assert
        Assert.AreEqual(0, route.Id)
        Assert.AreEqual(0, route.CarId)
        Assert.IsNull(route.Car)
        Assert.IsNull(route.StartLocation)
        Assert.IsNull(route.EndLocation)
        Assert.AreEqual(DateTime.MinValue, route.StartTime)
        Assert.AreEqual(DateTime.MinValue, route.EndTime)
        Assert.AreEqual(0, route.Distance)
        Assert.AreEqual(0, route.FuelUsed)
    End Sub

    <Test>
    Public Sub Route_Set_Properties_Changes_Values()
        ' Arrange
        Dim route As New Route()

        ' Act
        route.Id = 2
        route.CarId = 202
        route.Car = New Car With {
            .Id = 2,
            .Make = "Honda",
            .Model = "Civic",
            .Year = 2021,
            .VIN = "0987654321HGFEDCBA"
        }
        route.StartLocation = "New Start City"
        route.EndLocation = "New End City"
        route.StartTime = New DateTime(2024, 2, 1, 9, 0, 0)
        route.EndTime = New DateTime(2024, 2, 1, 11, 0, 0)
        route.Distance = 200.7
        route.FuelUsed = 15.5

        ' Assert
        Assert.AreEqual(2, route.Id)
        Assert.AreEqual(202, route.CarId)
        Assert.IsNotNull(route.Car)
        Assert.AreEqual("Honda", route.Car.Make)
        Assert.AreEqual("Civic", route.Car.Model)
        Assert.AreEqual(2021, route.Car.Year)
        Assert.AreEqual("0987654321HGFEDCBA", route.Car.VIN)
        Assert.AreEqual("New Start City", route.StartLocation)
        Assert.AreEqual("New End City", route.EndLocation)
        Assert.AreEqual(New DateTime(2024, 2, 1, 9, 0, 0), route.StartTime)
        Assert.AreEqual(New DateTime(2024, 2, 1, 11, 0, 0), route.EndTime)
        Assert.AreEqual(200.7, route.Distance)
        Assert.AreEqual(15.5, route.FuelUsed)
    End Sub

End Class
