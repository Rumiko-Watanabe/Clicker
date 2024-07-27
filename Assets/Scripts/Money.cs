using UnityEngine;

public class Money : MonoBehaviour
{
    // �������� ������, ������� � ��������, � ����� ������� �� ��� Y
    [SerializeField] private float _flySpeed = 6;
    [SerializeField] private float _fallSpeed = 7;
    [SerializeField] private float _rotationSpeed = 1700;
    [SerializeField] private float _Ymax = 3;

    // ��������� ����������� � ��������, ���� ��� ��������, ������ �� ������
    private Vector3 randomDirection;
    private float randomRotation;
    private bool isFalling = false;

    // ������������� ��������� �������� ��� ����������� � ��������
    void Start()
    {
        // ��������� ���������� ����������� � ��������� �� X � Y
        randomDirection = new Vector3(
            Random.Range(-1, 1),
            Random.Range(0.5f, 1),
            0
        ).normalized;

        // ��������� ���������� �������� ��� ��������
        randomRotation = Random.Range(
            -_rotationSpeed,
            _rotationSpeed
        );
    }

    // ���������� ��������� �������� ������ ����
    void Update()
    {
        if (!isFalling)
        {
            // ������ �������� � ��������� ����������� � �������� ��������� ������
            transform.position += randomDirection * _flySpeed * Time.deltaTime;

            // ������ ��������� � �������� ��������� ��������
            transform.Rotate(
                Vector3.forward,
                randomRotation * Time.deltaTime
            );

            // ��������, ������ �� ������ ������� �� ��� Y
            if (transform.position.y >= _Ymax)
            {
                isFalling = true; // ���� ������, �������� ����� �������
            }
        }
        else
        {
            // ������ ������ ���� � �������� ��������� �������
            transform.position += Vector3.down * _fallSpeed * Time.deltaTime;

            // ������ ���������� ��������� � �������� ��������� ��������
            transform.Rotate(
                Vector3.forward,
                randomRotation * Time.deltaTime
            );
        }

        // ���� ������ ������ ���� ������������� ������ �� ��� Y, ���������� ���
        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }
}