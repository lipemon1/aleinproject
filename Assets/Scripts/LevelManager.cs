using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    void Awake()
    {
        Cursor.visible = true;
    }

    public void LoadLevel(string name)
    {
        Application.LoadLevel(name);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadNextLevel()
    {
        Application.LoadLevel(Application.loadedLevel + 1);
        
    }
    
}
