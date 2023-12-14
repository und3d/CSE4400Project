using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDungeonLives : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Player")
        {
            PersistentController.Instance.DungeonLives = 3;
            PersistentController.Instance.GetComponent<AudioSource>().clip = PersistentController.Instance.DungeonMusic;
            PersistentController.Instance.GetComponent<AudioSource>().Play();
        }
    }
}
