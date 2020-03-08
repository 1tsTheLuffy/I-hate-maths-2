using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Circle))]
public class Circle : MonoBehaviour
{
    [SerializeField]private int eventType = 0;
    private int randomMovepoint = 0;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float distance = 1f;
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private float timer = 1f;
    [SerializeField] private float timeToMoveToNextEvent = 1f;
    [SerializeField] private float startTime = 1f;
    [SerializeField] private float timeBtwShoot = 1f;
    [SerializeField] private float health = 10f;

    [SerializeField] private Vector2 target;

    [SerializeField] private Transform[] movepoints;
    [SerializeField] private Transform Player;

    [SerializeField] Fire fire;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sr;
    CameraShake shake;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();

        randomMovepoint = Random.Range(0, movepoints.Length);
        eventType = 1;


        timer = timeToMoveToNextEvent;
    }

    private void Update()
    {

        #region AI

        if (Player == null)
            return;

        transform.Rotate(0f, 0f, 7f);

        distance = Vector2.Distance(transform.position, movepoints[randomMovepoint].position);

        if (eventType == 1)
        {
            if(distance > minDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, movepoints[randomMovepoint].position, speed * Time.deltaTime);
            }
            else if (distance < minDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, movepoints[randomMovepoint].position, speed * Time.deltaTime);

                if(startTime <= 0)
                {
                    Shoot();
                    startTime = timeBtwShoot;
                }else
                {
                    startTime -= Time.deltaTime;
                }

                if(Vector2.Distance(transform.position, movepoints[randomMovepoint].position) < .2f)
                {
                    randomMovepoint = Random.Range(0, movepoints.Length);
                }

                if (timer <= 0)
                {
                    eventType = Random.Range(1, 3);
                    if (eventType == 2)
                    {
                        target = new Vector2(Player.position.x, Player.position.y);
                    }
                    timer = timeToMoveToNextEvent;
                }
                else
                {
                    timer -= Time.deltaTime;
                }
            }
        }else if(eventType == 2)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * 2 * Time.deltaTime);
        }

        if(Vector2.Distance(transform.position, target) < .2f)
        {
            eventType = Random.Range(1, 3);
            return;
        }

        #endregion

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Shoot()
    {
        fire.angleStep = ((fire.endAngle - fire.startAngle) / fire.numberOfBullet) * .5f;
        fire.angle = fire.startAngle;

        for (int i = 0; i < fire.numberOfBullet; i++)
        {
            float x = transform.position.x + Mathf.Sin((fire.angle * Mathf.PI) / 180f);
            float y = transform.position.y + Mathf.Cos((fire.angle * Mathf.PI) / 180f);
            Vector3 dir = new Vector3(x, y, 0);
            Vector2 bulletDir = (dir - transform.position).normalized;

            GameObject bul = fire.pool.GetFromPool();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<CircleEnemyBullet>().SetMoveDirection(bulletDir);

            fire.angle += fire.angleStep;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet_1"))
        {
            StartCoroutine(Damage());
            animator.SetTrigger("Hit");
            StartCoroutine(Flash());
            shake.C_Shake(.1f, 1.5f, .8f);
            Destroy(collision.transform.gameObject);
            health -= 1f;
        }
    }

    IEnumerator Flash()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(.1f);
        sr.color = Color.white;
    }

    IEnumerator Damage()
    {
        speed = speed - 6f;
        yield return new WaitForSeconds(.2f);
        speed = 10f;
    }
}
