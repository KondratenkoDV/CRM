using System;

namespace Crm.Application.Crud.Employee
{
    public class EmployeeParameters
    {
        public int Id { get; set; }

        public string Name { get; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int PositionId { get; set; }
    }
}
