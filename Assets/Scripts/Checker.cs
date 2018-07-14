using UnityEngine;

namespace Test
{
    //Класс проверяющий корректность данного выражения
    static public class Checker
    {
        //Метод, возвращающий результат проаерки (ошибку или решение)
        static public string GetResult(string s)
        {
            int errnum; // номер символа с ошибкой
            // Проверяем скобки
            if (!Brackets(s, out errnum))
            {
                return string.Format("Error with brackets in {0} symbol!", errnum + 1);
            }
            // Проеряем знаки
            if (!Signs(ref s, out errnum))
            {
                return string.Format("Error with signs in {0} symbol!", errnum + 1);
            }
            Debug.Log(s);
            return Calculator.Calculate(s);
        }

        //Метод, проверяющий корректность скобок
        static bool Brackets(string s, out int num)
        {
            int count = 0; // счетчик скобок
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(') count++;
                if (s[i] == ')') count--;
                if (count < 0) 
                {
                    // закрывающая скобка лишняя
                    num = i;
                    return false;
                }
            }
            num = s.Length;
            return count == 0;
        }

        //Метод, проверяющий корректность знаков
        static bool Signs(ref string s, out int num)
        {
            bool NextIsSign = false; // следующий символ должен быть знаком
            bool NewNumber = true; // начилось ли новое число
            bool MustInsertBr = false; // должны ли поставить скобку после числа
            bool AfterBr = true; // символ идет после открывающейся скобки
            num = 0; // номер символа с ошибкой

            for (int i = 0; i < s.Length; i++)
            {
                num = i;
                // Начилось ли число
                if (char.IsDigit(s[i]) && NewNumber)
                {
                    if (NextIsSign) return false;
                    NextIsSign = true;
                    NewNumber = false;
                    AfterBr = false;
                }
                else if (s[i] == ' ') NewNumber = true;
                // Если встретился знак
                else if (s[i] == '*' || s[i] == '/' || s[i] == '^' || s[i] == '+' || s[i] == '-')
                {
                    if (MustInsertBr)
                    {
                        // вставляем закрывающуюся скобку, если надо
                        s = s.Insert(i, ")");
                        i++;
                        MustInsertBr = false;
                    }

                    if (!NextIsSign)
                        if (AfterBr && (s[i] == '-' || s[i] == '+'))
                        {
                            // Преобразуем выражение типа -x в (0-x) 
                            MustInsertBr = true;
                            AfterBr = false;
                            s = s.Insert(i, "(0");
                            i += 2;
                        }
                        else return false;
                    NewNumber = true;
                    NextIsSign = false;
                } else if (s[i] == '(') AfterBr = true;
                // Недопустимый символ
                else if (s[i] != '.' && !char.IsDigit(s[i]) && s[i] != ')') return false;
            }

            // Вставляем закрывающуюся скобку в конец строки, если нужно
            if (MustInsertBr) s += ')';
            // Если последний симол знак -> false
            return NextIsSign;
        }
    }
}
