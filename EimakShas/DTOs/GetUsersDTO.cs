namespace EimakShas.DTOs
{
    public class GetUsersDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Phone { get; set; }
        public int DafimAmount { get; set; }
        public int DafimCompleted { get; set; }
        public int DafimNotCompleted { get; set; }
        public int PercentageCompleted { get; set; }
        public bool DafPerDay { get; set; }
        public bool HasText { get; set; }
        public int? ChavrisaId { get; set; }
        public string? ChavrisaName { get; set; }
    }
}
