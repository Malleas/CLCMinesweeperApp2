namespace CLCMinesweeperApp.Services.Data
{
    public class GameObject
    {
        public string JsonString { get; set; }

        public GameObject(string jsonString)
        {
            JsonString = jsonString;
        }
    }
}