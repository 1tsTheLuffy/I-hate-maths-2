using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Alpha))]
public class Alpha : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float health = 1f;
    [SerializeField] private float timer = .1f;
    [SerializeField] private float timeBtwShoot = .1f;
    [Range(0f, 1f)]
    [SerializeField] private float destroyWaitTime = .1f;

    [SerializeField] GameObject Bullet;
    [SerializeField] GameObject destroyParticle;

    [SerializeField] Transform Player;
    [SerializeField] Transform shootPoint;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sr;
    CameraShake shake;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        Player = GameObject.FindGameObjectWithTag("Player").transform;
        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();

        timer = timeBtwShoot;
    }

    private void Update()
    {
        if (Player == null)
            return;

        if(timer <= 0)
        {
            Instantiate(Bullet, shootPoint.position, shootPoint.rotation);
            timer = timeBtwShoot;
        }else
        {
            timer -= Time.deltaTime;
        }

        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (health <= 0)
        {
            Destroy(gameObject, destroyWaitTime);
        }

        if(transform.position.x < -14f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet_1"))
        {
            animator.SetTrigger("Hit");
            StartCoroutine(Flash());
            shake.C_Shake(.1f, 1.5f, .8f);
            Destroy(collision.transform.gameObject);
            health -= 1;
        }
        if (collision.CompareTag("Bullet_2"))
        {
            animator.SetTrigger("Hit");
            StartCoroutine(Flash());
            shake.C_Shake(.1f, 1.5f, .8f);
            health -= 1;
        }
    }

    private void OnDestroy()
    {
        GameObject instance = Instantiate(destroyParticle, transform.position, Quaternion.identity);
        Destroy(instance, 1.2f);
    }

    IEnumerator Flash()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(.2f);
        sr.color = Color.magenta;
    }
}