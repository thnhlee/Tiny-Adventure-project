using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverCanvasControll : MonoBehaviour
{
    public Canvas GameLostCanvas;

    public Canvas mainMenuCanvas;
    private Player player;

    public void OnClickReplay()
    {
        GameLostCanvas.gameObject.SetActive(false); // Hide the Lost canvas
        mainMenuCanvas.gameObject.SetActive(true);
        SceneManager.LoadScene("SampleScene");


    }
    public void OnClickExit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; // This will stop the play mode in the editor
        #else
                Application.Quit(); // This will quit the application when built
        #endif
    }

}
   

