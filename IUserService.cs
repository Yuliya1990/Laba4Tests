using EasylifeAPI.Models;

namespace EasylifeAPI.Services
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
        User GetById(int id);
        User GetByPhone(string phoneNumber );
        User GetByEmail(string email);
        User GetByNIE(string NIE);
        void Delete(int id);
        void AddClient(Client client);
        void AddWorker(Worker client);
        void SaveChanges();
    }
}
