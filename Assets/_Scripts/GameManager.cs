using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    private void Start()
    {
        
    }

    private void Update()
    {
        //Start game if player press enter or the start text
        if (Input.GetKeyDown(KeyCode.Return))
            StartGame();

        //Quit game if player press escape key
        if (Input.GetKeyDown(KeyCode.Escape))
            QuitGame();
    }

    #region UI Functions
    public void StartGame()
    {
        //DO the UI hide, reset operations here
        Debug.Log("Start Game");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
    #endregion
}
