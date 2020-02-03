namespace BookStore.Domain.Constants
{
    public static class DbColumnConstraints
    {
        public const int FirstNameLength = 100;

        public const int LastNameLength = 100;

        public const int EmailLength = 320;

        public const int PasswordLength = 128;

        public const int RoleLength = 50;

        public const int BookCategoryLength = 100;

        public const int BookCommentLength = 3000;

        public const int BookTitleLength = 100;

        public const int DescriptionLength = 3000;

        public const int ImageSize = 5 * 1024 * 1024;
    }
}
