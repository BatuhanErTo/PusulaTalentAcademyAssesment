//List<int> numbers = new List<int> {1,2,2,3};
//var result = Question1.MaxIncreasingSubArrayAsJson(numbers);
//Console.WriteLine(result);
//List<string> words = new List<string> {"miscellaneous", "queue", "sky", "cooperative", "MEEAİÜhaberüĞ"};
//var result = Question2.LongestVowelSubsequenceAsJson(words);
//Console.WriteLine(result);

//string xmlData = "<People><Person><Name>Fatma</Name><Age>33</Age><Department>Finance</Department><Salary>6000</Salary><HireDate>2018-11-01</HireDate></Person></People>";
//Console.WriteLine(Question3.FilterPeopleFromXml(xmlData));

IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees
    =  [("Mehmet", 26, "Finance", 5000m, new DateTime(2021, 7, 1)),("Zeynep", 39, "IT", 9000m,
new DateTime(2018, 11, 20))];

Console.WriteLine(Question4.FilterEmployees(employees));
