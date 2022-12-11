// using Microsoft.AspNetCore.Mvc;

// namespace PortalApi.Tests.Controller.Tests;

// public class UserControllerTests
// {
//     readonly IUserService userService = A.Fake<IUserService>();
//     readonly UserController userController;

//     public UserControllerTests() => userController = new UserController(userService);

//     [Fact]
//     public async Task TestAddUserSucessfully()
//     {
//         User mockUser = A.Fake<User>();
//         mockUser.UserId = 1;
//         A.CallTo(() => userService.AddUserAsync(A<User>.Ignored)).Returns(mockUser);

//         var response = await userController.AddUserAsync(mockUser);
//         var result = response.Result as OkObjectResult;

//         result.Should().NotBeNull();
//         result.Value.Should().Be(mockUser);
//         result.StatusCode.Should().Be(200);
//     }

//     [Fact]
//     public async Task TestAddUserUnsuccessfully()
//     {
//         A.CallTo(() => userService.AddUserAsync(A<User>.Ignored)).Throws(new Exception());

//         var response = await userController.AddUserAsync(null);
//         var result = response.Result as BadRequestResult;

//         result.Should().NotBeNull();
//         result.StatusCode.Should().Be(400);
//     }

//     [Fact]
//     public async Task TestGetUserSucessfully()
//     {
//         User mockUser = A.Fake<User>();
//         mockUser.UserId = 1;
//         A.CallTo(() => userService.GetUserAsync(A<int>.Ignored)).Returns(mockUser);

//         var response = await userController.GetUserAsync(userId: 1);
//         var result = response.Result as OkObjectResult;

//         result.Should().NotBeNull();
//         result.Value.Should().Be(mockUser);
//         result.StatusCode.Should().Be(200);
//     }

//     [Fact]
//     public async Task TestGetUserUnsucessfully()
//     {
//         // A.CallTo(() => userService.GetUserAsync(A<int>.Ignored)).Throws(new Exception());

//         var response = await userController.GetUserAsync(userId: 100);
//         var result = response.Result as OkObjectResult;

//         result.Should().NotBeNull();
//         // Console.WriteLine(result.Value);
//         result.Value.Should().NotBeNull();
//         result.StatusCode.Should().Be(200);
//     }

//     [Fact]
//     public async Task TestDeleteUserSucessfully()
//     {
//         bool expectedResult = true;
//         User mockUser = A.Fake<User>();
//         mockUser.UserId = 1;
//         A.CallTo(() => userService.DeleteUserAsync(A<int>.Ignored)).Returns(expectedResult);

//         var response = await userController.DeleteUserAsync(userId: 1);
//         var result = response.Result as OkObjectResult;

//         result.Should().NotBeNull();
//         result.Value.Should().Be(expectedResult);
//         result.StatusCode.Should().Be(200);
//     }
// }