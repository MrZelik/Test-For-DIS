using UnityEngine;
using UnityEngine.UI;

public class ButtonFunction : MonoBehaviour, IButton
{
    public enum Functions   //Список возможных функций кнопки
    {
        CapsLock,
        Backspace,
        ChangeLanguage,
        Space,

        NONE
    }

    [SerializeField] private Functions _function = Functions.NONE;  //Присваивание функции в инспекторе (тип NONE стоит по умолчанию)

    public Functions function => _function;

    private Button button;  //Компонент кнопка

    private void Awake()
    {
        button = GetComponent<Button>();    //Присваивание компонента кнопки

        button.onClick.AddListener(SendValue);  //Подписка на событие нажатия кнопки
    }

    public void SendValue()     //Отправка функции контроллеру клавиатуры (реализует функцию интерфейса IButton)
    {
        KeyboardController.instance.AcceptFunction(this);
    }
}
