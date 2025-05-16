using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healAmount = 50;
    public AudioClip pickupSound;
    public AudioClip effectSound;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected for HealthPickup");
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.audioSource.PlayOneShot(pickupSound, 1.0f);
            player.audioSource.PlayOneShot(effectSound, 1.0f);
            player.health += healAmount;
            Destroy(gameObject);
        }
    }
}
