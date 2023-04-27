namespace TrainStationManagementApp.Menu.Interfaces
{
    public interface ISuperAdminMenu
    {
        public void RealSuperAdminMenu(string email);
        public void CreateManagerMenu();
        public void CreateTrainMenu();
        public void VieweAllUserMenu();
        public void ViewAllTrainMenu();
        public void DeleteManagerMenu();
        public void UpdateManagerMenu();
        public void Logout();
    }
}