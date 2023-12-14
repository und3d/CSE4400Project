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
