using System.Collections.Generic;
using TrainStationManagementApp.Models.Entities;
using TrainStationManagementApp.Models.Enums;

namespace TrainStationManagementApp.Managers.Interfaces
{
    public interface ISuperAdminManager
    {
        public SuperAdmin Get(int id);
        public SuperAdmin Get(string email);
        public User Update(string firstName, string lastName, User user, string phoneNumber, Gender gender, string address);
        // public SuperAdmin Update(User user);
        public bool Delete(int id);
        public List<SuperAdmin> GetAll();
    }
}