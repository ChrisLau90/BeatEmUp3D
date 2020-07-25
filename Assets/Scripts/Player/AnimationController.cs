using UnityEngine;

namespace Assets.Scripts
{
    public class AnimationController : MonoBehaviour
    {
        private Animator animator;
        private AnimatorStateInfo currentStateInfo;
        private new Rigidbody rigidbody;
        //private AttackController attackController;

        static int currentState;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            //attackController = GetComponent<AttackController>();
        }

        void Update()
        {
            currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            currentState = currentStateInfo.fullPathHash;
        }

        private void FixedUpdate()
        {
            animator.SetFloat("Speed", rigidbody.velocity.sqrMagnitude);
            //animator.SetBool("Attack", attackController.isAttacking);
        }
    }
}
