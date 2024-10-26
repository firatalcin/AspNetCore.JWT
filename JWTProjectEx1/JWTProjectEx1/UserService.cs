namespace JWTProjectEx1
{
    public class UserService : IUserService
    {
        private readonly List<User> users = new List<User>() 
        {
            new User
            {
                Id = 1,
                Name = "Fırat",
                Surname = "Alçın",
                Username = "firatalcin",
                Password = "1234"
            }
        };

        public User AuthenticateUser(string userName, string password)
        {
            return users.FirstOrDefault(x => x.Username == userName && x.Password == password);
        }
    }
}
