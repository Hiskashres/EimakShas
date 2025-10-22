using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;

namespace EimakShas.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "This is a required field")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This is a required field")]
        public string LastName { get; set; }
        public string Phone {  get; set; }
        public string PasswordHash { get; set; }
        public bool DafPerDay { get; set; }
        [Required(ErrorMessage = "This is a required field")]
        public string HasText { get; set; }
        public bool IsAdmin { get; set; }
        public ICollection<Umid> Umidim { get; set; } = new List<Umid>();
    }
}
