using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button button;
    public float difficulty;
    private SpawnManager gameManager;
    public AudioClip buttonSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        button = gameObject.GetComponent<Button>();
        gameManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        button.onClick.AddListener(SetDifficulty);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void SetDifficulty()
    {
        gameManager.StartGame(difficulty);
    }
}
