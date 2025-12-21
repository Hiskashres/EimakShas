namespace EimakShas.Models
{
    public class YomHashas
    {
        public int YomHashasId { get; set; }

        public int DafimAmount { get; set; }

        public int DafimCompleted { get; set; }

        public int PercentCompleted { get; set; }

        public int PercentCompleted_Bonus { get; set; }

        public int Goal { get; set; }

        public int BonusGoal { get; set; }

        public TimeOnly EndTime { get; set; }
    }
}
