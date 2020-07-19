using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    public float walkMovementSpeed;
    public float attackMovementSpeed;
    public float xMin, xMax, zMin, zMax;
    public bool isGuarding;

    //public GameObject attackChopHitBox, attackGutPunchHitBox;
    //public Sprite attackChopHitFrame, attackGutPunchHitFrame;

    private float movementSpeed;
    private bool facingRight;
    private new Rigidbody rigidbody;
    private Animator animator;
    private AnimatorStateInfo currentStateInfo;
    private SpriteRenderer currentSprite;

    static int currentState;
    static int idleState = Animator.StringToHash("Base Layer.Idle");
    static int walkState = Animator.StringToHash("Base Layer.Walk");
    static int jumpState = Animator.StringToHash("Base Layer.Jump");
    static int punch0State = Animator.StringToHash("Base Layer.Punch0");
    static int punch1State = Animator.StringToHash("Base Layer.Punch1");
    static int punch2State = Animator.StringToHash("Base Layer.Punch2");

    // Start is called before the first frame update
    void Start()
    {
        currentSprite = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        movementSpeed = walkMovementSpeed;
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        currentState = currentStateInfo.fullPathHash;

        if (currentState == idleState)
            Debug.Log("idle");
        if (currentState == walkState)
            Debug.Log("walk");
        if (currentState == jumpState)
            Debug.Log("jump");
        if (currentState == punch0State)
            Debug.Log("punch0");
        if (currentState == punch1State)
            Debug.Log("punch1");
        if (currentState == punch2State)
            Debug.Log("punch2");

        // Control Speed Based on Commands
        if (currentState == idleState || currentState == walkState)
            movementSpeed = walkMovementSpeed;
        else
            movementSpeed = attackMovementSpeed;

        // Guard
        if (Input.GetMouseButton(2))
        {
            animator.SetBool("Guard", true);
            isGuarding = true;
        }
        else
        {
            animator.SetBool("Guard", false);
            isGuarding = false;
        }

        // Hit stun
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // hit animation code
            animator.SetBool("IsHit", true);
        } else
        {
            animator.SetBool("IsHit", false);
        }
    }

    // Not effected by frame rate
    private void FixedUpdate()
    {
        // Movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rigidbody.velocity = movement * movementSpeed;
        rigidbody.position = new Vector3(
            Mathf.Clamp(rigidbody.position.x, xMin, xMax),
            transform.position.y,
            Mathf.Clamp(rigidbody.position.z, zMin, zMax)
        );

        if (moveHorizontal > 0 && !facingRight)
            Flip();
        else if (moveHorizontal < 0 && facingRight)
            Flip();

        animator.SetFloat("Speed", rigidbody.velocity.sqrMagnitude);

        // Combo Attack
        if (Input.GetMouseButton(0))
            animator.SetBool("Attack", true);
        else
            animator.SetBool("Attack", false);

        //if (attackChopHitFrame == currentSprite.sprite)
        //{
        //    attackChopHitBox.gameObject.SetActive(true);
        //}
        //else if (attackGutPunchHitFrame == currentSprite.sprite)
        //{
        //    attackGutPunchHitBox.gameObject.SetActive(true);
        //}
        //else
        //{
        //    attackChopHitBox.gameObject.SetActive(false);
        //    attackGutPunchHitBox.gameObject.SetActive(false);
        //}

    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 thisScale = transform.localScale;
        thisScale.x *= -1;
        transform.localScale = thisScale;
    }
}
