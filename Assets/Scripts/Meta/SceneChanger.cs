using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeSceneToMatch()
    {
        GameManager.Instance.LoadMatch();
    }
    public void ChangeSceneToMenu()
    {
        GameManager.Instance.LoadStartMenu();
    }
    public void ChangeSceneToGameEnd()
    {
        SceneManager.LoadScene("Game End");
    }

}
