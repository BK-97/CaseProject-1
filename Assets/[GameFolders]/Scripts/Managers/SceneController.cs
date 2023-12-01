using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    #region Events
    [HideInInspector]
    public UnityEvent OnSceneStartedLoading = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnSceneUnloading = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnSceneLoaded = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnSceneUnLoaded = new UnityEvent();
    [HideInInspector]
    public SceneEvent OnSceneInfo = new SceneEvent();
    #endregion
    public bool loadingInProgress { get; private set; }
    #region Methods
    public void LoadScene(int buildIndex)
    {
        if (loadingInProgress)
            return;

        StartCoroutine(LoadSceneCo(buildIndex));
    }

    IEnumerator LoadSceneCo(int buildIndex)
    {
        loadingInProgress = true;
        yield return new WaitForSeconds(1f);

        OnSceneStartedLoading.Invoke();

        yield return SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Additive);
        var loadedScene = SceneManager.GetSceneByBuildIndex(buildIndex);
        SceneManager.SetActiveScene(loadedScene);
        yield return new WaitForSeconds(0.2f);
        OnSceneLoaded.Invoke();
        OnSceneInfo.Invoke(loadedScene, true);
        GameManager.Instance.IsGameStarted = true;
        loadingInProgress = false;
    }

    public void UnloadScene(int sceneIndex)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(0));
        var test = SceneManager.GetSceneByBuildIndex(sceneIndex);
        StartCoroutine(UnloadSceneCo(sceneIndex));
    }

    IEnumerator UnloadSceneCo(int sceneIndex)
    {
        OnSceneUnloading.Invoke();
        yield return SceneManager.UnloadSceneAsync(sceneIndex);
        OnSceneUnLoaded.Invoke();
    }
    #endregion
}

public class SceneEvent : UnityEvent<Scene, bool> { }