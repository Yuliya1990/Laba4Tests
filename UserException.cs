namespace EasylifeAPI.Services
{
    public class UserNotFoundException: Exception
    {
        public UserNotFoundException()
        : base("User not found") { }
    }
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string msg)
        : base(msg) { }
    }
}
