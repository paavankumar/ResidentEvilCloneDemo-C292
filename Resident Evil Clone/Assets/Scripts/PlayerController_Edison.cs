using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Edison : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float mouseSensitivity = 60f;

    [SerializeField] float verticalLookLimit;
    [SerializeField] Transform fpsCamera;

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject Projectile;

    private bool isGrounded;
    private float xRotation;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Move();
        LookAround();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    private void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -verticalLookLimit, verticalLookLimit);

        fpsCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        Vector3 moveVelocity = move * moveSpeed;

        moveVelocity.y = rb.velocity.y;

        rb.velocity = moveVelocity;
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, fpsCamera.forward, out hit, 100))
        {
            Debug.DrawRay(firePoint.position, firePoint.forward * hit.distance, Color.red, 2f);
            if (hit.transform.CompareTag("Zombie"))
            {
                hit.transform.GetComponent<Zombie_Edison>().TakeDamage(1);
            }
        }
    }

    private void ShootBullet()
    {
        GameObject bullet = Instantiate(Projectile, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(firePoint.forward * 10, ForceMode.Impulse);
    }
}
