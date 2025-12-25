using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

        public string? PasswordHash { get; set; }
        public int? ChavrisaId { get; set; }

        public int DafimAmount { get; set; }

        public int DafimFinished { get; set; }

        public int PercentageFinished { get; set; }

        public bool DafPerDay { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        public bool HasText { get; set; }

        public bool IsAdmin { get; set; }

        public User? Chavrisa { get; set; }

        //[JsonIgnore]
        public User? LinkedTo { get; set; }

        public ICollection<UserUmid>? UserUmidim { get; set; } = [];

        public ICollection<YomHashas_Daf>? yomHashas_Dafim { get; set; } = [];
    }
}
