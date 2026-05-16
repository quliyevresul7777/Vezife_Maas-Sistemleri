namespace SalaryManagementSystem;

public class Position
{
    public int Id { get; set; }

    public string Name { get; set; }

    public decimal BaseSalary { get; set; }
}

public class Employee
{
    public int Id { get; set; }

    public string FullName { get; set; }

    public int PositionId { get; set; }

    public DateTime HireDate { get; set; }
}

public class Salary
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public int Month { get; set; }

    public int Year { get; set; }

    public decimal Bonus { get; set; }

    public decimal Deduction { get; set; }

    public decimal FinalSalary { get; set; }

    public DateTime CreatedDate { get; set; }
}

public class CreateSalaryDto
{
    public int EmployeeId { get; set; }

    public int Month { get; set; }

    public int Year { get; set; }

    public decimal Bonus { get; set; }

    public decimal Deduction { get; set; }
}
