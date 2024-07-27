using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    // Переменные для хранения текущего счета, уровня, очков за клик, стоимости апгрейда, множителей для кликов и апгрейдов
    [SerializeField] private int _score;
    [SerializeField] private int _lvl = 1;
    [SerializeField] private int _click = 5;
    [SerializeField] private int _upgrade = 20;
    [SerializeField] private int _clickMultiplier = 2;
    [SerializeField] private int _upgMultiplier = 4;

    // Текстовые поля для отображения значений счета, уровня, очков за клик и стоимости апгрейда
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _lvlText;
    [SerializeField] private Text _clickText;
    [SerializeField] private Text _upgradeText;

    // Кнопка апгрейда и деньги
    [SerializeField] private GameObject _btnUpgrade;
    [SerializeField] private GameObject _money;

    // Основная камера для преобразования координат экрана в мировые координаты
    [SerializeField] private Camera _mainCamera;

    // Проверка, может ли кнопка апгрейда быть активной
    private void UpgradeCheck()
    {
        // Если очков недостаточно для апгрейда, отключаем кнопку
        if (_upgrade > _score)
        {
            _btnUpgrade.SetActive(false);
        }
        else
        {
            // Если очков достаточно, включаем кнопку
            _btnUpgrade.SetActive(true);
        }
    }

    // Метод, вызываемый при нажатии основной кнопки
    public void ButtonClick()
    {
        try
        {
            _score = checked(_score + _click);     // Увеличиваем счет на заданное количество очков за клик    
            SpawnMoney();                       // Спауним деньги
            UpgradeCheck();                     // Проверяем, можно ли активировать кнопку апгрейда
        }
        catch (System.OverflowException e)
        {
            // Если произошло переполнение, выводим сообщение об ошибке в консоль
            Debug.LogError("Overflow occurred while adding score: " + e.Message);
            // Сбрасываем очки за клик, стоимость апгрейда и счет до начальных значений
            _click = 5;
            _upgrade = 20;
            _score = 0;
        }

    }

    // Метод, вызываемый при апгрейде
    public void UpgradeClick()
    {
        try
        {
            _score -= _upgrade;           // Уменьшаем счет на стоимость апгрейда
            _click *= _clickMultiplier;   // Увеличиваем количество очков за клик с учетом множителя
            _upgrade *= _upgMultiplier;   // Увеличиваем стоимость следующего апгрейда с учетом множителя
            _lvl++;                      // Увеличиваем уровень
            UpgradeCheck();             // Проверяем, можно ли активировать кнопку апгрейда
        }            
        catch (System.OverflowException e)
        {
            // Если произошло переполнение, выводим сообщение об ошибке в консоль
            Debug.LogError("Overflow occurred while adding score: " + e.Message);
            // Сбрасываем очки за клик, стоимость апгрейда и счет до начальных значений
            _click = 5;
            _upgrade = 20;
            _score = 0;
        }
    }

    // Метод для спауна денег на экране в позиции курсора
    public void SpawnMoney()
    {
        // Преобразуем координаты экрана в мировые координаты и создаем деньги на этой позиции
        Instantiate(
            _money,
            _mainCamera.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward,
            Quaternion.identity
        );
    }

    // Обновление состояния объектов каждый кадр
    void Update()
    {
        _scoreText.text = _score.ToString();                  // Обновляем текст счета
        _lvlText.text = "LV " + _lvl.ToString();              // Обновляем текст уровня
        _clickText.text = "+" + _click.ToString();            // Обновляем текст очков за клик
        _upgradeText.text = "UPGRADE " + _upgrade.ToString(); // Обновляем текст стоимости апгрейда
    }
}
