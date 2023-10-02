namespace Taxify.Web.Models;

public class UserModel
{
    public string Username {get; set;}
    public string FirstName {get;set; }
    public string LastName { get;set; }
    public string Phone {get;set; }
    public string? ImagePath {get; set; }
    public string? ImageName {get; set; }
    public IFormFile file {get ; set;} 
}
