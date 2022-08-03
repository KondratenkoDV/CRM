using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Employee
{
    public class CreateEmployeeDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int PositionId { get; set; }
    }
}
