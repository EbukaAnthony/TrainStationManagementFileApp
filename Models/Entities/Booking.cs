using System;

namespace TrainStationManagementApp.Models.Entities
{
    public class Booking : BaseEntity
    {
        public int PassengerId{ get;set;}
        public string PassengerEmail { get; set; }
        public int TrainId { get; set; }
        public string ReferenceNumber { get; set; }
        public int SeatNumber { get; set; }

        public Booking(int id, int passengerId, string passengerEmail, int trainId, string referenceNumber, int seatNumber, bool isDeleted, DateTime dateCreated, DateTime dateUpdated) : base(id, isDeleted, dateCreated, dateUpdated)
        {
            Id = id;
            PassengerId = passengerId;
            PassengerEmail = passengerEmail;
            TrainId = trainId;
            ReferenceNumber = referenceNumber;
            SeatNumber = seatNumber;
            IsDeleted = isDeleted;
            DateCreated = dateCreated;
            DateUpdated = dateUpdated;
        }

        public override string ToString()
        {
            return $"{Id};{PassengerId};{PassengerEmail};{TrainId};{ReferenceNumber};{SeatNumber};{IsDeleted};{DateCreated};{DateUpdated}";
        }

        public static Booking ToBooking(string booking)
        {
            var result = booking.Split(';');
            int id = int.Parse(result[0]);
            int passengerId = int.Parse(result[1]);
            string passengerEmail = result[2];
            int trainId = int.Parse(result[3]);
            string referenceNumber = result[4];
            int seatNumber = int.Parse(result[5]);
            bool isDeleted = bool.Parse(result[6]);
            DateTime dateCreated = DateTime.Parse(result[7]);
            DateTime dateUpdated = DateTime.Parse(result[8]);
            Booking booking1 = new Booking(id, passengerId, passengerEmail, trainId, referenceNumber, seatNumber, isDeleted, dateCreated, dateUpdated);
            return booking1;
        }
    }
}