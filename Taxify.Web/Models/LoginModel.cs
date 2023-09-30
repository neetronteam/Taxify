namespace Taxify.Web.Models;

public class LoginModel
{
    public string Phone {get;set;}
    public string Password {get;set; }
    public bool KeepLoggedIn { get; set; }
}
