namespace WpfApp.MVVM.Model
{
    public static class UserSession
    {
        public static int LoggedInUserId { get; private set; }

        public static void SetCurrentUser(int userId)
        {
            LoggedInUserId = userId;
        }
    }
}
