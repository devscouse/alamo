using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float range;
    private float currDistance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float distance = speed * Time.deltaTime;
        currDistance += distance;
        if (currDistance >= range)
        {
            Destroy(gameObject);
        }
        transform.Translate(distance * Vector3.forward);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

    }
}
