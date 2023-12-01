using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
public class InputManager : Singleton<InputManager>
{
    private InputActions input = null;
    private Vector2 moveVector = Vector2.zero;
    public static UnityEvent OnInteractInput = new UnityEvent();
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
        input.Player.Interact.performed += Interact;
    }
    private void Interact(InputAction.CallbackContext value)
    {
        OnInteractInput.Invoke();
    }
    private void RemoveInputListeners()
    {
        input.Disable();
        input.Player.WASD.performed -= OnMovementPerformed;
        input.Player.WASD.canceled -= OnMovementCancelled;
        input.Player.Interact.performed -= Interact;

    }
    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
    }
    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
    }
    public Vector3 GetDirection()
    {
        Vector3 direction = new Vector3(moveVector.x, 0, moveVector.y);
        return direction;
    }
}
