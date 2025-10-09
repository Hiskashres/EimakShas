using Microsoft.AspNetCore.SignalR;

namespace EimakShas.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone {  get; set; }
        public string PasswordHash { get; set; }
        public bool DafPerDay { get; set; }
        public string HasText { get; set; }
        public bool IsAdmin { get; set; }
        public ICollection<Umid> Umidim { get; set; } = new List<Umid>();
    }
}
