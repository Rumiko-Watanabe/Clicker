using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    // ���������� ��� �������� �������� �����, ������, ����� �� ����, ��������� ��������, ���������� ��� ������ � ���������
    [SerializeField] private int _score;
    [SerializeField] private int _lvl = 1;
    [SerializeField] private int _click = 5;
    [SerializeField] private int _upgrade = 20;
    [SerializeField] private int _clickMultiplier = 2;
    [SerializeField] private int _upgMultiplier = 4;

    // ��������� ���� ��� ����������� �������� �����, ������, ����� �� ���� � ��������� ��������
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _lvlText;
    [SerializeField] private Text _clickText;
    [SerializeField] private Text _upgradeText;

    // ������ �������� � ������
    [SerializeField] private GameObject _btnUpgrade;
    [SerializeField] private GameObject _money;

    // �������� ������ ��� �������������� ��������� ������ � ������� ����������
    [SerializeField] private Camera _mainCamera;

    // ��������, ����� �� ������ �������� ���� ��������
    private void UpgradeCheck()
    {
        // ���� ����� ������������ ��� ��������, ��������� ������
        if (_upgrade > _score)
        {
            _btnUpgrade.SetActive(false);
        }
        else
        {
            // ���� ����� ����������, �������� ������
            _btnUpgrade.SetActive(true);
        }
    }

    // �����, ���������� ��� ������� �������� ������
    public void ButtonClick()
    {
        try
        {
            _score = checked(_score + _click);     // ����������� ���� �� �������� ���������� ����� �� ����    
            SpawnMoney();                       // ������� ������
            UpgradeCheck();                     // ���������, ����� �� ������������ ������ ��������
        }
        catch (System.OverflowException e)
        {
            // ���� ��������� ������������, ������� ��������� �� ������ � �������
            Debug.LogError("Overflow occurred while adding score: " + e.Message);
            // ���������� ���� �� ����, ��������� �������� � ���� �� ��������� ��������
            _click = 5;
            _upgrade = 20;
            _score = 0;
        }

    }

    // �����, ���������� ��� ��������
    public void UpgradeClick()
    {
        try
        {
            _score -= _upgrade;           // ��������� ���� �� ��������� ��������
            _click *= _clickMultiplier;   // ����������� ���������� ����� �� ���� � ������ ���������
            _upgrade *= _upgMultiplier;   // ����������� ��������� ���������� �������� � ������ ���������
            _lvl++;                      // ����������� �������
            UpgradeCheck();             // ���������, ����� �� ������������ ������ ��������
        }            
        catch (System.OverflowException e)
        {
            // ���� ��������� ������������, ������� ��������� �� ������ � �������
            Debug.LogError("Overflow occurred while adding score: " + e.Message);
            // ���������� ���� �� ����, ��������� �������� � ���� �� ��������� ��������
            _click = 5;
            _upgrade = 20;
            _score = 0;
        }
    }

    // ����� ��� ������ ����� �� ������ � ������� �������
    public void SpawnMoney()
    {
        // ����������� ���������� ������ � ������� ���������� � ������� ������ �� ���� �������
        Instantiate(
            _money,
            _mainCamera.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward,
            Quaternion.identity
        );
    }

    // ���������� ��������� �������� ������ ����
    void Update()
    {
        _scoreText.text = _score.ToString();                  // ��������� ����� �����
        _lvlText.text = "LV " + _lvl.ToString();              // ��������� ����� ������
        _clickText.text = "+" + _click.ToString();            // ��������� ����� ����� �� ����
        _upgradeText.text = "UPGRADE " + _upgrade.ToString(); // ��������� ����� ��������� ��������
    }
}
