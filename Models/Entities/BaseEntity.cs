using System;

namespace TrainStationManagementApp.Models.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }


        public BaseEntity(int id, bool isDeleted, DateTime dateCreated, DateTime dateUpdated)
        {
            Id = id;
            IsDeleted = isDeleted;
            DateCreated = dateCreated;
            DateUpdated = dateUpdated;
        }
    }
}