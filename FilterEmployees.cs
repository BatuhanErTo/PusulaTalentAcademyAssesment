using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

public class Question4
{
    private const string TURKISH_CULTURE_INFO_CODE = "tr-TR";
    public static string FilterEmployees(
        IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
    {
        List<Employee> employeeList;
        try
        {
            employeeList = GetFilteredEmployeeListFromTuple(employees);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e.Message);
            employeeList = new List<Employee>();
        }


        if (employeeList.Count == 0) return JsonSerializer.Serialize(new EmployeesSummary(new List<string>(), 0, 0, 0, 0, 0));

        var tr = CultureInfo.GetCultureInfo(TURKISH_CULTURE_INFO_CODE);

        EmployeesSummary employeesSummary = CalculateEmployeeSummary(tr, employeeList);
        return JsonSerializer.Serialize(employeesSummary, new JsonSerializerOptions { Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) });

    }

    private static EmployeesSummary CalculateEmployeeSummary(CultureInfo cultureInfo, List<Employee> employeeList)
    {
        List<string> names = employeeList.Select(employee => employee.Name)
                                    .OrderByDescending(name => name.Length)
                                    .ThenBy(name => name, StringComparer.Create(cultureInfo, CompareOptions.IgnoreCase))
                                    .ToList();
        decimal totalSalary = employeeList.Sum(employees => employees.Salary);
        int employeeCount = employeeList.Count;
        decimal avgSalary = totalSalary / employeeCount;
        decimal maxSalary = employeeList.Max(employees => employees.Salary);
        decimal minSalary = employeeList.Min(employees => employees.Salary);

        return new EmployeesSummary(names, totalSalary, decimal.Parse(avgSalary.ToString("F2")), minSalary, maxSalary, employeeCount);
    }

    private static List<Employee> GetFilteredEmployeeListFromTuple(
        IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
    {
        if (employees == null) throw new ArgumentNullException("Employees cannot be null!");

        var departmentFilters = new List<string> { "IT", "FINANCE" };
        return employees.Where(emp =>
        {
            var department = emp.Department.Trim();
            var name = emp.Name.Trim();
            return !string.IsNullOrWhiteSpace(department)
                && !string.IsNullOrWhiteSpace(name)
                && departmentFilters.Contains(department.ToUpperInvariant());
        })
                                .Where(emp => emp.Age >= 25 && emp.Age <= 40)
                                .Where(emp => departmentFilters.Contains(emp.Department.Trim().ToUpperInvariant()))
                                .Where(emp => emp.Salary >= 5000m && emp.Salary <= 9000m)
                                .Where(emp => emp.HireDate.Year >= 2017)
                                .Select(emp => new Employee
                                {
                                    Name = emp.Name,
                                    Age = emp.Age,
                                    Department = emp.Department,
                                    Salary = emp.Salary,
                                    HireDate = emp.HireDate
                                }).ToList();

    }

    private record EmployeesSummary(
        List<string> Names, decimal TotalSalary, decimal AverageSalary, decimal MinSalary, decimal MaxSalary, int Count);

    private class Employee
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
    }

}