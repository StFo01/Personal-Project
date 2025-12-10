using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 10.0f;
    private float boostSpeed = 50.0f;
    private float rotateSpeed = 50.0f;
    private float zbound = 9.5f;
    private float xbound = 19.5f;
    private float bounceOnHit = 2.0f;
    public ParticleSystem explosionParticle;
    public ParticleSystem boostParticle;
    public ParticleSystem winParticle;
    public AudioClip crashSound;
    public AudioSource playerAudio;
    public AudioClip powerupSound;
    public AudioClip noBoostSound;
    public AudioClip boostSound;
    public AudioClip winSound;
    private AudioSource integratedSound;
    private bool canBoost = true;
    public bool gameOver = false;
    public bool winCondition = false;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        integratedSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ConstrainPlayerPosition();
    }

    //Moves the player based on arrow key input
    void MovePlayer()
    {   
        if(titleText.activeSelf == false){
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 moveDir = new Vector3(horizontalInput, 0, verticalInput).normalized;

            //Left and right movevment and rotation
            playerRb.AddForce(Vector3.right * speed * horizontalInput);
            transform.Rotate(Vector3.right * rotateSpeed * horizontalInput * Time.deltaTime);

            //Up and down movevment and rotation
            playerRb.AddForce(Vector3.forward * speed * verticalInput);
            transform.Rotate(Vector3.forward * rotateSpeed * verticalInput * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space) && canBoost)
            {
                if (moveDir != Vector3.zero)
                {
                    StartCoroutine(BoostCooldown());
                    playerRb.AddForce(moveDir * boostSpeed, ForceMode.Impulse);
                    boostParticle.Play();
                    integratedSound.PlayOneShot(boostSound);
                }
            }
            if (Input.GetKeyDown(KeyCode.Space) && !canBoost)
            {
                integratedSound.PlayOneShot(noBoostSound);
            }
        }
    }
    private IEnumerator BoostCooldown()
    {
        canBoost = false;
        yield return new WaitForSeconds(3f); // 2 second delay
        canBoost = true;
    }

    //Prevent the player from leaving the top or bottom of the screen
    void ConstrainPlayerPosition()
    {
        //Left and right bounds and velocity neutralization on hitting the bounds
        if (transform.position.x > xbound)
        {
            transform.position = new Vector3(xbound, transform.position.y, transform.position.z);
            playerRb.linearVelocity = new Vector3(-bounceOnHit, playerRb.linearVelocity.y, playerRb.linearVelocity.z);
        }
        else if (transform.position.x < -xbound)
        {
            transform.position = new Vector3(-xbound, transform.position.y, transform.position.z);
            playerRb.linearVelocity = new Vector3(bounceOnHit, playerRb.linearVelocity.y, playerRb.linearVelocity.z);
        }

        //Up and down bounds and velocity neutralization on hitting the bounds
        if (transform.position.z > zbound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zbound);
            playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, playerRb.linearVelocity.y, -bounceOnHit);
        }
        else if (transform.position.z < -zbound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zbound);
            playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, playerRb.linearVelocity.y, bounceOnHit);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            EnemyMovement worm = collision.gameObject.GetComponent<EnemyMovement>();

            if(worm.wormId == 1 && transform.localScale.x > 1.25f)
            {
                Destroy(collision.gameObject);
                transform.localScale *= 1.05f;
                integratedSound.PlayOneShot(powerupSound);
                return;
            }

            if(worm.wormId == 2 && transform.localScale.x > 1.7f)
            {
                Destroy(collision.gameObject);
                transform.localScale *= 1.05f;
                integratedSound.PlayOneShot(powerupSound);
                return;
            }

            if(worm.wormId == 3 && transform.localScale.x > 2.5f)
            {
                Destroy(collision.gameObject);
                transform.localScale *= 1.05f;
                integratedSound.PlayOneShot(winSound);
                winParticle.Play();
                winCondition = true;
                winText.gameObject.SetActive(true);
                restartButton.gameObject.SetActive(true);
                return;
            }

            explosionParticle.transform.parent = null;
            explosionParticle.Play();

            AudioSource audioInstance = Instantiate(playerAudio, transform.position, Quaternion.identity);
            audioInstance.PlayOneShot(crashSound, 1.0f);
            gameOver = true;
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Powerup"))
        {
            transform.localScale *= 1.05f;
            integratedSound.PlayOneShot(powerupSound);
            Destroy(other.gameObject);
        }
    }
}
