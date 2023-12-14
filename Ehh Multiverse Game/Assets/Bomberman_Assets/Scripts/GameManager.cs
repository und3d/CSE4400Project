using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject[] players;

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void CheckWinState()
    {
        int alivePlayers = 0;
        int aliveAI = 0;

        foreach (GameObject player in players)
        {
            if (player.activeSelf && player.CompareTag("Player"))
            {
                alivePlayers++;
            }
            else if (player.activeSelf && player.CompareTag("AI"))
            {
                aliveAI++;
            }
        }

        // Check if only one player is alive or only AI are left
        if (alivePlayers == 1 && aliveAI == 0)
        {
            // Player wins
            Invoke(nameof(NewRound), 3f);
        }
        else if (alivePlayers == 0 && aliveAI > 0)
        {
            // AI wins
            Invoke(nameof(NewRound), 3f);
        }
    }


    private void NewRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}


/*using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject[] players;

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void CheckWinState()
    {
        int aliveCount = 0;

        foreach (GameObject player in players)
        {
            if (player.activeSelf) {
                aliveCount++;
            }
        }

        if (aliveCount <= 1) {
            Invoke(nameof(NewRound), 3f);
        }
    }

    private void NewRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}*/