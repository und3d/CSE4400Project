using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentController : MonoBehaviour
{
    public static PersistentController Instance { get; set; }

    public int GlobalLives = 3;
    public int BombermanLives = 3;
    public int RPGLives = 3;
    public int DungeonLives = 3;

    public AudioClip DungeonMusic;
    public AudioClip BombermanMusic;
    public AudioClip MenuMusic;
    public AudioClip Zone0Music;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.LoadScene("MainMenu");
        GetComponent<AudioSource>().clip = MenuMusic;
        GetComponent<AudioSource>().Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
            Instance.GetComponent<AudioSource>().Stop();
            Instance.GetComponent<AudioSource>().clip = Instance.MenuMusic;
            Instance.GetComponent<AudioSource>().Play();
        }
    }
}
