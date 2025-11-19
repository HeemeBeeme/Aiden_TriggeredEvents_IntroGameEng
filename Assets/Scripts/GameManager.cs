using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; protected set; }

    public float MouseSensitivity = 1.5f;
    public float Brightness = 1;

    public bool ChromaticAberrationActivity = true;
    public bool FilmGrainActivity = true;
    public bool MotionBlurActivity = true;

    public AudioSource GameMusicSource;
    public AudioSource FlashOnSource;
    public AudioSource FlashOffSource;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }



    }
    void Update()
    {
        
    }
}
