using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRPGLives : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Player")
        {
            PersistentController.Instance.RPGLives = 3;
        }
    }
}
