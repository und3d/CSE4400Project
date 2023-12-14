using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRPGLives : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PersistentController.Instance.RPGLives = 3;
        PersistentController.Instance.GetComponent<AudioSource>().Stop();
    }
}
