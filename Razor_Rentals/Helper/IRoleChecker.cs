namespace Razor_Rentals.Helper
{
    public interface IRoleChecker
    {
         bool IsAdmin(string userEmail);
    }
}
