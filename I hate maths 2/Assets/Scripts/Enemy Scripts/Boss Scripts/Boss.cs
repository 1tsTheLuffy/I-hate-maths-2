﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Boss))]
public class Boss : MonoBehaviour
{
    public float health;
    public float currentHealth;

    private int randomMovePoint;

    [Header("Floats")]
    [SerializeField] int eventType;
    [SerializeReference] int secondStageEvent;
    [SerializeField] float movementSpeed;
    [SerializeField] float chaseSpeed;

    [Header("Timers")]
    [SerializeField] float startTime;
    [SerializeField] float timeToMoveToNextState;
    [SerializeField] float timer;
    [SerializeField] float timeToShoot;

    [Space]
    [SerializeField] Vector2 target;
    [Space]

    [Header("GameObjects")]
    [SerializeField] GameObject[] Bullet;

    [Header("Transform")]
    [SerializeField] Transform[] movePoints;
    [SerializeField] Transform[] shootPoint;
    [SerializeField] Transform bus;

    [SerializeField] BossHealthBar healthBar;

    [Header("Color")]
    [SerializeField] Color[] hitColor;

    CameraShake shake;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        bus = GameObject.FindGameObjectWithTag("Player").transform;
        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();

        target = new Vector2(bus.position.x, bus.position.y);

        startTime = timeToMoveToNextState;
        timer = timeToShoot;

        health = 50;
        currentHealth = health;
        healthBar.setMaxHealth(health);


        eventType = Random.Range(1, 4);
        secondStageEvent = Random.Range(1, 3);
        randomMovePoint = Random.Range(0, movePoints.Length);
    }

    private void Update()
    {
        
        transform.localScale = new Vector3(1f, 1f, 1f);

        if(Input.GetKeyDown(KeyCode.Keypad1))
        {

        }



        // FIRST STAGE.............

        if (health > 25)                       
        {

           // sr.color = hitColor[0];


            if (eventType == 1)
            {
                animator.SetBool("isGun", true);
                animator.SetBool("isThorn", false);

                transform.Rotate(0f, 0f, 10f);

                transform.position = Vector2.MoveTowards(transform.position, movePoints[randomMovePoint].position, movementSpeed * Time.deltaTime);

                if (timer <= 0)
                {
                    shoot();
                    timer = timeToShoot;
                }
                else
                {
                    timer -= Time.deltaTime;
                }

                if (Vector2.Distance(transform.position, movePoints[randomMovePoint].position) < .2f)
                {
                    randomMovePoint = Random.Range(0, movePoints.Length);
                }

                if (startTime <= 0)
                {
                    eventType = Random.Range(1, 4);
                    if (eventType == 2)
                    {
                        chaseSpeed = Random.Range(20f, 26f);
                        target = new Vector2(bus.position.x, bus.position.y);
                    }
                    startTime = timeToMoveToNextState;
                }
                else
                {
                    startTime -= Time.deltaTime;
                }
            }
            else if (eventType == 2)
            {
                animator.SetBool("isThorn", true);
                animator.SetBool("isGun", false);
                transform.Rotate(0f, 0f, 10f);

                transform.position = Vector2.MoveTowards(transform.position, target, chaseSpeed * Time.deltaTime);
            }
            else if (eventType == 3)
            {
                animator.SetBool("isThorn", false);
                animator.SetBool("isGun", false);

                if (startTime <= 0)
                {
                    eventType = Random.Range(1, 4);
                    if (eventType == 3)
                    {
                        eventType = 2;
                    }
                    startTime = timeToMoveToNextState;
                }
                else
                {
                    startTime -= Time.deltaTime;
                }
            }

            if (transform.position.x == target.x && transform.position.y == target.y)
            {
                eventType = 2;
                target = new Vector2(bus.position.x, bus.position.y);
                //randomMovePoint = Random.Range(0, movePoints.Length);
            }
        } ////////////////////////////////////////////////////////////// END OF FIRST STAGE......................................................
        if (health <= 25)                   // +++++++++++ SECOND STAGE OF THE BOSS +++++++++++++ \\
        {
          //  sr.color = hitColor[1];
            animator.SetTrigger("Second");

            if (secondStageEvent == 1)
            {
                transform.Rotate(0f, 0f, 10f);

                transform.position = Vector2.MoveTowards(transform.position, movePoints[randomMovePoint].position, movementSpeed * Time.deltaTime);

                if (Vector2.Distance(transform.position, movePoints[randomMovePoint].position) < .2f)
                {
                    randomMovePoint = Random.Range(0, movePoints.Length);
                }

                if (timer <= 0)
                {
                    shoot();
                    timer = .5f;
                }
                else
                {
                    timer -= Time.deltaTime;
                }

                if (startTime <= 0)
                {
                    secondStageEvent = Random.Range(1, 3);
                    if (secondStageEvent == 2)
                    {
                        chaseSpeed = Random.Range(20f, 28f);
                        target = new Vector2(bus.position.x, bus.position.y);
                    }
                    startTime = timeToMoveToNextState;
                }
                else
                {
                    startTime -= Time.deltaTime;
                }
            }
            else if (secondStageEvent == 2)
            {
                sr.color = Color.red;
                transform.Rotate(0f, 0f, 5f);
                transform.position = Vector2.MoveTowards(transform.position, target, chaseSpeed * Time.deltaTime);

                if (timer <= 0)
                {
                    shoot();
                    timer = .9f;
                }
                else
                {
                    timer -= Time.deltaTime;
                }

                if (startTime <= 0)
                {
                    secondStageEvent = Random.Range(1, 3);
                    startTime = timeToMoveToNextState;
                }
                else
                {
                    startTime -= Time.deltaTime;
                }
            }
            // For Second Stage Only..

            if (transform.position.x == target.x && transform.position.y == target.y)
            {
                secondStageEvent = 2;
                target = new Vector2(bus.position.x, bus.position.y);
            }
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            health = 24;
        }

        healthBar.setHealth(health);
    }


    // +++++++ TRIGGER +++++++ \\
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            shake.C_Shake(.07f, 2.5f, .8f);
            int randomEvent = Random.Range(1, 3);
            eventType = 1;
            secondStageEvent = 1;
        }

        if(collision.CompareTag("Bullet_1"))
        {
            StartCoroutine(Flash());
            Destroy(collision.transform.gameObject);
            shake.C_Shake(.1f, 1.5f, .8f);
            health -= 1f;
        }

        if(collision.CompareTag("Bullet_2"))
        {
            StartCoroutine(Flash());
            Destroy(collision.transform.gameObject);
            shake.C_Shake(.1f, 1.5f, .8f);
            health -= 2f;
        }

        //if (collision.CompareTag("Bullet_1"))
        //{
        //    if (eventType == 3)
        //    {
        //        shake.C_Shake(.08f, 1f, .8f);
        //        StartCoroutine(hitFlash());
        //        Destroy(collision.transform.gameObject);
        //        health -= 1;
        //      //  GameObject instance = Instantiate(damageParticle, collision.transform.position, Quaternion.identity);
        //      //  Destroy(instance, 1.2f);
        //    }
        //    if (eventType == 2)
        //    {
        //        StartCoroutine(hitFlash());
        //        shake.C_Shake(.08f, 1f, .8f);
        //        Destroy(collision.transform.gameObject);
        //        health -= 1;
        //        eventType = 1;
        //    }
        //    if (health < 25)
        //    {
        //        if (secondStageEvent == 1)
        //        {
        //            StartCoroutine(hitFlash());
        //            shake.C_Shake(.8f, 1f, .8f);
        //            Destroy(collision.transform.gameObject);
        //            health -= 1;
        //           // GameObject instance = Instantiate(damageParticle, collision.transform.position, Quaternion.identity);
        //           // Destroy(instance, 1.2f);
        //        }
        //        if (secondStageEvent == 2)
        //        {
        //            StartCoroutine(hitFlash());
        //            shake.C_Shake(.8f, 1f, .8f);
        //            Destroy(collision.transform.gameObject);
        //        }
        //    }

      //  }
    }

    private void shoot()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(Bullet[i], shootPoint[i].position, shootPoint[i].rotation);
        }
    }

    IEnumerator Flash()
    {
        sr.color = Color.red; 
        yield return new WaitForSeconds(.2f);
        if(health > 25f)
        {
            sr.color = hitColor[0];
        }else if(health < 25f)
        {
            sr.color = hitColor[1];
        }
    }
}
