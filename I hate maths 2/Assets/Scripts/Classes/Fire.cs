using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Fire
{
    [Header("Number of bullets")]
    public int numberOfBullet = 1;

    [Header("Directions")]
    public float angle;
    public float startAngle;
    public float endAngle;
    public float angleStep;

    [Header("Timers")]
    public float startTimeForShoot;
    public float timeBtwShoot;

    [Header("Shoot Point")]
    public Transform shootPoint;

    public ObjectPooler pool;
}
