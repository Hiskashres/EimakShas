namespace EimakShas.Models
{
    public class Umid
    {
        public int UmidId { get; set; }
        public int DafId { get; set; }
        public string UmidLetter { get; set; }
        public bool IsCompleted_Umid { get; set; }
        public int? UserId { get; set; }
        public Daf Daf { get; set; }
        public List<UserUmid> UserUmidim { get; set; } = [];
    }
}
