namespace EimakShas.Models
{
    public class Masechta
    {
        public int MasechtaId { get; set; }
        public string MasechtaName { get; set; }
        public int MasechtaDafCount { get; set; }
        public int MasechtaOrder { get; set; }
        public bool LastUmidDoubleSided { get; set; }
        public int DafimFinished { get; set; }
        public int PercentageFinished { get; set; }
        public ICollection<Daf> Dafim { get; set; } =[];
    }
}