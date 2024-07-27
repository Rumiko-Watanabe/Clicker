using UnityEngine;

public class Money : MonoBehaviour
{
    // Скорость полета, падения и вращения, а также потолок по оси Y
    [SerializeField] private float _flySpeed = 6;
    [SerializeField] private float _fallSpeed = 7;
    [SerializeField] private float _rotationSpeed = 1700;
    [SerializeField] private float _Ymax = 3;

    // Случайное направление и вращение, флаг для проверки, падает ли объект
    private Vector3 randomDirection;
    private float randomRotation;
    private bool isFalling = false;

    // Инициализация случайных значений для направления и вращения
    void Start()
    {
        // Генерация случайного направления в диапазоне по X и Y
        randomDirection = new Vector3(
            Random.Range(-1, 1),
            Random.Range(0.5f, 1),
            0
        ).normalized;

        // Генерация случайного значения для вращения
        randomRotation = Random.Range(
            -_rotationSpeed,
            _rotationSpeed
        );
    }

    // Обновление состояния объектов каждый кадр
    void Update()
    {
        if (!isFalling)
        {
            // Объект движется в случайном направлении с заданной скоростью полета
            transform.position += randomDirection * _flySpeed * Time.deltaTime;

            // Объект вращается с заданной скоростью вращения
            transform.Rotate(
                Vector3.forward,
                randomRotation * Time.deltaTime
            );

            // Проверка, достиг ли объект потолка по оси Y
            if (transform.position.y >= _Ymax)
            {
                isFalling = true; // Если достиг, включаем режим падения
            }
        }
        else
        {
            // Объект падает вниз с заданной скоростью падения
            transform.position += Vector3.down * _fallSpeed * Time.deltaTime;

            // Объект продолжает вращаться с заданной скоростью вращения
            transform.Rotate(
                Vector3.forward,
                randomRotation * Time.deltaTime
            );
        }

        // Если объект падает ниже определенного уровня по оси Y, уничтожаем его
        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }
}