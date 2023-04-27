using System;

namespace TrainStationManagementApp.Models.Entities
{
    public class SuperAdmin : BaseEntity
    {
        public int UserId { get; set; }

        public SuperAdmin(int id, int userId, bool isDeleted, DateTime dateCreated, DateTime dateUpdated) : base(id, isDeleted, dateCreated, dateUpdated)
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

        public static SuperAdmin ToSuperAdmin(string admin)
        {
            var result = admin.Split(';');
            int id = int.Parse(result[0]);
            int userId = int.Parse(result[1]);
            bool isDeleted = bool.Parse(result[2]);
            DateTime dateCreated =DateTime.Parse(result[3]);
            DateTime dateUpdated =DateTime.Parse(result[4]);
            SuperAdmin superAdmin = new SuperAdmin(id, userId, isDeleted, dateCreated, dateUpdated);
            return superAdmin;
        }
    }
}