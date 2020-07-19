using UnityEngine;

public class KyokoControllerScript : MonoBehaviour
{
    public float walkMovementSpeed;
    public float attackMovementSpeed;
    public float xMin, xMax, zMin, zMax;

    public GameObject attackChopHitBox;
    public Sprite attackChopHitFrame;

    private float movementSpeed;
    private bool facingRight;
    private Rigidbody rigidbody;
    private Animator animator;
    private AnimatorStateInfo currentStateInfo;
    private SpriteRenderer currentSprite;

    static int currentState;
    static int idleState = Animator.StringToHash("Base Layer.Idle");
    static int walkState = Animator.StringToHash("Base Layer.Walk");
    static int runState = Animator.StringToHash("Base Layer.Run");
    static int jumpState = Animator.StringToHash("Base Layer.Jump");
    static int attack1ChopState = Animator.StringToHash("Base Layer.Attack01Chop");
    static int attack2SnapKickState = Animator.StringToHash("Base Layer.Attack02SnapKick");

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
        if (currentState == runState)
            Debug.Log("run");
        if (currentState == jumpState)
            Debug.Log("jump");
        if (currentState == attack1ChopState)
            Debug.Log("chop");
        if (currentState == attack2SnapKickState)
            Debug.Log("gut punch");
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
        if (Input.GetButton("LightAttack"))
            animator.SetBool("Attack", true);
        else
            animator.SetBool("Attack", false);

        if (attackChopHitFrame == currentSprite.sprite)
        {
            attackChopHitBox.gameObject.SetActive(true);
        }
        else
        {
            attackChopHitBox.gameObject.SetActive(false);
        }

    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 thisScale = transform.localScale;
        thisScale.x *= -1;
        transform.localScale = thisScale;
    }
}
