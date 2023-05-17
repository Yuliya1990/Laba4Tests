using EasylifeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EasylifeAPI.Services
{
    public class UserService:IUserService
    {
        private readonly EasyLifeApiDBContext _context;
        public UserService(EasyLifeApiDBContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
        public User GetById(int id)
        {
            return _context.Users.Where(x => x.Userid == id).FirstOrDefault();
        }
        public User GetByPhone(string phoneNumber)
        {
            return _context.Users.Where(u => u.PhoneNumber == phoneNumber).FirstOrDefault();
        }
        public User GetByEmail(string email)
        {
            return _context.Users.Where(u => u.Email == email).FirstOrDefault();
        }
        public User GetByNIE(string NIE)
        {
            return _context.Users.Where(u => u.NIE == NIE).FirstOrDefault();
        }
        public void Delete(int id)
        {

        }
        public void AddClient(Client client)
        {
            _context.Clients.Add(client);

        }
        public void AddWorker(Worker worker)
        {
            _context.Workers.Add(worker);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
