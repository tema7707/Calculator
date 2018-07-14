/* Автор: Широков Артемий
 * Дата: 14.07.2018
 * Специально для Broccoli Games
 */
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    // Класс, определяющий поедение элементов интерфейса
    public class UIBehavior : MonoBehaviour
    {
        public InputField input; // поле вода
        public Text output; // поле вывода

        // Решение выражения или вывод ошибки, вызывается при нажатии на "=" 
        public void Calculate()
        {
            try
            {
                output.text = Checker.GetResult(input.text);
            } catch
            {
                output.text = "Error!";
            }
        }

        // Добавление символа в строку ввода при нажатии на кнопки
        public void AddSmth(string simb)
        {
            input.text += simb;
        }

        // Удаление всех символов из строки ввода
        public void Clear()
        {
            input.text = "";
        }
    }
}
