using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public int LevelOne;
    // load scene 1 when play button is clicked
    public void playGame() {
      SceneManager.LoadScene("LevelOne");
    }
}
