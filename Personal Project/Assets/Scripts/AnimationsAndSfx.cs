using System.Numerics;
using UnityEngine;

public class AnimationsAndSfx : MonoBehaviour
{
    public GameObject player;
    public GameObject yellowIndicator;
    public GameObject greenIndicator;
    public GameObject redIndicator;
    private AudioSource appleAudioSource;
    public AudioClip powerUpSound;
    private bool yellowPlayed = false;
    private bool greenPlayed = false;
    private bool redPlayed = false;

    void Start()
    {
        appleAudioSource = GetComponent<AudioSource>();
    }
    void Update()
    {

        yellowIndicator.transform.position = new UnityEngine.Vector3(transform.position.x, 0, transform.position.z);
        greenIndicator.transform.position = new UnityEngine.Vector3(transform.position.x, 0, transform.position.z);
        redIndicator.transform.position = new UnityEngine.Vector3(transform.position.x, 0, transform.position.z);

        UnityEngine.Vector3 newScale = player.transform.localScale + UnityEngine.Vector3.one;

        yellowIndicator.transform.localScale = newScale;
        greenIndicator.transform.localScale = newScale;
        redIndicator.transform.localScale = newScale;

        // YELLOW
        if(player.transform.localScale.x > 1.25f && !yellowPlayed)
        {
            yellowPlayed = true;
            yellowIndicator.SetActive(true);
            appleAudioSource.PlayOneShot(powerUpSound);
        }

        // GREEN
        if(player.transform.localScale.x > 1.7f && !greenPlayed)
        {
            greenPlayed = true;
            greenIndicator.SetActive(true);
            appleAudioSource.PlayOneShot(powerUpSound);
        }

        // RED
        if(player.transform.localScale.x > 2.5f && !redPlayed)
        {
            redPlayed = true;
            redIndicator.SetActive(true);
            appleAudioSource.PlayOneShot(powerUpSound);
        }
    }
}
