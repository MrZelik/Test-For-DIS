using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonValue : MonoBehaviour, IButton
{
    [SerializeField] private string _value;     //значение буквы в нижнем регистре(английский)
    [SerializeField] private string _shiftValue;    //значение буквы в верхнем регистре (английский)
    [SerializeField] private string _altValue;  //значение буквы в нижнем регистре на другом языке (русский)
    [SerializeField] private string _altShiftValue; //значение буквы в верхнем регистре на другом языке (русский)

    public string value => _value;
    public string shiftValue => _shiftValue;
    public string altValue => _altValue;
    public string altShiftValue => _altShiftValue;

    private Button button;  //Компонент кнопка
    private TextMeshProUGUI buttonText;     //Компонент текста кнопки

    private void Awake()
    {
        button = GetComponent<Button>();    //Присваивание компонента кнопки
        buttonText = gameObject.GetComponentInChildren<TextMeshProUGUI>();  //Присваивание компонента текста из дочернего объекта

        button.onClick.AddListener(SendValue);  //Подписка на событие нажатия кнопки
    }

    private void Start()
    {
        ChangeButtonTextValue();    //Изначальное присвоение тексту кнопки конкретной буквы
    }

    private void OnEnable()
    {
        KeyboardController.OnKeyboardCapsLock += ChangeButtonTextValue;     //Подписка на событие смены регистра
        KeyboardController.OnChangeLanguage += ChangeButtonTextValue;   //Подписка на событие смены языка
    }

    private void OnDisable()
    {
        KeyboardController.OnKeyboardCapsLock -= ChangeButtonTextValue;     //Отписка от события смены регистра
        KeyboardController.OnChangeLanguage -= ChangeButtonTextValue;   //Отписка от события смены языка
    }

    public void SendValue()     //Отправка буквы контроллеру клавиатуры (реализует функцию интерфейса IButton)
    {
        KeyboardController.instance.PrintLetter(this);
    }

    private void ChangeButtonTextValue()    //Смена значения буквы в тексте кнопки
    {
        if (KeyboardController.instance.isEnglish)  //Включен ли английский язык
        {
            if (KeyboardController.instance.isCapsLock)     //Включен ли капслок
                buttonText.text = shiftValue;
            else
                buttonText.text = value;
        }
        else
        {
            if (KeyboardController.instance.isCapsLock)     //Включен ли капслок
                buttonText.text = altShiftValue;
            else
                buttonText.text = altValue;
        }
    }
}
