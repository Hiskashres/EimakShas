namespace EimakShas.Models
{
    public class YomHashas_Daf
    {
        public int YomHashas_DafId { get; set; }
        public int DafId { get; set; }
        public Daf Daf { get; set; }
        //public int UserId { get; set; }
        //public User User { get; set; }
        public ICollection<User> Users { get; set; } = [];
    }
}
