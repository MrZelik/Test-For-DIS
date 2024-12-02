using UnityEngine;
using UnityEngine.UI;

public class ButtonFunction : MonoBehaviour, IButton
{
    public enum Functions   //������ ��������� ������� ������
    {
        CapsLock,
        Backspace,
        ChangeLanguage,
        Space,

        NONE
    }

    [SerializeField] private Functions _function = Functions.NONE;  //������������ ������� � ���������� (��� NONE ����� �� ���������)

    public Functions function => _function;

    private Button button;  //��������� ������

    private void Awake()
    {
        button = GetComponent<Button>();    //������������ ���������� ������

        button.onClick.AddListener(SendValue);  //�������� �� ������� ������� ������
    }

    public void SendValue()     //�������� ������� ����������� ���������� (��������� ������� ���������� IButton)
    {
        KeyboardController.instance.AcceptFunction(this);
    }
}
