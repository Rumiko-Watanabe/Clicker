using UnityEngine;

public class Money : MonoBehaviour
{
    // �������� ������, ������� � ��������, � ����� ������� �� ��� Y
    [SerializeField] float flySpeed = 6;
    [SerializeField] float fallSpeed = 7;
    [SerializeField] float rotationSpeed = 1700;
    [SerializeField] float Ymax = 3;

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
            -rotationSpeed,
            rotationSpeed
        );
    }

    // ���������� ��������� �������� ������ ����
    void Update()
    {
        if (!isFalling)
        {
            // ������ �������� � ��������� ����������� � �������� ��������� ������
            transform.position += randomDirection * flySpeed * Time.deltaTime;

            // ������ ��������� � �������� ��������� ��������
            transform.Rotate(
                Vector3.forward,
                randomRotation * Time.deltaTime
            );

            // ��������, ������ �� ������ ������� �� ��� Y
            if (transform.position.y >= Ymax)
            {
                isFalling = true; // ���� ������, �������� ����� �������
            }
        }
        else
        {
            // ������ ������ ���� � �������� ��������� �������
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;

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