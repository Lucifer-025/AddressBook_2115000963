using ModelLayer.Model;

public interface IUserRL
{
    Task<User?> GetByEmailAsync(string email);
    Task AddUserAsync(User user);
    Task UpdatePasswordAsync(int userId, string newPassword);
    Task<User?> GetByIdAsync(int userId); 
}