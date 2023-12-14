using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        PersistentController.Instance.GetComponent<AudioSource>().Stop();
        PersistentController.Instance.GetComponent<AudioSource>().clip = PersistentController.Instance.Zone0Music;
        PersistentController.Instance.GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Zone_0");
    }

    public void QuitGame()
    {
        PersistentController.Instance.GetComponent<AudioSource>().Stop();
        Application.Quit();
    }
}
