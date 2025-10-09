namespace EimakShas.Models
{
    public class ShasCycle
    {
        public int ShasCycleId {  get; set; }
        public string ShasCycleName { get; set; }
        public ICollection<Masechta> Masechtas {  get; set; } = new List<Masechta>();
    }
}