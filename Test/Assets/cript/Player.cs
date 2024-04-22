using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f; // �������� ������������
    public float sensitivity = 10f; // ���������������� ����
    public float jumpForce = 10f; // ���� ������
    public float gravity = 100f; // ����������

    private Rigidbody rb;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private float verticalRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; // ���������� ������� � ������ ������
        // ������������� ��������� ���� �������� ������
        Camera.main.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    void Update()
    {
        // ��������� ����� �� ���������� ��� �����������
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        moveInput = new Vector3(moveHorizontal, 0f, moveVertical);
        moveVelocity = moveInput.normalized * speed;

        // ��������� ����� �� ���� ��� �������� ������
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        transform.Rotate(Vector3.up * mouseX);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        // ���������, ��� �� ����� ������
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // ���������� �������� � Rigidbody ��� ��������
        rb.velocity = transform.TransformDirection(moveVelocity);

        // ���������� ����������
        rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
    }

    bool IsGrounded()
    {
        // ���������, ��������� �� �������� �� �����
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f))
        {
            return true;
        }
        return false;
    }

    void Jump()
    {
        // ��������� ���� ������ � Rigidbody
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
