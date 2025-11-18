namespace EimakShas.Models
{
    public class UserUmid
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int UmidId { get; set; }
        public Umid Umid { get; set; }
        public bool IsCompleted_UserUmid { get; set; }
    }
}
