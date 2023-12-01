using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    #region Params
    public float maxSpeed;

    private CharacterAnimationController animController;
    public CharacterAnimationController AnimController { get { return (animController == null) ? animController = GetComponent<CharacterAnimationController>() : animController; } }
    private float currentSpeed;
    private Rigidbody rb = null;
    private bool canMove;
    const float ACCELERATION = 10;
    const float ROTATE_SPEED = 720;
    #endregion
    #region MonoMethods

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        LevelManager.Instance.OnLevelStart.AddListener(() => canMove = true);
        CharacterHealthController.OnCharacterTakeDamage.AddListener(() => StartCoroutine(HitActionCO()));
        CharacterHealthController.OnCharacterDie.AddListener(() => canMove = false);
    }
    private void OnDisable()
    {
        LevelManager.Instance.OnLevelStart.RemoveListener(() => canMove = true);
        CharacterHealthController.OnCharacterTakeDamage.RemoveListener(() => StartCoroutine(HitActionCO()));
        CharacterHealthController.OnCharacterDie.RemoveListener(() => canMove = false);
    }
    private void Update()
    {
        if (canMove)
            Move();
    }
    #endregion
    #region MovementMethods

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

        rb.velocity = new Vector3(moveDirection.x * currentSpeed, rb.velocity.y, moveDirection.z * currentSpeed);
        AnimController.SetSpeed(currentSpeed, maxSpeed);
        RotateTowards(moveDirection);
    }
    public void RotateTowards(Vector3 rotateDirection)
    {
        if (rotateDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(rotateDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * ROTATE_SPEED);
        }
    }
   
    IEnumerator HitActionCO()
    {
        canMove = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.AddForce(Vector3.back * rb.mass * 5, ForceMode.Impulse);
        yield return new WaitForSeconds(0.5f);

        rb.freezeRotation = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        canMove = true;
    }

    public void MoveEnd()
    {
        rb.velocity = Vector3.zero;
        AnimController.SetSpeed(currentSpeed, maxSpeed);
    }
    #endregion
}
