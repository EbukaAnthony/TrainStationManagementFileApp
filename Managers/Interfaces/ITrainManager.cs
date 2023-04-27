using System;
using System.Collections.Generic;
using TrainStationManagementApp.Models.Entities;

namespace TrainStationManagementApp.Managers.Interfaces
{
    public interface ITrainManager
    {
         public Train CreateTrain(string name, string takeOffPoint, string destination, TimeSpan takeOfTime, TimeSpan landingTime, double price, int capacity, int availableSpace);
         public Train GetTrain(int id);
         public Train GetTrain(string name);
         public List<Train> GetAllTrain();
         public List<Train> GetTrainByDeleteStatus(bool isDeleted);
         public Train UpdateTrain(string name, string takeOffPoint, string destination, TimeSpan takeOfTime, TimeSpan landingTime, double price, int capacity, int availableSpace);
         public bool DeleteTrain(int id);
         public bool DeleteTrain(string name);
         public void RefreshFile();
    }
}