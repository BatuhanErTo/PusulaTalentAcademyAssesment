using System;
using System.Linq;
using System.Xml.Linq;
using System.Text.Json;
using System.Collections;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Globalization;

public class Question3
{
     private const string TURKISH_CULTURE_INFO_CODE = "tr-TR";
    public static string FilterPeopleFromXml(string xmlData)
    {
        List<Person> personList;

        try
        {
            personList = GetFilteredPeopleFromXmlData(xmlData);
        }
        catch (System.Exception e)
        {
            Console.WriteLine(e.Message);
            personList = new List<Person>();
        }

        if (personList.Count == 0) return JsonSerializer.Serialize(new PersonListReport(new List<string>(), 0, 0, 0, 0));

        var tr = CultureInfo.GetCultureInfo(TURKISH_CULTURE_INFO_CODE);

        List<string> names = personList.Select(person => person.Name).OrderBy(name => name, StringComparer.Create(tr, CompareOptions.IgnoreCase)).ToList();
        double totalSalary = personList.Sum(person => person.Salary);
        double maxSalary = personList.Max(person => person.Salary);
        int count = personList.Count;
        double avgSalary = totalSalary / count;

        PersonListReport report = new PersonListReport(names, totalSalary, avgSalary, maxSalary, count);
        return JsonSerializer.Serialize(report, new JsonSerializerOptions { Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) });
    }

    private static List<Person> GetFilteredPeopleFromXmlData(string xmlData)
    {
        if (string.IsNullOrWhiteSpace(xmlData)) throw new ArgumentNullException("XML Data cannot be null");

        XDocument xmlDocument = XDocument.Parse(xmlData);

        return xmlDocument.Descendants("Person")
                                .Select(
                                    person => new
                                    {
                                        Name = person.Element("Name")?.Value ?? string.Empty,
                                        Age = int.TryParse(person.Element("Age")?.Value, out var age) ? age : 0,
                                        Department = person.Element("Department")?.Value ?? string.Empty,
                                        Salary = int.TryParse(person.Element("Salary")?.Value, out var salary) ? salary : 0,
                                        HireDate = DateOnly.TryParse(person.Element("HireDate")?.Value, out var hireDate) ? hireDate : DateOnly.MinValue
                                    }
                                )
                                .Where(obj => obj.Age > 30)
                                .Where(obj => string.Equals(obj.Department, "IT", StringComparison.OrdinalIgnoreCase))
                                .Where(obj => obj.Salary > 5000.00)
                                .Where(obj => obj.HireDate.Year < 2019)
                                .Select(
                                    obj => new Person
                                    {
                                        Name = obj.Name,
                                        Age = obj.Age,
                                        Department = obj.Department,
                                        Salary = obj.Salary,
                                        HireDate = obj.HireDate
                                    }).ToList();
    }

    private record PersonListReport(List<string> Names, double TotalSalary, double AverageSalary, double MaxSalary, int Count);
    private class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }
        public double Salary { get; set; }
        public DateOnly HireDate { get; set; }
    }
}