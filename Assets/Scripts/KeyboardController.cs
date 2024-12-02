using System;
using TMPro;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField; //���� � ������� ��������� �����

    public static KeyboardController instance;   //��������

    public static Action OnKeyboardCapsLock;  //���������� ��� ��������� ��� ���������� ��������
    public static Action OnChangeLanguage;  //���������� ��� ����� �����

    public bool isCapsLock = false; //������� �� �������

    public bool isEnglish = false;  //�������� �� ���������� ���������

    private string messageText;

    private void Awake()
    {
        if (!instance)   //�������� ���������
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PrintLetter(ButtonValue buttonValue)    //�������, ������� �������� ����� � InputField
    {
        if (isEnglish)  //������� �������� ����� � ����������� �� �����
        {
            if (isCapsLock)    //�������� �������� ����� � ����������� �� ��������� ��������
                inputField.text += buttonValue.shiftValue.ToString();   //�������� ��������� �����
            else
                inputField.text += buttonValue.value.ToString();    //�������� �����
        }
        else
        {
            if (isCapsLock)    //�������� �������� ����� � ����������� �� ��������� ��������
                inputField.text += buttonValue.altShiftValue.ToString();   //�������� ��������� �����
            else
                inputField.text += buttonValue.altValue.ToString();    //�������� �����
        }
    }

    public void AcceptFunction(ButtonFunction buttonFunction)   //��������� �������, ������������ �������
    {
        switch (buttonFunction.function)    //��������� ����� ������� ���������� � ������
        {
            case ButtonFunction.Functions.CapsLock:     //�������� ������� ������������ ��������
                {
                    if (isCapsLock)     //����������� �������� ��������
                        isCapsLock = false;
                    else
                        isCapsLock = true;

                    OnKeyboardCapsLock?.Invoke();     //�������� ������� OnKeyboardCapsLock
                    break;
                }

            case ButtonFunction.Functions.Backspace:     //�������� ������� �������� ��������� �����
                {
                    inputField.text = inputField.text.Substring(0, inputField.text.Length-1);   //������� ��������� ����� � ������
                    break;
                }

            case ButtonFunction.Functions.ChangeLanguage:     //�������� ������� ����� �����
                {
                    if (isEnglish)     //����������� �������� �����
                        isEnglish = false;
                    else
                        isEnglish = true;

                    OnChangeLanguage?.Invoke();     //�������� ������� OnChangeLanguage
                    break;
                }

            case ButtonFunction.Functions.Space:     //�������� ������� �������
                {
                    inputField.text += " ";     //������ ������ � ������ InputField
                    break;
                }

            case ButtonFunction.Functions.NONE:
                {
                    Debug.LogErrorFormat("������ {0} �� ���������� �������", buttonFunction.gameObject.name);   //�������� � ������� � ���, ��� ������� �� ������ �������
                    break;
                }

            default:
                {
                    Debug.LogErrorFormat("����������� ������� ������!\n��� ������: {0}", buttonFunction.gameObject.name);   //�������� � ������� � ���, ��� � ������� �� ��������� �������
                    break;
                }
        }
    }
}
