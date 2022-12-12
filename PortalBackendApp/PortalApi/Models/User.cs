using System.Diagnostics.CodeAnalysis;

namespace PortalApi.Models;

public class User
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    #region Relationships

    public List<EBook>? EBooks { get; set; }

    public List<Collection>? Collections { get; set; }

    public List<Annotation>? Annotations { get; set; }

    #endregion

    public User(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
        EBooks = new List<EBook>();
        Collections = new List<Collection>();
        Annotations = new List<Annotation>();
    }

    [ExcludeFromCodeCoverage]
    public User()
    {
        // Empty constructor used for ef core
    }

    public override bool Equals(object? obj)
    {
        User? otherUser = obj as User;

        return otherUser != null
            && Name.Equals(otherUser.Name)
            && Email.Equals(otherUser.Email)
            && Password.Equals(otherUser.Password);
    }
}
