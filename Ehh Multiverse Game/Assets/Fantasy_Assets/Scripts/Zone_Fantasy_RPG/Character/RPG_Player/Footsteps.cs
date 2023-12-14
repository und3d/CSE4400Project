using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioClip[] footstepsSFX;
    public AudioSource audioSource;
    public Rigidbody2D rig;
    public float playRate;
    private float lastPlayTime;

    void Update ()
    {
        if(rig.velocity.magnitude > 0 && Time.time - lastPlayTime > playRate)
        {
            Play();
        }
    }

    void Play ()
    {
        lastPlayTime = Time.time;

        AudioClip clipToPlay = footstepsSFX[Random.Range(0, footstepsSFX.Length)];
        audioSource.PlayOneShot(clipToPlay);
    }
}
