// See https://aka.ms/new-console-template for more information
using ConsoleApp1;
using System.Collections;

string InnStr = "1+(1+1)*2-1(6/3+(2+-3*(-1,5+-+1)))";
//Ошибка Проверка на скобочки
//Ошибка нагромождение знаков
//Ошибка на наличие букв
//Проверка на положит./отриц. числа(2 знака подряд)
//Проверка на одиночную операцию (*1)

//Для проверки символов
string InnStr2 = "(0123456789^*/,+-=)";
//Для проверки знаков операций
string InnStr3 = "1**2";

string InnStr4 = "^1+(1+1)*2-1(6/3+(2+-3*(-1,5+1)))";
//Для проверки последовательности знаков операций
string InnStr5 = "+++1+^2";


string InnStr6 = "---2*+-1^++2/--2+((---2*+-1^++2/--2)*(---2*+-1^++2/--2)^(---2*+-1^++2/--2))";
string InnStr7 = "---2*+-1^++2/--2*((---2 * +-1 ^ ++2 / --2)^(---2*+-1^++2/--2))";
string InnStr8 = "9^(2-1+(3-2))";
Console.WriteLine(InnStr8.IndexOf('l'));

ArrayList lstArr = new ArrayList() {'1', '-', 'f', '-', '1' };
Console.WriteLine(lstArr.Contains('-'));
Console.WriteLine(lstArr[0]);
Console.WriteLine(lstArr[0].Equals('-') );

CalcExpression exp = new CalcExpression(InnStr8);

Console.WriteLine(exp.ErrSatus);
Console.WriteLine(exp.ErrDescription);

Console.WriteLine(InnStr);
exp.ShowLst();




Console.ReadKey();


