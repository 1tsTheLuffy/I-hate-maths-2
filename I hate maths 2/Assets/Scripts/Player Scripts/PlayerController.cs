// This script consist of everything related to player like shooting, movement, powerUps Pickup.
// I am a lazy fuck.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerController))]
public class PlayerController : MonoBehaviour
{
    private Vector2 movement;
    private Vector2 mousePos;
    #region float
    [Header("Floats")]
    [SerializeField] float x1 = 1f;
    [SerializeField] float x2 = 1f;
    [SerializeField] float y1 = 1f;
    [SerializeField] float y2 = 1f;
    [SerializeField] float startTimeForShoot = 1f;
    [SerializeField] float timeBtwShoot = 1f;
    [SerializeField] float waitTime = .2f;
    public float health = 15f;
    #endregion

    [Header("GameObject")]
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject[] bulletType;
    [SerializeField] GameObject shootParticle;
    [SerializeField] GameObject[] shootParticleType;
    [SerializeField] GameObject[] powerUpTaken;

    [Header("Transforms")]
    [SerializeField] Transform shootPoint;

    [Header("Camera Shake")]
    [SerializeField] CameraShake shake;
    [SerializeField] float shakeDuration = .1f;
    [SerializeField] float shakeAmplitude = 1.2f;
    [SerializeField] float shakeFrequency = .5f;

    [Header("Colors")]
    [SerializeField] Color[] flash;

    [Header("UI")]
    [SerializeField] HealthBar bar;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Camera cam;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        cam = Camera.main;

        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();

        bullet = bulletType[0];
        shootParticle = shootParticleType[0];

        startTimeForShoot = timeBtwShoot;

        bar.setMaxHealth(health);

        Cursor.visible = false;
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, x1, x2), Mathf.Clamp(transform.position.y, y1, y2), transform.position.z);

        if(Input.GetMouseButton(0) && startTimeForShoot <= 0)
        {
            shake.C_Shake(shakeDuration, shakeAmplitude, shakeFrequency);
         //   StartCoroutine(Flash(flash[1]));
            GameObject instance = Instantiate(shootParticle, shootPoint.position, shootPoint.rotation * new Quaternion(0f, 90f, 0f, 0f));
            Shoot();
            Destroy(instance, 1f);
            startTimeForShoot = timeBtwShoot;
        }else
        {
            startTimeForShoot -= Time.deltaTime;
        }

        if(health <= 0)
        {
            Destroy(gameObject);
            return;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            health--;
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            health++;
        }

        bar.setHealth(health);
    }

    #region Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet_2_PowerUp"))
        {
            Destroy(collision.transform.gameObject);
            GameObject instance = Instantiate(powerUpTaken[0], transform.position, Quaternion.identity);
            bullet = bulletType[1];
            shootParticle = shootParticleType[1];
            waitTime = .2f;
            StartCoroutine(Flash(Color.blue));
            Destroy(instance, 1f);
            return;
        }
        if(collision.CompareTag("Bullet_3_PowerUp"))
        {
            Destroy(collision.transform.gameObject);
            GameObject instance = Instantiate(powerUpTaken[1], transform.position, Quaternion.identity);
            bullet = bulletType[2];
            shootParticle = shootParticleType[2];
            waitTime = .2f;
            StartCoroutine(Flash(Color.yellow));
            Destroy(instance, 1f);
            return;
        }

        if(collision.CompareTag("Health_PowerUp"))
        {
            Destroy(collision.transform.gameObject);
            GameObject instance = Instantiate(powerUpTaken[2], transform.position, Quaternion.identity);
            health += 5f;
            waitTime = .2f;
            StartCoroutine(Flash(flash[2]));
            Destroy(instance, 1f);
            return;
        }
    }
    #endregion

    private GameObject Shoot()
    {
        Instantiate(bullet, shootPoint.position, Quaternion.identity);
        return bullet;
    }

    IEnumerator Flash(Color color)
    {
        sr.color = color;
        yield return new WaitForSeconds(waitTime);
        sr.color = flash[0];
    }
}
