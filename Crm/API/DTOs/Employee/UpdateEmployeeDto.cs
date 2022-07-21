namespace API.DTOs.Employee
{
    public class UpdateEmployeeDto
    {
        public string NewFirstName { get; set; }

        public string NewLastName { get; set; }

        public int NewPositionId { get; set; }
    }
}
