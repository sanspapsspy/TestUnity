using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTest : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public int damage = 10;
    public AudioClip shootSound;
    public AudioSource audioSource;
    private bool isFiring = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ��� ������� ����� ������ ����
        {
            isFiring = true;
            Fire();
        }

        if (Input.GetMouseButtonUp(0)) // ��� ���������� ����� ������ ����
        {
            isFiring = false;
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation); // ������� ����
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletSpeed; // ������ �������� ������ ����

        Destroy(bullet, 2f); // ���������� ���� ����� 2 �������

        if (audioSource != null && shootSound != null) // ������������� ���� ��������
        {
            audioSource.PlayOneShot(shootSound);
        }

        if (isFiring) // ���� ������ ���� ������������
        {
            Invoke("Fire", 0.5f); // ������� ������ 0.5 �������
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // ���� ���� ����������� � ������
        {
            Destroy(gameObject); // ���������� ����
        }
    }
}
