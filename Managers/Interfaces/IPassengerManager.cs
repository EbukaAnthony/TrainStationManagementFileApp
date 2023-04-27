using System.Collections.Generic;
using TrainStationManagementApp.Models.Entities;

namespace TrainStationManagementApp.Managers.Interfaces
{
    public interface IPassengerManager
    {
        Passenger Register(User user);
        public Passenger Get(int id);
        public Passenger Get(string email);
        public bool Delete(int id);
        public List<Passenger> GetAllPassenger();
    }
}