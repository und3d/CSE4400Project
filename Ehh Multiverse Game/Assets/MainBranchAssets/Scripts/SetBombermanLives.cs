using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBombermanLives : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PersistentController.Instance.BombermanLives = 3;
    }
}
