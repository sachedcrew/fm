Imports System.Net.Http
Imports System.Threading.Tasks
Imports Microsoft.AspNetCore.Mvc.Testing
Imports NUnit.Framework

Namespace FleetManagmentApp.Tests
    <TestFixture>
    Public Class ProgramTests
        Private ReadOnly _factory As WebApplicationFactory(Of Program)

        Public Sub New()
            _factory = New WebApplicationFactory(Of Program)()
        End Sub

        <Test>
        Public Sub Test_CorsPolicy()
            ' Arrange
            Dim client As HttpClient = _factory.CreateClient()
            Dim request = New HttpRequestMessage(HttpMethod.Options, "/")
            request.Headers.Add("Origin", "http://localhost:3000")

            ' Act
            Dim response = client.SendAsync(request).Result

            ' Assert
            Assert.IsTrue(response.Headers.Contains("Access-Control-Allow-Origin"), "CORS policy does not include 'Access-Control-Allow-Origin' header.")
            Dim allowedOrigins = response.Headers.GetValues("Access-Control-Allow-Origin")
            Assert.Contains("http://localhost:3000", allowedOrigins.ToList(), "CORS policy does not include expected origin.")
        End Sub
    End Class
End Namespace
