using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDefeated : MonoBehaviour
{
    GameObject slime;

    private void FixedUpdate()
    {
        slime = GameObject.FindGameObjectWithTag("Enemy");
        CheckIfBossDefeated();
    }

    public void CheckIfBossDefeated()
    {
        if (slime == null)
        {
            SceneManager.LoadScene("Zone_0");
            PersistentController.Instance.GetComponent<AudioSource>().Stop();
            PersistentController.Instance.GetComponent<AudioSource>().clip = PersistentController.Instance.Zone0Music;
            PersistentController.Instance.GetComponent<AudioSource>().Play();
        }
    }
}
