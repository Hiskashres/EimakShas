namespace EimakShas.Models.Requests
{
    public class AssignDafimRequest
    {
        public int UserId { get; set; }
        public int[] DafimIds { get; set; }
    }
}
