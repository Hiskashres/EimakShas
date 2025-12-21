namespace EimakShas.DTOs
{
    public class YomHashasInfoDTO
    {
        public int MainGoal { get; set; }
        public int BonusGoal { get; set; }
        public TimeOnly EndTime { get; set; }
        public int DafimAmount { get; set; }
        public int DafimFinished {  get; set; }
        public int DafimNotFinished { get; set; }
        public int PercentageFinished { get; set; }
        public int DafimNotFinished_Bonus { get; set; }
        public int PercentageFinished_Bonus { get; set; }
    }
}
