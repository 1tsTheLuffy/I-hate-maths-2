using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFire : MonoBehaviour
{
    [Header("Number of bullets")]
    [SerializeField] int size;

    [Header("Direction")]
    [SerializeField] float angle;
    [SerializeField] float startAngle;
    [SerializeField] float endAngle;
    [SerializeField] float angleStep;

    [Header("Time")]
    [SerializeField] float timer;
    [SerializeField] float timeBtwSpawn;

    ObjectPooler pool;

    private void Start()
    {
        InvokeRepeating("Shoot", 2, 2);

        pool = GetComponent<ObjectPooler>();
    }

    private void Shoot()
    {
        angleStep = ((endAngle - startAngle) / size) * .5f;
        angle = startAngle;

        for (int i = 0; i < size; i++)
        {
            float x = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float y = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);
            Vector3 dir = new Vector3(x, y, 0);
            Vector2 bulletDir = (dir - transform.position).normalized;

            GameObject bul = pool.GetFromPool();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<CircleEnemyBullet>().SetMoveDirection(bulletDir);

            angle += angleStep;
        }
    }
}
