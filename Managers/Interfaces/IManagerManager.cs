using System.Collections.Generic;
using TrainStationManagementApp.Models.Entities;
using TrainStationManagementApp.Models.Enums;

namespace TrainStationManagementApp.Managers.Interfaces
{
    public interface IManagerManager
    {
       public Manager CreateManager(string firstName, string lastName, string email, string password, string phoneNumber, Gender gender, string address,int userId);
    // public Manager CreateManager(int userId);
       public Manager GetManager(int id);
       public Manager GetManager(string staffNumber);
       public List<Manager> GetAllManager();
       public User UpdateManager(string firstName, string lastName, User user, string phoneNumber, Gender gender, string address);
       //public Manager UpdateManager(Manager manager);
       public bool DeleteManager(int id);
       
    }
}