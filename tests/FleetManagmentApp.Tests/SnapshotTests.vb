Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Metadata
Imports Microsoft.Extensions.DependencyInjection
Imports NUnit.Framework
Imports FleetManagmentApp.Models

<TestFixture>
Public Class ModelSnapshotTests

    Private _options As DbContextOptions(Of ApplicationDbContext)

    <SetUp>
    Public Sub SetUp()
        ' Configure in-memory database for testing
        _options = New DbContextOptionsBuilder(Of ApplicationDbContext)() _
            .UseSqlServer("Server=(localdb)\mssqllocaldb;Database=FleetManagmentAppTest;Trusted_Connection=True;") _
            .Options
    End Sub

    <Test>
    Public Sub Test_ModelSnapshot()
        ' Arrange
        Using context As New ApplicationDbContext(_options)
            ' Ensure the database is created
            context.Database.EnsureCreated()

            ' Act
            ' Retrieve the model snapshot
            Dim modelSnapshot As IModel = context.Model

            ' Assert
            Assert.IsNotNull(modelSnapshot, "Model snapshot should not be null")

            ' Example of assertions to validate the model snapshot
            Dim entityTypes = modelSnapshot.GetEntityTypes()
            Assert.IsTrue(entityTypes.Any(Function(e) e.ClrType = GetType(Car)), "Car entity type should be present")
            Assert.IsTrue(entityTypes.Any(Function(e) e.ClrType = GetType(ApplicationUser)), "ApplicationUser entity type should be present")
        End Using
    End Sub

End Class
