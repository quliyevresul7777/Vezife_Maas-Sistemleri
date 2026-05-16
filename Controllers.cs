using Microsoft.AspNetCore.Mvc;

namespace SalaryManagementSystem;

[ApiController]
[Route("api/positions")]
public class PositionController : ControllerBase
{
    [HttpPost]
    public IActionResult AddPosition(Position position)
    {
        position.Id = DataStore.Positions.Count + 1;

        DataStore.Positions.Add(position);

        return Ok(position);
    }

    [HttpGet]
    public IActionResult GetPositions()
    {
        return Ok(DataStore.Positions);
    }
}

[ApiController]
[Route("api/employees")]
public class EmployeeController : ControllerBase
{
    [HttpPost]
    public IActionResult AddEmployee(Employee employee)
    {
        Position position = DataStore.Positions.FirstOrDefault(x => x.Id == employee.PositionId);

        if (position == null)
        {
            return BadRequest("Position not found");
        }

        employee.Id = DataStore.Employees.Count + 1;

        DataStore.Employees.Add(employee);

        return Ok(employee);
    }

    [HttpGet]
    public IActionResult GetEmployees()
    {
        return Ok(DataStore.Employees);
    }

    [HttpGet("{id}")]
    public IActionResult GetEmployee(int id)
    {
        Employee employee = DataStore.Employees.FirstOrDefault(x => x.Id == id);

        if (employee == null)
        {
            return NotFound();
        }

        return Ok(employee);
    }
}

[ApiController]
[Route("api/salaries")]
public class SalaryController : ControllerBase
{
    [HttpPost("calculate")]
    public IActionResult CalculateSalary(CreateSalaryDto dto)
    {
        if (dto.Bonus < 0 || dto.Deduction < 0)
        {
            return BadRequest("Bonus or Deduction cannot be negative");
        }

        if (dto.Month < 1 || dto.Month > 12)
        {
            return BadRequest("Month must be between 1 and 12");
        }

        if (dto.Year < 2000)
        {
            return BadRequest("Year is invalid");
        }

        Employee employee = DataStore.Employees.FirstOrDefault(x => x.Id == dto.EmployeeId);

        if (employee == null)
        {
            return BadRequest("Employee not found");
        }

        Position position = DataStore.Positions.FirstOrDefault(x => x.Id == employee.PositionId);

        decimal finalSalary = position.BaseSalary + dto.Bonus - dto.Deduction;

        Salary salary = new Salary();

        salary.Id = DataStore.Salaries.Count + 1;
        salary.EmployeeId = dto.EmployeeId;
        salary.Month = dto.Month;
        salary.Year = dto.Year;
        salary.Bonus = dto.Bonus;
        salary.Deduction = dto.Deduction;
        salary.FinalSalary = finalSalary;
        salary.CreatedDate = DateTime.Now;

        DataStore.Salaries.Add(salary);

        return Ok(salary);
    }

    [HttpGet("{employeeId}")]
    public IActionResult GetSalaryHistory(int employeeId)
    {
        List<Salary> salaries = DataStore.Salaries
            .Where(x => x.EmployeeId == employeeId)
            .ToList();

        return Ok(salaries);
    }
}
