using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LetterChoiser : MonoBehaviour
{
    [SerializeField] private InputActionProperty stickAxis;     //Ось стика, отвечающая за выбор кнопки
    [SerializeField] private InputActionProperty acceptButton;  //Кнопка, отвечающая за нажатие выбранной кнопки

    public KeyboardLine[] keyboardLines;    //Массива линий клавиатуры, содержащий в себе кнопки этой линии

    private int currentLine = 0;    //Индекс текущей строки
    private int currentButtonInLine = 0;    //Индекс текущей кнопки в строке

    public Button currentButton;    //Текущая выбранная кнопка

    private void OnEnable()
    {
        stickAxis.action.started += ChangeLetter;   //Подписка на движение стика
        acceptButton.action.started += ClickOnButton;   //Подписка на нажатие кнопки применения
    }

    private void OnDisable()
    {
        stickAxis.action.started -= ChangeLetter;   //Отписка от движения стика
        acceptButton.action.started -= ClickOnButton;    //Отписка от нажатия кнопки применения
    }

    private void ChangeLetter(InputAction.CallbackContext callback)     //Функция смены выбранной кнопки
    {
        currentButton.targetGraphic.color = Color.white;    //Окрашивание предыдущей кнопки в белый цвет

        if (callback.ReadValue<Vector2>().y < 0)    //Смена кнопки в зависимости от направления движения стика
            currentLine++;  //Смена индекса линии
        else if (callback.ReadValue<Vector2>().y > 0)
            currentLine--;

        if (callback.ReadValue<Vector2>().x > 0)
            currentButtonInLine++;  //Смена индекса кнопки в линии
        else if (callback.ReadValue<Vector2>().x < 0)
            currentButtonInLine--;

        CheckCurrentButtonPos();    //Вызов функции проверки индексов

        currentButton = keyboardLines[currentLine].buttonsInLine[currentButtonInLine];  //Присваивание кнопки из массива линий и кнопок
        currentButton.targetGraphic.color = Color.gray;      //Окрашивание текущей кнопки в серый цвет
    }

    private void CheckCurrentButtonPos()    //Функция проверки крайних положений индексов
    {
        if (currentLine >= keyboardLines.Length)    //Проверка положения индекса линий
            currentLine = keyboardLines.Length - 1;
        else if (currentLine < 0)
            currentLine = 0;

        if (currentButtonInLine >= keyboardLines[currentLine].buttonsInLine.Length)     //Проверка положения индекса кнопки в текущей линии
            currentButtonInLine = keyboardLines[currentLine].buttonsInLine.Length - 1;
        else if (currentButtonInLine < 0)
            currentButtonInLine = 0;
    }

    private void ClickOnButton(InputAction.CallbackContext callback)    //Функция нажатия на кнопку
    {
        if(currentButton.TryGetComponent<IButton>(out IButton iButton))     //Проверка на наличие интерфеса IButton
            iButton.SendValue();    //Отпавка значения или функции кнопки в контроллер клавиатуры

    }
}

[System.Serializable]
public class KeyboardLine   //Класс линий, содержащий в себе массив кнопок данной линии
{
    public Button[] buttonsInLine;
}
