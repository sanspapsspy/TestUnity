using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public int damageAmount = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // Проверяем, что объект соприкоснулся именно с врагом
        {
            Enemy enemy = other.GetComponent<Enemy>(); // Получаем компонент Enemy из объекта врага
            if (enemy != null)
            {
                enemy.TakeDamage(damageAmount); // Наносим урон врагу
            }
        }
    }
}