using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelExit : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay = 1f;

    //ostatni level, do poprawy, ale po przekroczeniu ostatniego portalu powrot do level1
    [SerializeField] int Lastlevel = 2;
    void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadNextLevel());
    }
  
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(LevelLoadDelay);
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentSceneIndex == Lastlevel )
            SceneManager.LoadScene(0);
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

}
