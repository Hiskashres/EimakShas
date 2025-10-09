namespace EimakShas.Models
{
    public class Daf
    {
        public int DafId { get; set; }
        public int MasechtaId { get; set; }
        public string DafLetter { get; set; }
        public bool IsCompleted { get; set; }
        public Masechta Masechta { get; set; }
        public ICollection<Umid> Umidim { get; set; } = new List<Umid>();
    }
}