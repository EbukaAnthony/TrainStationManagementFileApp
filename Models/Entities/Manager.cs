using System;

namespace TrainStationManagementApp.Models.Entities
{
    public class Manager : BaseEntity
    {
        public int UserId { get; set; }
        public string StaffNumber { get; set; }

        public Manager(int id, int userId, string staffNumber,bool isDeleted, DateTime dateCreated, DateTime dateUpdated) : base(id, isDeleted, dateCreated, dateUpdated)
        {
            Id = id;
            UserId = userId;
            StaffNumber = staffNumber;
            IsDeleted = isDeleted;
            DateCreated = dateCreated;
            DateUpdated = dateUpdated;
        }

        public override string ToString()
        {
            return $"{Id};{UserId};{StaffNumber};{IsDeleted};{DateCreated};{DateUpdated}";
        }

        public static Manager ToManager(string manager)
        {
            var result = manager.Split(';');
            int id = int.Parse(result[0]);
            int userId = int.Parse(result[1]);
            string staffNumber = result[2];
            bool isDeleted = bool.Parse(result[3]);
            DateTime dateCreated = DateTime.Parse(result[4]);
            DateTime dateUpdated = DateTime.Parse(result[5]);
            Manager manager1 = new Manager(id, userId, staffNumber, isDeleted, dateCreated, dateUpdated);
            return manager1;
        }
    }
}