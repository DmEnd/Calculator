using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Сalculator.Models
{

    internal class MathExpression
    {
        //Поля
        private ArrayList expArrLst = new ArrayList(); //Выражение в виде коллекции
        private string mathExcpressionStr;             //Выражение в текстовом виде
        private bool errStatus;                        //Cтатус наличия ошибки
        private string errDescription;                 //Описание ошибки

        //Свойства
        public bool ErrSatus { get { return errStatus; } }
        public string ErrDescription { get { return errDescription; } }
        public string MathExcpressionStr
        {
            get
            {
                return mathExcpressionStr;
            }
            set
            {
                //Удаление старых результатов
                if (value.Contains("="))
                {
                    value = value.Remove(value.IndexOf('='));
                }

                //Исходное выражение без результата
                mathExcpressionStr=value; 

                //Проверка на недопутимые символы
                if (CheckChars(value))
                {
                    errDescription = "Ошибка. Выражение содержит недопутимые символы.";
                    return;
                }
                //Проверка корректности расстановки скобок
                if (errStatus = CheckBracket(value))
                {
                    errDescription = "Ошибка. Cкобки расставлены НЕ корректно.";
                    return;
                }
                //Проверка расстановки математических знаков
                if (errStatus = CheckOperationsSymbols(value))
                {
                    errDescription = "Ошибка. Математические знаки расставлены НЕ корректно.";
                    return;
                }

                //Приведение последовательности плюсов и минусов к одному знаку
                StrUnifPlusMinus(ref value);

                //Получение первоначального коллекции
                ExpArrLstSet(value);

                //Расчёт
                ExpArrLstSelectExp(expArrLst);


                if (errStatus)
                {
                    return;
                }
                else
                {
                    mathExcpressionStr = mathExcpressionStr+"="+Convert.ToString(expArrLst[0]);
                    errDescription = "Ошибок Не обнаруженно.";
                }
            }
        }


        //Конструктор
        public MathExpression(string innExcpression)
        {
            MathExcpressionStr = innExcpression;
        }


        //Метод приведение последовательности плюсов и минусов к одному знаку в исходной строке
        private void StrUnifPlusMinus(ref string str)
        {

            string tempStr = str;

            List<char> tempLst = new List<char>();

            int i = 0, i_s = 0;

            while (i < str.Length)
            {
                if (str[i] == '+' || str[i] == '-')
                {
                    i_s = (tempLst.Count == 0) ? i : i_s;
                    tempLst.Add(str[i]);

                }
                else if (tempLst.Count > 1)
                {
                    var g = tempLst.GroupBy(p => p);
                    int m = g.Where(d => (d.Key == '-')).ToList().Capacity - 1;
                    m = m < 0 ? 0 : m;
                    str = str.Remove(i_s, tempLst.Count).Insert(i_s, ((m == 0) || !(m % 2.0 > 0)) ? "+" : "-");
                    tempLst.Clear();
                    i = i_s;
                }
                else if (tempLst.Count == 1)
                {
                    tempLst.Clear();
                }
                i++;
            }
        }

        //Метод получение первоначальной коллекции
        private void ExpArrLstSet(string str)
        {
            str = Regex.Replace(str, " ", ""); // Удаление пробелов

            string tempStr = String.Empty;

            for (int i = 0; i < str.Length; i++)
            {
                if (Char.IsNumber(str[i]) || str[i] == (',')) //Занесение цифр и ',' в буферную строку
                {
                    tempStr += Convert.ToString(str[i]);
                    if (i == str.Length - 1)
                    {
                        expArrLst.Add(tempStr);
                    }
                }
                else if (!string.IsNullOrEmpty(tempStr)) //Добавление элемента в коллекцию ИЗ буферной строки 
                {
                    expArrLst.Add(tempStr);
                    tempStr = String.Empty;
                    expArrLst.Add(str[i]);
                }
                else if (string.IsNullOrEmpty(tempStr)) //Добавление элемента в коллекцию БЕЗ буферной строки
                {
                    expArrLst.Add(str[i]);
                }
            }
        }

        //Метод начинающий расчёт. Выделяет часть выражения в скобках. Выражения, представленного в виде коллекции.
        private void ExpArrLstSelectExp(ArrayList expArrLst)
        {
            ArrayList tempExpArrLst = new ArrayList();

            int i = 0, i_s = 0;

            while (i < expArrLst.Count)
            {
                if (expArrLst[i].Equals('('))
                {
                    tempExpArrLst.Clear();
                    i_s = i;
                }
                else if (expArrLst[i].Equals(')'))
                {
                    //Расчёта выражения в скобках
                    ExpArrLstAddPlusMinus(ref tempExpArrLst); 
                    ExpArrLstCalc(ref tempExpArrLst);

                    if (errStatus) { return; }

                    expArrLst[i] = tempExpArrLst[0];
                    expArrLst.RemoveRange(i_s, i - i_s);
                    tempExpArrLst.Clear();
                    i = 0;
                }
                else
                {
                    tempExpArrLst.Add(expArrLst[i]);
                }
                i++;
            }

            //Расчёта оставшегося без скобок выражения
            ExpArrLstAddPlusMinus(ref expArrLst);
            ExpArrLstCalc(ref expArrLst);
        }

        //Метод, добавляющий '+','-' ,идущий после '*','/','^', в следующий элемент коллекции.
        public static void ExpArrLstAddPlusMinus(ref ArrayList tempExpArrLst)
        {
            ArrayList lst1 = new ArrayList()
            {
                '*','/','^'
            };

            ArrayList lst2 = new ArrayList()
            {
                '+','-'
            };

            if (tempExpArrLst[0].Equals('-')) //Проверка первого символа
            {
                tempExpArrLst.RemoveAt(0);
                tempExpArrLst[0] = "-" + tempExpArrLst[0];
            }

            for (int i = 0; i < tempExpArrLst.Count; i++)
            {
                if (lst1.Contains(tempExpArrLst[i]) && lst2.Contains(tempExpArrLst[i + 1]))
                {
                    if (tempExpArrLst[i + 1].Equals('-'))
                    {
                        tempExpArrLst[i + 2] = "-" + tempExpArrLst[i + 2]; ;
                    }
                    tempExpArrLst.RemoveAt(i + 1);
                }
            }
        }

        //Метод, вычисляющий выражение, занесенное в коллекцию. Записывающий результат в исходную коллекцию.
        private void ExpArrLstCalc(ref ArrayList tempExpArrLst)
        {
            while (tempExpArrLst.Contains('^')) //Возведение в степень
            {
                int i = tempExpArrLst.IndexOf('^');
                tempExpArrLst[i + 1] = Math.Pow(Convert.ToDouble(tempExpArrLst[i - 1]), Convert.ToDouble(tempExpArrLst[i + 1]));
                tempExpArrLst.RemoveRange(i - 1, 2);
            }
            while (tempExpArrLst.Contains('*')) //Умножение
            {
                int i = tempExpArrLst.IndexOf('*');
                tempExpArrLst[i + 1] = Convert.ToDouble(tempExpArrLst[i - 1]) * Convert.ToDouble(tempExpArrLst[i + 1]);
                tempExpArrLst.RemoveRange(i - 1, 2);
            }
            while (tempExpArrLst.Contains('/')) //Деление
            {
                int i = tempExpArrLst.IndexOf('/');

                if (Convert.ToDouble(tempExpArrLst[i + 1]) == 0) //Проверка деления на 0
                {
                    errStatus = true;
                    errDescription = "Ошибка. Деление на 0.";
                    return;
                }

                tempExpArrLst[i + 1] = Convert.ToDouble(tempExpArrLst[i - 1]) / Convert.ToDouble(tempExpArrLst[i + 1]);
                tempExpArrLst.RemoveRange(i - 1, 2);
            }
            while (tempExpArrLst.Contains('+') || tempExpArrLst.Contains('-')) //Сложение или Вычитание
            {
                int i_minus = tempExpArrLst.IndexOf('-') == -1 ? tempExpArrLst.Count : tempExpArrLst.IndexOf('-');
                int i_plus = tempExpArrLst.IndexOf('+') == -1 ? tempExpArrLst.Count : tempExpArrLst.IndexOf('+');

                if (i_plus < i_minus) 
                {
                    tempExpArrLst[i_plus + 1] = Convert.ToDouble(tempExpArrLst[i_plus - 1]) + Convert.ToDouble(tempExpArrLst[i_plus + 1]);
                    tempExpArrLst.RemoveRange(i_plus - 1, 2);
                }
                else if (i_minus < i_plus)
                {
                    tempExpArrLst[i_minus + 1] = Convert.ToDouble(tempExpArrLst[i_minus - 1]) - Convert.ToDouble(tempExpArrLst[i_minus + 1]);
                    tempExpArrLst.RemoveRange(i_minus - 1, 2);
                }
            }
        }

        //Метод проверки на недопутимые символы
        static bool CheckChars(string str)
        {   
            str = Regex.Replace(str, "[-+)/(,[0-9\\]^*= ]", "");
            if (str == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Метод проверки корректности расстановки скобок
        static bool CheckBracket(string str)
        {
            //Случай, когда имеются только скобки
            str = Regex.Replace(str, "[)( ]", "");
            if (str == "")
            {
                return true;
            }
            else
            {
                return false;
            }

            //Все остальные случаи
            Stack<char> stack = new Stack<char>();

            Dictionary<char, char> dic = new Dictionary<char, char>
            {
                {'(',')'},
            };

            foreach (char c in str)
            {
                if (dic.ContainsKey(c))
                {
                    stack.Push(dic[c]);
                }
                else if (dic.ContainsValue(c))
                {

                    if (stack.Count == 0 || stack.Pop() != c)
                    {
                        return true;
                    }

                }
            }
            if (stack.Count == 0)
            {

                return false;
            }
            else
            {
                return true;

            }
        }

        //Метод проверки корректности расстановки математических знаков
        //допустимые варианты парных знаков *+, *-, /+, /-, ^+, ^-, ++, +-, -+, --, +++ и т.д. 
        static bool CheckOperationsSymbols(string str)
        {

            List<char> lst1 = new List<char>()
            {
                '*','/','^'
            };

            List<char> lst2 = new List<char>()
            {
                '+','-'
            };

            //Проверка первого символа
            if (lst1.Contains(str[0]))
            {
                return true;
            }

            //Проверка последнего символа
            if (lst1.Contains(str[str.Length - 1]) || lst2.Contains(str[str.Length - 1]))
            {
                return true;
            }

            Boolean flag = false;

            //Проверка последовательности математических знаков
            foreach (char c in str)
            {
                if (lst1.Contains(c) || lst2.Contains(c))
                {
                    if (flag == false)
                    {
                        flag = true;
                    }
                    else if (lst1.Contains(c))
                    {
                        return true;
                    }
                    else if (lst2.Contains(c))
                    {
                        continue;
                    }
                }
                else
                {
                    flag = false;
                }
            }
            return false;
        }
    }
}
