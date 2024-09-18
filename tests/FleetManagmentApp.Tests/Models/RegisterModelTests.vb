Imports NUnit.Framework
Imports FleetManagmentApp.Models

<TestFixture>
Public Class RegisterModelTests

    <Test>
    Public Sub RegisterModel_Properties_Are_Set_Correctly()
        ' Arrange
        Dim registerModel As New RegisterModel With {
            .Email = "test@example.com",
            .Password = "P@ssw0rd"
        }

        ' Act & Assert
        Assert.AreEqual("test@example.com", registerModel.Email)
        Assert.AreEqual("P@ssw0rd", registerModel.Password)
    End Sub

    <Test>
    Public Sub RegisterModel_Default_Constructor_Sets_Properties_To_Default_Values()
        ' Arrange
        Dim registerModel As New RegisterModel()

        ' Act & Assert
        Assert.IsNull(registerModel.Email)
        Assert.IsNull(registerModel.Password)
    End Sub

    <Test>
    Public Sub RegisterModel_Set_Properties_Changes_Values()
        ' Arrange
        Dim registerModel As New RegisterModel()

        ' Act
        registerModel.Email = "newuser@example.com"
        registerModel.Password = "NewP@ssw0rd"

        ' Assert
        Assert.AreEqual("newuser@example.com", registerModel.Email)
        Assert.AreEqual("NewP@ssw0rd", registerModel.Password)
    End Sub

End Class
