using LoginNexus.Models;
using Microsoft.JSInterop;

namespace LoginNexus.Services
{
    public class AuthService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly List<User> _users = new()
        {
            new User { Email = "Daniel@nexus.edu", Password = "123", Role = "Professor", Name = "Dr. Daniel" },
            new User { Email = "Fernando@nexus.edu", Password = "123", Role = "Student", Name = "Fernando 01" }
        };

        public AuthService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public User? Authenticate(string email, string password)
        {
            return _users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public async Task SaveUserPersistenceAsync(string userName)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "nexus_user_name", userName);
        }

        public async Task<string?> GetPersistedUserNameAsync()
        {
            return await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", "nexus_user_name");
        }

        public async Task LogoutAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "nexus_user_name");
        }
    }
}