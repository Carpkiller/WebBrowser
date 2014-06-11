namespace WebBrowser.SpravaHesiel
{
    public class UserData
    {
        public string Login { get; set; }
        public string Heslo { get; set; }

        public UserData(string login, string heslo)
        {
            Login = login;
            Heslo = heslo;
        }
    }
}
