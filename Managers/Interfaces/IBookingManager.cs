using System.Collections.Generic;
using TrainStationManagementApp.Models.Entities;

namespace TrainStationManagementApp.Managers.Interfaces
{
    public interface IBookingManager
    {
       public Booking CreateBooking (Passenger passenger, Train train);
       public Booking GetBooking (int id);
       Booking GetBooking(string referenceNumber);
       public List<Booking> GetAllBooking ();
       public bool DeleteBooking (int id);
    }
}