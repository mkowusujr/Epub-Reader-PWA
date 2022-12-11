// using Microsoft.AspNetCore.Mvc;

// namespace PortalApi.Tests.Controller.Tests;

// public class EBookControllerTests
// {
//     readonly IEBookService eBookService = A.Fake<IEBookService>();
//     readonly EBookController eBookController;

//     public EBookControllerTests() => eBookController = new EBookController(eBookService);

//     [Fact]
//     async Task TestGetEBookForUserSucessfully()
//     {
//         EBook mockEBook = A.Fake<EBook>();
//         A.CallTo(() => eBookService.GetEBookForUserAsync(A<int>.Ignored, A<int>.Ignored))
//             .Returns(mockEBook);

//         var response = await eBookController.GetEBookForUserAsync(userId: 1, eBookId: 1);
//         var result = response.Result as OkObjectResult;

//         result.Should().NotBeNull();
//         result.Value.Should().Be(mockEBook);
//         result.StatusCode.Should().Be(200);
//     }

//     [Fact]
//     void TestGetEBooksForUserSucessfully()
//     {
//         List<EBook> mockEBooks = A.CollectionOfDummy<EBook>(5).ToList();
//         A.CallTo(() => eBookService.GetEBooksForUser(A<int>.Ignored)).Returns(mockEBooks);

//         var response = eBookController.GetEBooksForUser(userId: 1);
//         var result = response.Result as OkObjectResult;

//         result.Should().NotBeNull();
//         result.Value.Should().Be(mockEBooks);
//         result.StatusCode.Should().Be(200);
//     }

//     [Fact]
//     async Task TestDeleteEBookForUserSucessfully()
//     {
//         bool expectedResult = true;
//         EBook mockEBook = A.Fake<EBook>();
//         A.CallTo(() => eBookService.DeleteEBookForUserAsync(A<int>.Ignored, A<int>.Ignored))
//             .Returns(expectedResult);

//         var response = await eBookController.DeleteEBookForUserAsync(userId: 1, eBookId: 1);
//         var result = response.Result as OkObjectResult;

//         result.Should().NotBeNull();
//         result.Value.Should().Be(expectedResult);
//         result.StatusCode.Should().Be(200);
//     }

//     [Fact]
//     async Task TestAddBookForUserSucessfully()
//     {
//         EBookInputModel mockEBookInputModel = A.Fake<EBookInputModel>();
//         EBook mockEBook = A.Fake<EBook>();
//         A.CallTo(() => eBookService.AddEBookForUserAsync(A<EBookInputModel>.Ignored)).Returns(mockEBook);

//         var response = await eBookController.AddBookForUser(mockEBookInputModel);
//         var result = response?.Result as OkObjectResult;

//         result.Should().NotBeNull();
//         result.Value.Should().Be(mockEBook);
//         result.StatusCode.Should().Be(200);
//     }
// }
