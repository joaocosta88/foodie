using Microsoft.AspNetCore.Identity;

namespace Foodie.Entities.Entities {

    public enum  FoodieUserRoles
    {
        Admin,
        User
    }

    public class FoodieUser : IdentityUser {
	}
}
