using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone0MusicCycle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PersistentController.Instance.GetComponent<AudioSource>().Stop();
        PersistentController.Instance.GetComponent<AudioSource>().clip = PersistentController.Instance.Zone0Music;
        PersistentController.Instance.GetComponent<AudioSource>().Play();
    }
}
