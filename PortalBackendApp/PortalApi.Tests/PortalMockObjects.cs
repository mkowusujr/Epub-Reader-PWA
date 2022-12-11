using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PortalApi.Tests;

public class PortalMockObjects
{
    #region MockUsers
    public User mockUserJohnDoe { get; }
    public User mockUserJaneDoe { get; }
    public User mockUserSueDoe { get; }
    public List<User> mockUsers { get; }
    #endregion

    public PortalMockObjects()
    {
        #region MockUsers
        mockUserJohnDoe = new User(
            name: "John Doe",
            email: "john@doe.com",
            password: "Adaefwfaasw"
        );
        mockUserJaneDoe = new User(
            name: "Jane Doe",
            email: "jane@doe.com",
            password: "Adaefwfaasw"
        );
        mockUserSueDoe = new User(name: "Sue Doe", email: "sue@doe.com", password: "Adaefwfaasw");
        mockUsers = new List<User>() { mockUserJohnDoe, mockUserJaneDoe, mockUserSueDoe };
        #endregion
    }

    public EBookInputModel GenerateEBookForUser(int userId, int bookOption = 1)
    {
        string filePath;
        switch (bookOption)
        {
            case 2:
                filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                @"Services.Tests\Resources\moby-dick.epub");
                break;
            case 3:
                filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                @"Services.Tests\Resources\great-gatsby.epub");
                break;
            default:
                filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                @"Services.Tests\Resources\frankenstein.epub");
                break;
        }

        FormFile franksteinFormFile;
        var stream = File.OpenRead(filePath);

        franksteinFormFile = new FormFile(
            stream,
            0,
            stream.Length,
            null,
            Path.GetFileName(stream.Name)
        )
        {
            Headers = new HeaderDictionary(),
            ContentType = "application/epub+zip"
        };

        return new EBookInputModel(
            userId: userId,
            epubFile: franksteinFormFile,
            collections: new List<Collection>()
        );
    }
}
