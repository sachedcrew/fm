Imports NUnit.Framework
Imports FleetManagmentApp.Models

<TestFixture>
Public Class CarTests

    <Test>
    Public Sub Car_Properties_Are_Set_Correctly()
        ' Arrange
        Dim car As New Car With {
            .Id = 1,
            .Make = "Toyota",
            .Model = "Camry",
            .Year = 2020,
            .VIN = "1234567890ABCDEFG"
        }

        ' Act & Assert
        Assert.AreEqual(1, car.Id)
        Assert.AreEqual("Toyota", car.Make)
        Assert.AreEqual("Camry", car.Model)
        Assert.AreEqual(2020, car.Year)
        Assert.AreEqual("1234567890ABCDEFG", car.VIN)
    End Sub

    <Test>
    Public Sub Car_Default_Constructor_Sets_Properties_To_Default_Values()
        ' Arrange
        Dim car As New Car()

        ' Act & Assert
        Assert.AreEqual(0, car.Id)
        Assert.IsNull(car.Make)
        Assert.IsNull(car.Model)
        Assert.AreEqual(0, car.Year)
        Assert.IsNull(car.VIN)
    End Sub

    <Test>
    Public Sub Car_Set_Properties_Changes_Values()
        ' Arrange
        Dim car As New Car()

        ' Act
        car.Id = 2
        car.Make = "Honda"
        car.Model = "Civic"
        car.Year = 2021
        car.VIN = "0987654321HGFEDCBA"

        ' Assert
        Assert.AreEqual(2, car.Id)
        Assert.AreEqual("Honda", car.Make)
        Assert.AreEqual("Civic", car.Model)
        Assert.AreEqual(2021, car.Year)
        Assert.AreEqual("0987654321HGFEDCBA", car.VIN)
    End Sub

End Class
