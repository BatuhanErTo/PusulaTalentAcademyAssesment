//List<int> numbers = new List<int> {1,2,2,3};
//var result = Question1.MaxIncreasingSubArrayAsJson(numbers);
//Console.WriteLine(result);
//List<string> words = new List<string> {"miscellaneous", "queue", "sky", "cooperative", "MEEAİÜhaberüĞ"};
//var result = Question2.LongestVowelSubsequenceAsJson(words);
//Console.WriteLine(result);

string xmlData = "<People><Person><Name>Fatma</Name><Age>33</Age><Department>Finance</Department><Salary>6000</Salary><HireDate>2018-11-01</HireDate></Person></People>";
Console.WriteLine(Question3.FilterPeopleFromXml(xmlData));
