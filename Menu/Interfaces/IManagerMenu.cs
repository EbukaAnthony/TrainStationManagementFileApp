namespace TrainStationManagementApp.Menu.Interfaces
{
    public interface IManagerMenu
    {
        public void RealManagerMenu(string email);
        public void CreateTrainMenu();
        public void ViewTrainMenu();
        public void ViewAllTrainMenu();
        public void ViewAllPassengerMenu();
        public void UpdatePassengerMenu();
        public void DeleteTrainMenu();
        public void UpdateTrainMenu();
        public void Logout();
    }
}