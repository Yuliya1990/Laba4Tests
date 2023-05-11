namespace EasylifeAPI.Services
{
    public interface IUserService
    {
        List<User> GetUsers();
        User GetById(int id);
        void Register(UserRegister newUser);
        
    }
}
