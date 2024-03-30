using Microsoft.AspNetCore.Identity;

namespace WA_InfoShop.Models;

public class ApplicationUser : IdentityUser
{
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime CreatedDate { get; set; }
}