using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
 
    public void StartFirstLevel()
    {
        ScenePersist scenePersist = FindObjectOfType<ScenePersist>();
        Destroy(scenePersist);
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenu()
    {
        ScenePersist scene = FindObjectOfType<ScenePersist>();
        Destroy(scene);
        GameSession game = FindObjectOfType<GameSession>();
        game.ResetGameSession();
        Destroy(game);
     
      
        SceneManager.LoadScene(0);
    
   }
}
