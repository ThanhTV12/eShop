namespace eShopSolution.ViewModel.System.User
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public override string ToString()
        {
            return "UserName: " + UserName + "\n"
                  + "Password: " + Password + "\n"
                  + "RememberMe: " + RememberMe + "\n";
        }
    }
}