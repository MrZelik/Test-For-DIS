using System;
using TMPro;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField; //Поле в которое выводится текст

    public static KeyboardController instance;   //Синглтон

    public static Action OnKeyboardCapsLock;  //Вызывается при включении или выключении капслока
    public static Action OnChangeLanguage;  //Вызывается при смене языка

    public bool isCapsLock = false; //Включен ли капслок

    public bool isEnglish = false;  //Включена ли английская раскладка

    private string messageText;

    private void Awake()
    {
        if (!instance)   //Проверка синглтона
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PrintLetter(ButtonValue buttonValue)    //Функция, которая печатает букву в InputField
    {
        if (isEnglish)  //Выбиает значение буквы в зависимости от языка
        {
            if (isCapsLock)    //Выбирает значение буквы в зависимости от состояния капслока
                inputField.text += buttonValue.shiftValue.ToString();   //Печатает заглавную букву
            else
                inputField.text += buttonValue.value.ToString();    //Печатает букву
        }
        else
        {
            if (isCapsLock)    //Выбирает значение буквы в зависимости от состояния капслока
                inputField.text += buttonValue.altShiftValue.ToString();   //Печатает заглавную букву
            else
                inputField.text += buttonValue.altValue.ToString();    //Печатает букву
        }
    }

    public void AcceptFunction(ButtonFunction buttonFunction)   //Принимает функцию, отправленную кнопкой
    {
        switch (buttonFunction.function)    //Проверяет какая функция содержится в кнопке
        {
            case ButtonFunction.Functions.CapsLock:     //Выбирает функцию переключения капслока
                {
                    if (isCapsLock)     //Переключает значение капслока
                        isCapsLock = false;
                    else
                        isCapsLock = true;

                    OnKeyboardCapsLock?.Invoke();     //Вызывает событие OnKeyboardCapsLock
                    break;
                }

            case ButtonFunction.Functions.Backspace:     //Выбирает функцию удаления последней буквы
                {
                    inputField.text = inputField.text.Substring(0, inputField.text.Length-1);   //Удаляет последную букву в строке
                    break;
                }

            case ButtonFunction.Functions.ChangeLanguage:     //Выбирает функцию смены языка
                {
                    if (isEnglish)     //Переключает значение языка
                        isEnglish = false;
                    else
                        isEnglish = true;

                    OnChangeLanguage?.Invoke();     //Вызывает событие OnChangeLanguage
                    break;
                }

            case ButtonFunction.Functions.Space:     //Выбирает функцию пробела
                {
                    inputField.text += " ";     //Ставит пробел в строку InputField
                    break;
                }

            case ButtonFunction.Functions.NONE:
                {
                    Debug.LogErrorFormat("Кнопке {0} не присвоенна функция", buttonFunction.gameObject.name);   //Сообщает в консоль о том, что клавише не задана функция
                    break;
                }

            default:
                {
                    Debug.LogErrorFormat("Неизвестная функция кнопки!\nИмя кнопки: {0}", buttonFunction.gameObject.name);   //Сообщает в консоль о том, что у клавиши не известная функция
                    break;
                }
        }
    }
}
