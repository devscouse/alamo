using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rb;
    public float acceleration;
    public float maxSpeed;
    public float damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rb == null)
        {
            return;
        }

        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        float distanceToPlayer = (player.transform.position - transform.position).magnitude;

        float acc = acceleration;
        if (distanceToPlayer < 2) { acc *= 2; }
        rb.AddForce(directionToPlayer * acc, ForceMode.Acceleration);

        float currSpeed = rb.linearVelocity.magnitude;
        if (currSpeed > maxSpeed)
        {
            rb.AddForce(rb.linearVelocity * (maxSpeed - currSpeed), ForceMode.VelocityChange);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision detected between enemy and player");
        }
    }
}
