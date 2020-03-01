using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerController))]
public class PlayerController : MonoBehaviour
{
    private Vector2 movement;
    private Vector2 mousePos;
    [Header("Floats")]
    [SerializeField] float x1 = 1f;
    [SerializeField] float x2 = 1f;
    [SerializeField] float y1 = 1f;
    [SerializeField] float y2 = 1f;
    [SerializeField] float shakeDuration = .1f;
    [SerializeField] float shakeAmplitude = 1.2f;
    [SerializeField] float shakeFrequency = .5f;

    [Header("Camera Shake")]
    [SerializeField] CameraShake shake;

    Rigidbody2D rb;
    Camera cam;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;

        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();

        Cursor.visible = false;
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, x1, x2), Mathf.Clamp(transform.position.y, y1, y2), transform.position.z);

        if(Input.GetMouseButtonDown(0))
        {
            shake.C_Shake(shakeDuration, shakeAmplitude, shakeFrequency);
        }
    }
}
