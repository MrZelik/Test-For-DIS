using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LetterChoiser : MonoBehaviour
{
    [SerializeField] private InputActionProperty stickAxis;     //��� �����, ���������� �� ����� ������
    [SerializeField] private InputActionProperty acceptButton;  //������, ���������� �� ������� ��������� ������

    public KeyboardLine[] keyboardLines;    //������� ����� ����������, ���������� � ���� ������ ���� �����

    private int currentLine = 0;    //������ ������� ������
    private int currentButtonInLine = 0;    //������ ������� ������ � ������

    public Button currentButton;    //������� ��������� ������

    private void OnEnable()
    {
        stickAxis.action.started += ChangeLetter;   //�������� �� �������� �����
        acceptButton.action.started += ClickOnButton;   //�������� �� ������� ������ ����������
    }

    private void OnDisable()
    {
        stickAxis.action.started -= ChangeLetter;   //������� �� �������� �����
        acceptButton.action.started -= ClickOnButton;    //������� �� ������� ������ ����������
    }

    private void ChangeLetter(InputAction.CallbackContext callback)     //������� ����� ��������� ������
    {
        currentButton.targetGraphic.color = Color.white;    //����������� ���������� ������ � ����� ����

        if (callback.ReadValue<Vector2>().y < 0)    //����� ������ � ����������� �� ����������� �������� �����
            currentLine++;  //����� ������� �����
        else if (callback.ReadValue<Vector2>().y > 0)
            currentLine--;

        if (callback.ReadValue<Vector2>().x > 0)
            currentButtonInLine++;  //����� ������� ������ � �����
        else if (callback.ReadValue<Vector2>().x < 0)
            currentButtonInLine--;

        CheckCurrentButtonPos();    //����� ������� �������� ��������

        currentButton = keyboardLines[currentLine].buttonsInLine[currentButtonInLine];  //������������ ������ �� ������� ����� � ������
        currentButton.targetGraphic.color = Color.gray;      //����������� ������� ������ � ����� ����
    }

    private void CheckCurrentButtonPos()    //������� �������� ������� ��������� ��������
    {
        if (currentLine >= keyboardLines.Length)    //�������� ��������� ������� �����
            currentLine = keyboardLines.Length - 1;
        else if (currentLine < 0)
            currentLine = 0;

        if (currentButtonInLine >= keyboardLines[currentLine].buttonsInLine.Length)     //�������� ��������� ������� ������ � ������� �����
            currentButtonInLine = keyboardLines[currentLine].buttonsInLine.Length - 1;
        else if (currentButtonInLine < 0)
            currentButtonInLine = 0;
    }

    private void ClickOnButton(InputAction.CallbackContext callback)    //������� ������� �� ������
    {
        if(currentButton.TryGetComponent<IButton>(out IButton iButton))     //�������� �� ������� ��������� IButton
            iButton.SendValue();    //������� �������� ��� ������� ������ � ���������� ����������

    }
}

[System.Serializable]
public class KeyboardLine   //����� �����, ���������� � ���� ������ ������ ������ �����
{
    public Button[] buttonsInLine;
}
