namespace EimakShas.Models
{
    public class Masechta
    {
        public int MasechtaId { get; set; }
        public int ShasCycleId { get; set; }
        public string MasechtaName { get; set; }
        public int MasechtaDafCount { get; set; }
        public int MasechtaOrder { get; set; }
        public bool LastUmidDoubleSided { get; set; }
        public ShasCycle ShasCycle { get; set; }
        public ICollection<Daf> Dafim { get; set; } = new List<Daf>();

    }
}