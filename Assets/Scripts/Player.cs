using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float lastFireTime = 0;
    public float health;
    public float maxHealth = 100;
    public float fireRate = 1;

    public AudioSource audioSource;
    private PlayerController playerController;
    public event Action PlayerDied;

    public AudioClip shootSound;
    public GameObject projectile;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        playerController.PlayerFired += Fire;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            PlayerDied();
            playerController.active = false;
            health = 0;
        }
        else if (health > maxHealth)
        {
            health = maxHealth;
        }

    }

    void OnCollisionStay(Collision collision)
    {
        // If we collide with an enemy we need to take damage
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health -= collision.gameObject.GetComponent<Enemy>().damage * Time.deltaTime;
        }

    }

    void Fire()
    {
        float currTime = Time.time;
        if (currTime - lastFireTime < 1 / fireRate)
        {
            return;
        }

        lastFireTime = currTime;
        audioSource.PlayOneShot(shootSound);
        Instantiate(projectile, transform.position, transform.rotation);

        // Does the ray intersect any objects excluding the player layer
        // if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit))
        // {
        //     Destroy(hit.collider.gameObject);
        //     Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        // }
        // else
        // {
        //     Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        // }

    }
}
