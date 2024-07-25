using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    // ���������� ��� �������� �������� �����, ������, ����� �� ����, ��������� ��������, ���������� ��� ������ � ���������
    [SerializeField] int score;
    [SerializeField] int lvl = 1;
    [SerializeField] int click = 5;
    [SerializeField] int upgrade = 20;
    [SerializeField] int clickMultiplier = 2;
    [SerializeField] int upgMultiplier = 4;

    // ��������� ���� ��� ����������� �������� �����, ������, ����� �� ���� � ��������� ��������
    [SerializeField] Text scoreText;
    [SerializeField] Text lvlText;
    [SerializeField] Text clickText;
    [SerializeField] Text upgradeText;

    // ������ �������� � ������
    [SerializeField] GameObject btnUpgrade;
    [SerializeField] GameObject money;

    // �������� ������ ��� �������������� ��������� ������ � ������� ����������
    [SerializeField] Camera mainCamera;

    // ��������, ����� �� ������ �������� ���� ��������
    private void UpgradeCheck()
    {
        // ���� ����� ������������ ��� ��������, ��������� ������
        if (upgrade > score)
        {
            btnUpgrade.SetActive(false);
        }
        else
        {
            // ���� ����� ����������, �������� ������
            btnUpgrade.SetActive(true);
        }
    }

    // �����, ���������� ��� ������� �������� ������
    public void ButtonClick()
    {
        try
        {
            score = checked(score + click);     // ����������� ���� �� �������� ���������� ����� �� ����    
            SpawnMoney();                       // ������� ������
            UpgradeCheck();                     // ���������, ����� �� ������������ ������ ��������
        }
        catch (System.OverflowException e)
        {
            // ���� ��������� ������������, ������� ��������� �� ������ � �������
            Debug.LogError("Overflow occurred while adding score: " + e.Message);
            // ���������� ���� �� ����, ��������� �������� � ���� �� ��������� ��������
            click = 5;
            upgrade = 20;
            score = 0;
        }

    }

    // �����, ���������� ��� ��������
    public void UpgradeClick()
    {
        try
        {
            score -= upgrade;           // ��������� ���� �� ��������� ��������
            click *= clickMultiplier;   // ����������� ���������� ����� �� ���� � ������ ���������
            upgrade *= upgMultiplier;   // ����������� ��������� ���������� �������� � ������ ���������
            lvl++;                      // ����������� �������
            UpgradeCheck();             // ���������, ����� �� ������������ ������ ��������
        }            
        catch (System.OverflowException e)
        {
            // ���� ��������� ������������, ������� ��������� �� ������ � �������
            Debug.LogError("Overflow occurred while adding score: " + e.Message);
            // ���������� ���� �� ����, ��������� �������� � ���� �� ��������� ��������
            click = 5;
            upgrade = 20;
            score = 0;
        }
    }

    // ����� ��� ������ ����� �� ������ � ������� �������
    public void SpawnMoney()
    {
        // ����������� ���������� ������ � ������� ���������� � ������� ������ �� ���� �������
        Instantiate(
            money,
            mainCamera.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward,
            Quaternion.identity
        );
    }

    // ���������� ��������� �������� ������ ����
    void Update()
    {
        scoreText.text = score.ToString();                  // ��������� ����� �����
        lvlText.text = "LV " + lvl.ToString();              // ��������� ����� ������
        clickText.text = "+" + click.ToString();            // ��������� ����� ����� �� ����
        upgradeText.text = "UPGRADE " + upgrade.ToString(); // ��������� ����� ��������� ��������
    }
}
