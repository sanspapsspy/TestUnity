using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public int damageAmount = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // ���������, ��� ������ ������������� ������ � ������
        {
            Enemy enemy = other.GetComponent<Enemy>(); // �������� ��������� Enemy �� ������� �����
            if (enemy != null)
            {
                enemy.TakeDamage(damageAmount); // ������� ���� �����
            }
        }
    }
}