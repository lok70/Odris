using UnityEngine;

public class ShootingPoint : MonoBehaviour
{
    public static Vector2 shootingPointCords = Vector2.zero;

    public Transform characterCollider;
    public float distanceFromCharacter = 1f;

    private Vector3 targetPosition;

    private void Update()
    {


        // �������� ������� ������� ���� � ������� �����������
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = transform.position.z;

        // ������� ������� ��� ������, ��������� �� �������� ���������� �� ���������� ���������
        Vector3 weaponPosition = characterCollider.position + (targetPosition - characterCollider.position).normalized * distanceFromCharacter;
        transform.position = weaponPosition;

        // ������������ ������ � ������� ����
        Vector3 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        shootingPointCords = transform.position;
    }
}

