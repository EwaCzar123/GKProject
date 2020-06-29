using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelExit : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay = 0.7f;


    void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadNextLevel());
    }
  
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(LevelLoadDelay);
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;


       ScenePersist scenePersist = FindObjectOfType<ScenePersist>();
       Destroy(scenePersist);

        SceneManager.LoadScene(currentSceneIndex + 1);
    }

}
