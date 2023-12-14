using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RemoveDoor : MonoBehaviour
{
    GameObject slime;

    private void FixedUpdate()
    {
        slime = GameObject.FindGameObjectWithTag("Enemy");
        CheckIfLastEnemy();
    }

    public void CheckIfLastEnemy()
    {
        if (slime == null)
        {
            Destroy(gameObject);
        }
    }
}
