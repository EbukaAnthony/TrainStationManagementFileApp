using System;

namespace TrainStationManagementApp.Models.Entities
{
    public class Passenger : BaseEntity
    {
        public int UserId { get; set; }

        public Passenger(int id, int userId, bool isDeleted, DateTime dateCreated, DateTime dateUpdated) : base(id, isDeleted, dateCreated, dateUpdated)
        {
            Id =id;
            UserId = userId;
            IsDeleted = isDeleted;
            DateCreated = dateCreated;
            DateUpdated = dateUpdated;
        }

        public override string ToString()
        {
            return $"{Id};{UserId};{IsDeleted};{DateCreated};{DateUpdated}";
        }

        public static Passenger ToPassenger(string passender)
        {
            var result = passender.Split(';');
            int id = int.Parse(result[0]);
            int userId = int.Parse(result[1]);
            bool isDeleted = bool.Parse(result[2]);
            DateTime dateCreated = DateTime.Parse(result[3]);
            DateTime dateUpdated = DateTime.Parse(result[4]);
            Passenger passenger = new Passenger(id, userId, isDeleted, dateCreated, dateUpdated);
            return passenger;
        }
    }
}