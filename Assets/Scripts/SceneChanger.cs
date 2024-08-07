using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeSceneToMatch()
    {
        SceneManager.LoadScene("Match");
    }
    public void ChangeSceneToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void ChangeSceneToGameEnd()
    {
        SceneManager.LoadScene("Game End");
    }

}
