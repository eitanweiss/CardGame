using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.LoadSceneAsync((int)SceneIndexes.STARTMENU,LoadSceneMode.Additive);
    }


     
    public GameObject loadingScreen;
    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    public ProgressBar bar;
    float totalSceneProgress;
    float totalSpawnProgress;
    public void LoadStartMenu()
    {
        //loadingScreen.SetActive(true);
        if(SceneManager.GetSceneByBuildIndex((int)SceneIndexes.MATCH).isLoaded)
        {
            SceneManager.UnloadSceneAsync((int)SceneIndexes.MATCH);
        }
        SceneManager.LoadSceneAsync((int)SceneIndexes.STARTMENU, LoadSceneMode.Additive);
    }


    public void LoadMatch()
    {
        //loadingScreen.SetActive(true);
        SceneManager.UnloadSceneAsync((int)SceneIndexes.STARTMENU);
        SceneManager.LoadSceneAsync((int)SceneIndexes.MATCH,LoadSceneMode.Additive);

    }

    public IEnumerator GetSceneProgress()
    {
        for (int i = 0; i < scenesLoading.Count; i++)
        {
            while (!scenesLoading[i].isDone)
            {
                totalSceneProgress = 0f;
                foreach(AsyncOperation operation in scenesLoading)
                {
                    totalSceneProgress += operation.progress;
                }
                totalSceneProgress = (totalSceneProgress/scenesLoading.Count) * 100f;
                yield return null;
            }
        }
        loadingScreen.SetActive(false);
    }


    public IEnumerator GetTotalProgress()
    {
        float totalProgress = 0f;
        //while(false)//spawning not finished
        //{
        //    //if(true)//not started yet
        //    //{
        //    //    totalSpawnProgress = 0f;
        //    //}
        //    //else
        //    //{
        //    //    //totalSpawnProgress  = progress of spawns
        //    //}
        //    totalProgress = Mathf.Round((totalSpawnProgress + totalSceneProgress) / 2);
        //}
        loadingScreen.SetActive(false);
        bar.value = totalProgress;
        yield return null;
    }

}
