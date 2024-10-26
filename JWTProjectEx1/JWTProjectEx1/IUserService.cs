namespace JWTProjectEx1
{
    public interface IUserService
    {
        User AuthenticateUser(string userName, string password);
    }
}
