// This script consist of everything related to player like shooting, movement, powerUps Pickup.
// I am a lazy fuck.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerController))]
public class PlayerController : MonoBehaviour
{
    #region VARIABLES

    private bool isDeHeat;
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
    [SerializeField] float heatNum = 0;
    [SerializeField] float heatTimer = .2f;
    [SerializeField] float startHeatTimer = .2f;
    [SerializeField] float startTimeForBullet = 1f;
    [SerializeField] float timeToChangeTheBullet = 1f;
    [SerializeField] float scoreTimer = .1f;
    [SerializeField] float timeToIncreaseScore = .1f;
    public float health = 15f;
    public int score = 0;
    #endregion

    [Header("GameObject")]
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject[] bulletType;
    [SerializeField] GameObject shootParticle;
    [SerializeField] GameObject[] shootParticleType;
    [SerializeField] GameObject[] powerUpTaken;
    [SerializeField] GameObject HeatBarBorder;

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
    [SerializeField] HeatBar heat;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sr;
    Camera cam;

    #endregion

    #region START

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        cam = Camera.main;

        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();

        bullet = bulletType[0];
        shootParticle = shootParticleType[0];

        startTimeForShoot = timeBtwShoot;
        startTimeForBullet = timeToChangeTheBullet;
        scoreTimer = timeToIncreaseScore;

        bar.setMaxHealth(health);
        heat.setMinHeatValue(heatNum);

        isDeHeat = false;

        HeatBarBorder.SetActive(false);

        Cursor.visible = false;
    }

    #endregion

    #region UPDATE

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, x1, x2), Mathf.Clamp(transform.position.y, y1, y2), transform.position.z);

        // SHOOTING........

        if(Input.GetMouseButton(0) && startTimeForShoot <= 0 && heatNum < 20f)
        {
            isDeHeat = false;
            shake.C_Shake(shakeDuration, shakeAmplitude, shakeFrequency);
         //   StartCoroutine(Flash(flash[1]));
            GameObject instance = Instantiate(shootParticle, shootPoint.position, shootPoint.rotation * new Quaternion(0f, 90f, 0f, 0f));
            Shoot();
            Destroy(instance, 1f);
            startTimeForShoot = timeBtwShoot;

            if (heatTimer <= 0)
            {
                heatNum++;
                heatTimer = startHeatTimer;
            }
            else
            {
                heatTimer -= Time.deltaTime;
            }

        }
        else
        {
            startTimeForShoot -= Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isDeHeat == false)
            {
                StartCoroutine(DeHeat());
            }
        }

        if(heatNum >= 19.5f && Input.GetMouseButton(0))
        {
            HeatBarBorder.SetActive(true);
        }else
        {
            HeatBarBorder.SetActive(false);
        }

        if(startTimeForBullet <= 0)
        {
            bullet = bulletType[0];
        }else
        {
            startTimeForBullet -= Time.deltaTime;
        }

        if(heatNum <= 0f)
        {
            heatNum = 0f;
        }

        if(bullet == bulletType[0])
        {
            shootParticle = shootParticleType[0];
        }else if(bullet == bulletType[1])
        {
            shootParticle = shootParticleType[1];
        }else if(bullet == bulletType[2])
        {
            shootParticle = shootParticleType[2];
        }

        heat.setHeatValue(heatNum);

        // END SHOOTING.

        if (health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(0);
            return;
        }

        if(health > 20)
        {
            health = 20;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            health--;
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            health++;
        }

        // EVERYTHING RELATED TO BULLET SWITCHING..............

        if(bullet == bulletType[0])
        {
            timeBtwShoot = .3f;
        }else if(bullet == bulletType[1])
        {
            timeBtwShoot = .4f;
        }else if(bullet == bulletType[2])
        {
            timeBtwShoot = .15f;
        }

        // END OF EVERYTHING RELATED TO BULLET SWITCHING..............

        // SCORE.......

        if(scoreTimer <= 0)
        {
            score += 1;
            scoreTimer = timeToIncreaseScore;
        }else
        {
            scoreTimer -= Time.deltaTime;
        }

        //END SCORE........

        bar.setHealth(health);
    }

    #endregion

    #region Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        #region PowerUps
        if (collision.CompareTag("Bullet_2_PowerUp"))
        {
            Destroy(collision.transform.gameObject);
            GameObject instance = Instantiate(powerUpTaken[0], transform.position, Quaternion.identity);
            bullet = bulletType[1];
            shootParticle = shootParticleType[1];
            waitTime = .2f;
            StartCoroutine(Flash(Color.blue));
            Destroy(instance, 1f);

            if(bullet == bulletType[0] || bullet == bulletType[2])
            {
                startTimeForBullet = 20f;
            }else if(bullet == bulletType[1])
            {
                startTimeForBullet += 20f;
            }

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

            if(bullet == bulletType[0] || bullet == bulletType[1])
            {
                startTimeForBullet = 20f;
            }else if(bullet == bulletType[2])
            {
                startTimeForBullet += 20f;
            }

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
        #endregion

        #region EnemiesHit

        if(collision.CompareTag("Enemy"))
        {
            SetTrigger();
            Trigger();
            Destroy(collision.transform.gameObject);
            shake.C_Shake(.1f, 2f, .8f);
            health -= 1f;
        }

        if(collision.CompareTag("EnemyBullet"))
        {
            SetTrigger();
            Trigger();
            Destroy(collision.transform.gameObject);
            shake.C_Shake(.1f, 1f, .8f);
            health -= 1f;
        }

        if(collision.CompareTag("Theta"))
        {
            SetTrigger();
            Destroy(collision.transform.gameObject);
            Trigger();
            shake.C_Shake(.1f, 2f, 1f);
            health -= 5f;
        }

        if(collision.CompareTag("EnemyPoolBullet"))
        {
            SetTrigger();
            collision.transform.gameObject.SetActive(false);
            health -= 1f;
            shake.C_Shake(.1f, 1.5f, .8f);
            Trigger();
        } 

        if(collision.CompareTag("Circle"))
        {
            SetTrigger();
            Trigger();
            shake.C_Shake(.1f, 2f, .8f);
            health -= 2f;
        }

        if(collision.CompareTag("MediumEnemy"))
        {
            SetTrigger();
            Trigger();
            shake.C_Shake(.1f, 2f, .8f);
            health -= 4f;
        }

        #endregion
    }
    #endregion

    private GameObject Shoot()
    {
        Instantiate(bullet, shootPoint.position, Quaternion.identity);
        return bullet;
    }

    private void Trigger()
    {
        StartCoroutine(Flash(Color.red));
    }

    private void SetTrigger()
    {
        animator.SetTrigger("Hit");
    }

    IEnumerator Flash(Color color)
    {
        sr.color = color;
        yield return new WaitForSeconds(waitTime);
        sr.color = flash[0];
    }

    IEnumerator DeHeat()
    {
        isDeHeat = true;
        while (isDeHeat == true)
        {
            heatNum--;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
