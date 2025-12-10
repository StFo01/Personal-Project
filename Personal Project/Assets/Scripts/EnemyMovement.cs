using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 3.0f;
    public int wormId;
    private Rigidbody enemyRb;
    private GameObject player;
    private float zDestroy = 12.0f;
    private float xDestroy = 22.0f;
    private Vector3 lookDirection; // This holds the one-time calculated direction

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        
        // 1. Calculate the direction vector (DONE ONCE)
        lookDirection = PlayerMovement();

        // 2. Calculate and apply the rotation (DONE ONCE)
        // This makes the enemy face the direction of the player at the moment of spawning.
        if (player != null)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            transform.rotation = targetRotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 3. Apply the velocity using the direction calculated in Start()
        // Since 'lookDirection' is not updated here, the movement is a straight line.
        enemyRb.linearVelocity = lookDirection * speed;

        // Destruction logic
        if(transform.position.z > zDestroy || transform.position.z < -zDestroy || transform.position.x > xDestroy || transform.position.x < -xDestroy)
        {
            Destroy(gameObject);
        }
    }

    Vector3 PlayerMovement()
    {
        // Ensure the player object exists before trying to get its position
        if (player != null)
        {
            // Calculate direction from enemy to player and normalize it (length of 1)
            Vector3 direction = (player.transform.position - transform.position).normalized;
            return direction;
        }
        return Vector3.zero; // Return zero if player isn't found
    }
}