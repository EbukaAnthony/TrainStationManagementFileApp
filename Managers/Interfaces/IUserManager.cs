using System.Collections.Generic;
using TrainStationManagementApp.Models.Entities;
using TrainStationManagementApp.Models.Enums;

namespace TrainStationManagementApp.Managers.Interfaces
{
    public interface IUserManager
    {
        public User LoginUser(string email, string password);
        public User RegisterUser(string firstName, string lastName, string email, string password, string phoneNumber, Gender gender, string address, RoleName role);
        public User GetUser(int id);
        public User GetUser(string email);
        public List<User> GetAllUser();
        public bool FundUserWallet(string email, double amount);
        public bool DebitUserWallet(string email, double amount);
        public bool FundManagerWallet(string managerEmail, double amount);
        public User UpdateUser(User user);
        public bool DeleteUser(string email);
    }
}