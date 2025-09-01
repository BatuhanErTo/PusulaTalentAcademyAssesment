// See https://aka.ms/new-console-template for more information
List<int> numbers = new List<int> {1,2,2,3};
var result = Question1.MaxIncreasingSubArrayAsJson(numbers);
Console.WriteLine(result);

/*

Giriş 1: [1,2,3,1,2]
Sonuç 1: [1,2,3]
Giriş 2 : [2,5,4,3,2,1]
Sonuç 2 : [2,5]
Giriş 3 : [1,2,2,3]
Sonuç 3 : [2,3]
Giriş 4 : [1,3,5,4,7,8,2,4,5]
Sonuç 4 : [4,7,8]
Giriş 5 : []
Sonuç 5 : []
*/