namespace Cafe.Api.Requests
{
    public class EmployeeRequest
    {
        public string Name{ get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid CafeId { get; set; } = Guid.Empty;
    }
}
