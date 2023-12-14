using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
    public float bombFuseTime = 3f;
    public Explosion explosionPrefab;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;
    public LayerMask explosionLayerMask;

    private void Start()
    {
        StartCoroutine(ExplodeAfterDelay());
    }

    private IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(bombFuseTime);
        // Explosion logic (similar to what was previously in BombController)
        Explode();
        Destroy(gameObject); // Destroy the bomb after triggering the explosion
    }

    private void Explode()
    {
        Vector2 position = transform.position;
        // ... (Explosion instantiation and handling, similar to BombController)
    }
}
