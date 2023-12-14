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
    }
}
