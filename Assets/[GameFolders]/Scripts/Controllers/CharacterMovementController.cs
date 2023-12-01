using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    private CharacterAnimationController animController;
    public CharacterAnimationController AnimController { get { return (animController == null) ? animController = GetComponent<CharacterAnimationController>() : animController; } }
    private Rigidbody rb = null;
    public float maxSpeed;
    [SerializeField]
    private float currentSpeed;
    bool canMove;
    const float ACCELERATION = 10;
    const float ROTATE_SPEED = 720;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Move()
    {
        Vector3 moveDirection = InputManager.Instance.GetDirection();
        float targetSpeed = maxSpeed;
        if (moveDirection == Vector3.zero)
            targetSpeed = 0;

        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * ACCELERATION);

        if (Mathf.Abs(currentSpeed) < 0.1f)
            currentSpeed = 0;
        if (Mathf.Abs(currentSpeed) > 4.9f)
            currentSpeed = 5;

        rb.velocity = moveDirection * currentSpeed;
        AnimController.SetSpeed(currentSpeed, maxSpeed);
        RotateTowards();
    }
    private void OnEnable()
    {
        LevelManager.Instance.OnLevelStart.AddListener(() => canMove = true);
    }
    private void OnDisable()
    {
        LevelManager.Instance.OnLevelStart.RemoveListener(() => canMove = true);
    }
    private void Update()
    {
        if(canMove)
            Move();
    }
    public void MoveEnd()
    {
        rb.velocity = Vector3.zero;
        AnimController.SetSpeed(currentSpeed, maxSpeed);
    }
    public void RotateTowards()
    {
        Vector3 rotateDirection = InputManager.Instance.GetDirection();
        if (rotateDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(rotateDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * ROTATE_SPEED);
        }
    }
    public void SetSpeed(int speed)
    {
        maxSpeed = speed;
    }
    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
}
