using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonValue : MonoBehaviour, IButton
{
    [SerializeField] private string _value;     //�������� ����� � ������ ��������(����������)
    [SerializeField] private string _shiftValue;    //�������� ����� � ������� �������� (����������)
    [SerializeField] private string _altValue;  //�������� ����� � ������ �������� �� ������ ����� (�������)
    [SerializeField] private string _altShiftValue; //�������� ����� � ������� �������� �� ������ ����� (�������)

    public string value => _value;
    public string shiftValue => _shiftValue;
    public string altValue => _altValue;
    public string altShiftValue => _altShiftValue;

    private Button button;  //��������� ������
    private TextMeshProUGUI buttonText;     //��������� ������ ������

    private void Awake()
    {
        button = GetComponent<Button>();    //������������ ���������� ������
        buttonText = gameObject.GetComponentInChildren<TextMeshProUGUI>();  //������������ ���������� ������ �� ��������� �������

        button.onClick.AddListener(SendValue);  //�������� �� ������� ������� ������
    }

    private void Start()
    {
        ChangeButtonTextValue();    //����������� ���������� ������ ������ ���������� �����
    }

    private void OnEnable()
    {
        KeyboardController.OnKeyboardCapsLock += ChangeButtonTextValue;     //�������� �� ������� ����� ��������
        KeyboardController.OnChangeLanguage += ChangeButtonTextValue;   //�������� �� ������� ����� �����
    }

    private void OnDisable()
    {
        KeyboardController.OnKeyboardCapsLock -= ChangeButtonTextValue;     //������� �� ������� ����� ��������
        KeyboardController.OnChangeLanguage -= ChangeButtonTextValue;   //������� �� ������� ����� �����
    }

    public void SendValue()     //�������� ����� ����������� ���������� (��������� ������� ���������� IButton)
    {
        KeyboardController.instance.PrintLetter(this);
    }

    private void ChangeButtonTextValue()    //����� �������� ����� � ������ ������
    {
        if (KeyboardController.instance.isEnglish)  //������� �� ���������� ����
        {
            if (KeyboardController.instance.isCapsLock)     //������� �� �������
                buttonText.text = shiftValue;
            else
                buttonText.text = value;
        }
        else
        {
            if (KeyboardController.instance.isCapsLock)     //������� �� �������
                buttonText.text = altShiftValue;
            else
                buttonText.text = altValue;
        }
    }
}
