using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Character : MonoBehaviour, IDamagable
{
    public enum Team
    {
        Player,
        Enemy
    }
    public string DisplayName;
    public int CurHP;
    public int MaxHP;

    public Team team;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip hitSFX;

    public event UnityAction onTakeDamage;
    public event UnityAction onHeal;

    public virtual void TakeDamage(int damageToTake)
    {
        CurHP -= damageToTake;

        audioSource.PlayOneShot(hitSFX);

        onTakeDamage?.Invoke();

        if(CurHP <= 0)
            Die();
    }

    public virtual void Die()
    {
        if (PersistentController.Instance.RPGLives > 1)
        {
            PersistentController.Instance.RPGLives -= 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Home Map");
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Zone_0");
            if (PersistentController.Instance.GlobalLives > 1)
            {
                PersistentController.Instance.GetComponent<AudioSource>().Stop();
                PersistentController.Instance.GetComponent<AudioSource>().clip = PersistentController.Instance.Zone0Music;
                PersistentController.Instance.GetComponent<AudioSource>().Play();
                PersistentController.Instance.GlobalLives -= 1;
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
                PersistentController.Instance.GlobalLives = 3;
                PersistentController.Instance.GetComponent<AudioSource>().Stop();
                PersistentController.Instance.GetComponent<AudioSource>().clip = PersistentController.Instance.MenuMusic;
                PersistentController.Instance.GetComponent<AudioSource>().Play();
            }
        }
    }

    public Team GetTeam()
    {
        return team;
    }

    public virtual void Heal (int healAmount)
    {
        CurHP += healAmount;

        if(CurHP > MaxHP)
            CurHP = MaxHP;

            onHeal?.Invoke();
    }

}
