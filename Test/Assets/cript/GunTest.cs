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
        if (Input.GetMouseButtonDown(0)) // При нажатии левой кнопки мыши
        {
            isFiring = true;
            Fire();
        }

        if (Input.GetMouseButtonUp(0)) // При отпускании левой кнопки мыши
        {
            isFiring = false;
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation); // Создаем пулю
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletSpeed; // Задаем скорость полета пули

        Destroy(bullet, 2f); // Уничтожаем пулю через 2 секунды

        if (audioSource != null && shootSound != null) // Воспроизводим звук выстрела
        {
            audioSource.PlayOneShot(shootSound);
        }

        if (isFiring) // Если кнопка огня удерживается
        {
            Invoke("Fire", 0.5f); // Выстрел каждые 0.5 секунды
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // Если пуля столкнулась с врагом
        {
            Destroy(gameObject); // Уничтожаем пулю
        }
    }
}
