namespace BookLibrary.Storage.Models.Account
{
    public class User
    {
        public string Login { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public static User FromPersistence(string login, string email, string firstName, string lastName)
        {
            return new User
            {
                Login = login,
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };
        }
    }
}
