Imports NUnit.Framework
Imports FleetManagmentApp.Models

<TestFixture>
Public Class LoginModelTests

    <Test>
    Public Sub LoginModel_Properties_Are_Set_Correctly()
        ' Arrange
        Dim loginModel As New LoginModel With {
            .Email = "user@example.com",
            .Password = "SecureP@ssw0rd"
        }

        ' Act & Assert
        Assert.AreEqual("user@example.com", loginModel.Email)
        Assert.AreEqual("SecureP@ssw0rd", loginModel.Password)
    End Sub

    <Test>
    Public Sub LoginModel_Default_Constructor_Sets_Properties_To_Default_Values()
        ' Arrange
        Dim loginModel As New LoginModel()

        ' Act & Assert
        Assert.IsNull(loginModel.Email)
        Assert.IsNull(loginModel.Password)
    End Sub

    <Test>
    Public Sub LoginModel_Set_Properties_Changes_Values()
        ' Arrange
        Dim loginModel As New LoginModel()

        ' Act
        loginModel.Email = "anotheruser@example.com"
        loginModel.Password = "NewP@ssw0rd"

        ' Assert
        Assert.AreEqual("anotheruser@example.com", loginModel.Email)
        Assert.AreEqual("NewP@ssw0rd", loginModel.Password)
    End Sub

End Class
