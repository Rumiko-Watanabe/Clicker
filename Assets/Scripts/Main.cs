using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    // Переменные для хранения текущего счета, уровня, очков за клик, стоимости апгрейда, множителей для кликов и апгрейдов
    [SerializeField] int score;
    [SerializeField] int lvl = 1;
    [SerializeField] int click = 5;
    [SerializeField] int upgrade = 20;
    [SerializeField] int clickMultiplier = 2;
    [SerializeField] int upgMultiplier = 4;

    // Текстовые поля для отображения значений счета, уровня, очков за клик и стоимости апгрейда
    [SerializeField] Text scoreText;
    [SerializeField] Text lvlText;
    [SerializeField] Text clickText;
    [SerializeField] Text upgradeText;

    // Кнопка апгрейда и деньги
    [SerializeField] GameObject btnUpgrade;
    [SerializeField] GameObject money;

    // Основная камера для преобразования координат экрана в мировые координаты
    [SerializeField] Camera mainCamera;

    // Проверка, может ли кнопка апгрейда быть активной
    private void UpgradeCheck()
    {
        // Если очков недостаточно для апгрейда, отключаем кнопку
        if (upgrade > score)
        {
            btnUpgrade.SetActive(false);
        }
        else
        {
            // Если очков достаточно, включаем кнопку
            btnUpgrade.SetActive(true);
        }
    }

    // Метод, вызываемый при нажатии основной кнопки
    public void ButtonClick()
    {
        try
        {
            score = checked(score + click);     // Увеличиваем счет на заданное количество очков за клик    
            SpawnMoney();                       // Спауним деньги
            UpgradeCheck();                     // Проверяем, можно ли активировать кнопку апгрейда
        }
        catch (System.OverflowException e)
        {
            // Если произошло переполнение, выводим сообщение об ошибке в консоль
            Debug.LogError("Overflow occurred while adding score: " + e.Message);
            // Сбрасываем очки за клик, стоимость апгрейда и счет до начальных значений
            click = 5;
            upgrade = 20;
            score = 0;
        }

    }

    // Метод, вызываемый при апгрейде
    public void UpgradeClick()
    {
        try
        {
            score -= upgrade;           // Уменьшаем счет на стоимость апгрейда
            click *= clickMultiplier;   // Увеличиваем количество очков за клик с учетом множителя
            upgrade *= upgMultiplier;   // Увеличиваем стоимость следующего апгрейда с учетом множителя
            lvl++;                      // Увеличиваем уровень
            UpgradeCheck();             // Проверяем, можно ли активировать кнопку апгрейда
        }            
        catch (System.OverflowException e)
        {
            // Если произошло переполнение, выводим сообщение об ошибке в консоль
            Debug.LogError("Overflow occurred while adding score: " + e.Message);
            // Сбрасываем очки за клик, стоимость апгрейда и счет до начальных значений
            click = 5;
            upgrade = 20;
            score = 0;
        }
    }

    // Метод для спауна денег на экране в позиции курсора
    public void SpawnMoney()
    {
        // Преобразуем координаты экрана в мировые координаты и создаем деньги на этой позиции
        Instantiate(
            money,
            mainCamera.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward,
            Quaternion.identity
        );
    }

    // Обновление состояния объектов каждый кадр
    void Update()
    {
        scoreText.text = score.ToString();                  // Обновляем текст счета
        lvlText.text = "LV " + lvl.ToString();              // Обновляем текст уровня
        clickText.text = "+" + click.ToString();            // Обновляем текст очков за клик
        upgradeText.text = "UPGRADE " + upgrade.ToString(); // Обновляем текст стоимости апгрейда
    }
}
