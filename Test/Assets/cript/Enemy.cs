using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float chaseRange = 10f;
    public float moveSpeed = 5f;
    public float runSpeed = 10f;
    public float wallAvoidanceDistance = 1.5f;
    public float changeDirectionTime = 5f;

    public AudioClip alertSound;
    private AudioSource audioSource;

    public int maxHealth = 100;
    private int currentHealth;
    private Vector3 startPosition;
    private bool isDead = false;

    private bool isChasing = false;

    public Enemy(bool isChasing)
    {
        this.isChasing = isChasing;
    }

    private Vector3 randomDirection;
    private float nextDirectionChangeTime;

    void Start()
    {
        audioSource = GetComponent <AudioSource>();
        currentHealth = maxHealth;
        startPosition = transform.position;
        StartCoroutine(RandomMovement());
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= chaseRange)
        {
            isChasing = true;
            StopCoroutine(RandomMovement());

            if (Physics.Raycast(transform.position, (player.position - transform.position).normalized, out RaycastHit hit, chaseRange) && hit.transform == player)
            {
                transform.LookAt(player);
                transform.position += transform.forward * Time.deltaTime * moveSpeed;
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(alertSound);
                }
            }
        }

        if (distanceToPlayer > chaseRange || !IsPlayerVisible())
        {
            isChasing = false;
            if (Time.time >= nextDirectionChangeTime)
            {
                randomDirection = Random.insideUnitSphere;
                randomDirection.y = 0;
                nextDirectionChangeTime = Time.time + changeDirectionTime;
            }

            transform.position += randomDirection.normalized * Time.deltaTime * moveSpeed;
        }
    }

    bool IsPlayerVisible()
    {
        RaycastHit hit;
        if (Physics.Linecast(transform.position, player.position, out hit))
        {
            if (hit.transform == player)
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator RandomMovement()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeDirectionTime);
            randomDirection = Random.insideUnitSphere;
            randomDirection.y = 0;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (!isDead)
        {
            currentHealth -= damageAmount;

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        isDead = true;
        gameObject.SetActive(false); // Скрываем объект противника

        Invoke("Respawn", 5f); // Вызываем метод Respawn через 5 секунд
    }

    void Respawn()
    {
        transform.position = startPosition;
        currentHealth = maxHealth;
        isDead = false;
        gameObject.SetActive(true); // Отображаем объект противника
    }
}