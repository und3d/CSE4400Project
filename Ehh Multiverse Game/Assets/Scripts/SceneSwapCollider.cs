using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapCollider : MonoBehaviour
{
    public Object swapToScene;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");
        SceneManager.LoadScene(swapToScene.name);
    }
}
