using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : Singleton<InputManager>
{
    private InputActions input = null;
    private Vector2 moveVector = Vector2.zero;
    private void Update()
    {
        if (!LevelManager.Instance.IsLevelStarted && GameManager.Instance.IsGameStarted)
        {
            if (Input.GetMouseButtonDown(0))
                LevelManager.Instance.OnLevelStart.Invoke();
        }
    }
    private void OnEnable()
    {
        input = new InputActions();
        LevelManager.Instance.OnLevelStart.AddListener(AddInputListeners);
        LevelManager.Instance.OnLevelFinish.RemoveListener(RemoveInputListeners);
    }
    private void OnDisable()
    {
        LevelManager.Instance.OnLevelStart.RemoveListener(AddInputListeners);
        LevelManager.Instance.OnLevelFinish.RemoveListener(RemoveInputListeners);
    }
    private void AddInputListeners()
    {
        input.Enable();
        input.Player.WASD.performed += OnMovementPerformed;
        input.Player.WASD.canceled += OnMovementCancelled;
    }
    private void RemoveInputListeners()
    {
        input.Disable();
        input.Player.WASD.performed -= OnMovementPerformed;
        input.Player.WASD.canceled -= OnMovementCancelled;
    }
    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        if (!LevelManager.Instance.IsLevelStarted)
            return;
        moveVector = value.ReadValue<Vector2>();
    }
    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        if (!LevelManager.Instance.IsLevelStarted)
            return;
        moveVector = Vector2.zero;
    }
    public Vector3 GetDirection()
    {
        Vector3 direction = new Vector3(moveVector.x, 0, moveVector.y);
        return direction;
    }
}
