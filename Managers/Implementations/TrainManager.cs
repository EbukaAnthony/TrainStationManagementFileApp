using System;
using System.Collections.Generic;
using System.IO;
using TrainStationManagementApp.Managers.Interfaces;
using TrainStationManagementApp.Models.Entities;

namespace TrainStationManagementApp.Managers.Implementations
{
    public class TrainManager : ITrainManager
    {
        static List<Train> listOfTrain = new List<Train>();
        string filePath =  AppFilePath.FilePathRoot("trainManager.txt");
        public TrainManager()
        {
            //ReadTrainFromFile();  // The Constructor Will Help Me To Read The File To The List.
        }

        public void ReadTrainFromFile()
        {
            try
            {
                if (File.Exists(filePath))
                {
                        var trains = File.ReadAllLines(filePath);
                        foreach (var train in trains)
                        {
                           listOfTrain.Add(Train.ToTrain(train));
                        }
                }
                else 
                {
                    string path = AppFilePath.FilePathRoot();
                    Directory.CreateDirectory(path);
                    string fileName = "trainManager.txt";
                    string fullPath = Path.Combine(path, fileName);
                    File.Create(fullPath);
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void WriteTrainToFile(Train train)
        {
            try
            {
                using(var write = new StreamWriter(filePath, true))
                {
                    write.WriteLine(train.ToString());
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void RefreshFile()
        {
            try
            {
                File.WriteAllText(filePath , string.Empty);
                using(StreamWriter stream = new StreamWriter(filePath))
                {
                    foreach (var item in listOfTrain)
                    {
                        stream.WriteLine(item.ToString());

                    }
                }
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public Train CreateTrain(string name, string takeOffPoint, string destination, TimeSpan takeOfTime, TimeSpan landingTime, double price, int capacity, int availableSpace)
        {
            var train = TryGet(name);
            if (train == null)
            {
                int id = listOfTrain.Count + 1;
                Train train1 = new Train(id, name, takeOffPoint, destination, takeOfTime, landingTime, price, capacity, availableSpace, false, DateTime.Now, DateTime.Now);
                WriteTrainToFile(train1);
                listOfTrain.Add(train1);
                
                return train1;
            }
            return null;
        }

        public bool DeleteTrain(int id)
        {
            var train = TryGet(id);
            if (train != null && train.IsDeleted == false)
            {
                train.IsDeleted = true;
                File.WriteAllText(filePath, string.Empty);
                RefreshFile();
                return true;
            }
            
            return false;
        }

        public bool DeleteTrain(string name)
        {
            var train = TryGet(name);
            if (train != null && train.IsDeleted == false)
            {
                train.IsDeleted = true;

                return true;
            }
            File.WriteAllText(filePath, string.Empty);
            return false;
        }

        public List<Train> GetAllTrain()
        {
            return listOfTrain;
        }

        public Train GetTrain(int id)
        {
             foreach (var train in listOfTrain)
            {
                if (train.Id == id && train.IsDeleted == false)
                {
                    return train;
                }
            }
            return null;
        }

        public Train GetTrain(string name)
        {
            foreach (var train in listOfTrain)
            {
                 
                if(train.Name== name)
                {
                    return train;
                }
                
            }
            return null;
        }

        public List<Train> GetTrainByDeleteStatus(bool isDeleted)
        {
            var trains = GetAllTrain();
            var newList = new List<Train>();
            foreach (var train in trains)
            {
                if (train.IsDeleted == isDeleted)
                {
                    newList.Add(train);
                }
            } 

            return newList;
        }

        public Train UpdateTrain(string name, string takeOffPoint, string destination, TimeSpan takeOfTime, TimeSpan landingTime, double price, int capacity, int availableSpace)
        {
            var trainExist = TryGet(name);
            if (trainExist== null)
            {
                return null;
            }
           trainExist.Name = name;
           trainExist.TakeOffPoint = takeOffPoint;
           trainExist.Destination = destination;
           trainExist.TakeOffTime = takeOfTime;
           trainExist.LandingTime = landingTime;
           trainExist.Price = price;
           trainExist.Capacity = capacity;
           trainExist.AvailableSpace = availableSpace;
           //File.WriteAllText(filePath, string.Empty);
           RefreshFile();
           return trainExist;
        }

        private Train TryGet(int id)
        {
            foreach (var train in listOfTrain)
            {
                if (train.Id == id && train.IsDeleted == false)
                {
                    return train;
                }
            }
            return null;
        }

        private Train TryGet(string name)
        {
            foreach (var train in listOfTrain)
            {
                if (train.Name == name && train.IsDeleted == false)
                {
                    return train;
                }
            }
            return null;
        }

        public string TrainToOption()
        {
            var trains = GetTrainByDeleteStatus(false);
            string text = "";

            if (trains.Count > 0)
            {               
                for (int i = 0; i < trains.Count; i++)
                {
                    if (i == trains.Count - 1)
                    {
                        text +=$"Enter {trains[i].Id} for {trains[i].Name}";
                    }
                    else
                    {
                        text +=$"Enter {trains[i].Id} for {trains[i].Name}, ";
                    }
                }
                return text;
            }
            else
            {
                return "Train not found";
            }
        }

       


    }
}