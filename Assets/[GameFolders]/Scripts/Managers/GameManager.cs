using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    #region Params
    private bool isGameStarted;
    public bool IsGameStarted { get { return isGameStarted; } set { isGameStarted = value; } }

    private bool isStageCompleted;
    public bool IsStageCompleted { get { return isStageCompleted; } set { isStageCompleted = value; } }

    #endregion
    #region Events
    [HideInInspector]
    public UnityEvent OnGameStart = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnGameEnd = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnStageWin = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnStageLoose = new UnityEvent();
    #endregion
    #region MonoBehaviourMethods
    private void OnEnable()
    {
        OnStageWin.AddListener(() => CompeleteStage(true));
        OnStageLoose.AddListener(() => CompeleteStage(false));
        LevelManager.Instance.OnLevelStart.AddListener(() => IsStageCompleted = false);
    }
    private void OnDisable()
    {
        OnStageWin.RemoveListener(() => CompeleteStage(true));
        OnStageLoose.RemoveListener(() => CompeleteStage(false));
        LevelManager.Instance.OnLevelStart.RemoveListener(() => IsStageCompleted = false);

    }
    #endregion
    #region MyMethods
    private void Start()
    {
        StartGame();
    }
    public void StartGame()
    {
        if (isGameStarted)
            return;

        isGameStarted = true;
        OnGameStart.Invoke();
    }
    public void EndGame()
    {
        if (!isGameStarted)
            return;
        isGameStarted = false;
        OnGameEnd.Invoke();
    }
    public void CompeleteStage(bool value)
    {
        if (!LevelManager.Instance.IsLevelStarted)
            return;
        Debug.Log("Win");

        if (IsStageCompleted == true)
            return;
        StartCoroutine(WaitLevelChange(value));
        IsStageCompleted = true;
    }

    IEnumerator WaitLevelChange(bool status)
    {
        yield return new WaitForSeconds(2);
        if (status)
            LevelManager.Instance.LoadNextLevel();
        else
            LevelManager.Instance.ReloadLevel();
    }
    #endregion
}