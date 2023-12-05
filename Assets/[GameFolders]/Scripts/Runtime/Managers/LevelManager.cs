using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class LevelManager : Singleton<LevelManager>
{
    #region Events
    [HideInInspector]
    public UnityEvent OnLevelStart = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnLevelFinish = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnLevelRestart = new UnityEvent();
    #endregion
    #region Params

    private bool isLevelStarted;
    public bool IsLevelStarted { get { return isLevelStarted; } set { isLevelStarted = value; } }

    public int LevelIndex
    {
        get
        {
            int level = PlayerPrefs.GetInt("CurrentLevel", 0);
            if (level >=  SceneManager.sceneCountInBuildSettings - 2)
            {
                level = 0;
            }
            return level;
        }
        set
        {
            PlayerPrefs.SetInt("CurrentLevel", value);
        }
    }
    #endregion
    #region Methods
    private void OnEnable()
    {
        GameManager.Instance.OnStageLoose.AddListener(ReloadLevel);
        OnLevelStart.AddListener(StartLevel);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnStageLoose.RemoveListener(ReloadLevel);
        OnLevelStart.RemoveListener(StartLevel);
    }
    public void ReloadLevel()
    {
        FinishLevel();
        StartCoroutine(WaitForRestart());

        //Use the code below to restart the level by removing/loading the current level and delete the code above.

        //SceneController.Instance.UnloadScene(LevelIndex + 2);
        //SceneController.Instance.LoadScene(LevelIndex + 2);
    }
    IEnumerator WaitForRestart()
    {
        yield return new WaitForSeconds(1);
        OnLevelRestart.Invoke();
        GameManager.Instance.IsStageCompleted = false;
    }

    public void LoadLastLevel()
    {
        FinishLevel();
        SceneController.Instance.LoadScene(LevelIndex + 2);
    }
    public void LoadNextLevel()
    {
        FinishLevel();
        SceneController.Instance.UnloadScene(LevelIndex + 2);
        LevelIndex++;
        if (LevelIndex > SceneManager.sceneCountInBuildSettings - 2)
        {
            LevelIndex = 0;
        }

        SceneController.Instance.LoadScene(LevelIndex + 2);
    }
    public void LoadPreviousLevel()
    {
        FinishLevel();

        LevelIndex--;
        if (LevelIndex <= -1)
        {
            LevelIndex = SceneManager.sceneCountInBuildSettings - 2;

        }

        SceneController.Instance.LoadScene(LevelIndex + 2);
    }
    private void StartLevel()
    {
        if (IsLevelStarted)
            return;
        IsLevelStarted = true;
    }

    public void FinishLevel()
    {
        if (!IsLevelStarted)
            return;

        IsLevelStarted = false;
        OnLevelFinish.Invoke();
    }
    #endregion

}