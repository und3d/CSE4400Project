using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBombermanLives : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PersistentController.Instance.GetComponent<AudioSource>().Stop();
        PersistentController.Instance.BombermanLives = 3;
        PersistentController.Instance.GetComponent<AudioSource>().clip = PersistentController.Instance.BombermanMusic;
        PersistentController.Instance.GetComponent<AudioSource>().Play();
    }
}
