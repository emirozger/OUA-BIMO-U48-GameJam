using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    public Transform target; // Takip edilecek hedef

    public float speed = 5f; // Enemy hızı
    public float distance = 1f; // Hedefe olan mesafe
    public float rotationSpeed = 5f; // Döndürme hızı
    public float slowingDistance = 3f; // Hedefe yaklaşırken yavaşlamak için mesafe

    void LateUpdate()
    {
        // Hedefin konumuna doğru yönel
        Vector3 direction = (target.position - transform.position).normalized;
        direction = Quaternion.AngleAxis(-180, Vector3.up) * direction;

        if (direction != Vector3.zero)
        { 
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }

        // Hedefe doğru hareket et
        float currentDistance = Vector3.Distance(transform.position, target.position);
        if (currentDistance > distance)
        {
            float targetSpeed = speed;
            if (currentDistance <= slowingDistance)
            {
                // Hedefe yaklaşırken hızını yavaşlat
                targetSpeed *= currentDistance / slowingDistance;
            }

            // Karakterin hareket yönü, karakterin forward yönüne göre belirlenir.
            Vector3 moveDirection = (target.position - transform.position).normalized;

            transform.position += moveDirection * targetSpeed * Time.deltaTime;
        }
    }
}
