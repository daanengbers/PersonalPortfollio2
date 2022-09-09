using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    [SerializeField] private LayerMask dashLayerMask;

    private enum State
    {
        normal,
        rolling,
    }

    private Vector3 moveDir;
    private Vector3 rollDir;
    private float rollSpeed;
    private bool isDashButtonDown;
    private State state;

    Vector2 movement;

    private void Awake()
    {
        state = State.normal;
    }

    private void Update()
    {
        switch (state)
        {
            case State.normal:
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");

                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
                animator.SetFloat("Speed", movement.sqrMagnitude);

                moveDir = new Vector3(movement.x, movement.y).normalized;

                Debug.Log(moveDir);

                HandleDodgeRoll();
                HandleDash();

                break;
            case State.rolling:
                float rollSpeedDropMultiplier = 3f;
                rollSpeed -= rollSpeed * rollSpeedDropMultiplier * Time.deltaTime;

                float rollSpeedMinimun = 5f;
                if (rollSpeed < rollSpeedMinimun)
                {
                    state = State.normal;
                }
                break;

        }
    }

        private void FixedUpdate()
        {
            switch (state) 
            {
                case State.normal:
                animator.SetBool("Rolling", false);


                rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);


                //Dash
        if (isDashButtonDown == true) {
            float dashAmount = 5f;
            Vector3 dashPosition = transform.position + moveDir * dashAmount;

            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, moveDir, dashAmount, dashLayerMask);
            if (raycastHit2D.collider != null)
            {
                dashPosition = raycastHit2D.point;
            }
            rb.MovePosition(dashPosition);
            isDashButtonDown = false;
        }
               //end Dash

                    break;
                case State.rolling:
                    rb.velocity = rollDir * rollSpeed;
                animator.SetFloat("Vertical", movement.y);
                animator.SetBool("Rolling", true);
                    break;
            }
            

    }

    private void HandleDodgeRoll() {
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rollDir = moveDir;
                    rollSpeed = 50f;
                    state = State.rolling;
                }
            }
        }

    private void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.F)) {
            isDashButtonDown = true;
        }
    }

}
