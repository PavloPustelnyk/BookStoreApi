using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Domain.Constants
{
    public static class UserRoles
    {
        public const string UserRole = "user";

        public const string AdminRole = "admin";

        public const string AllUsersRole = "user, admin";
    }
}
