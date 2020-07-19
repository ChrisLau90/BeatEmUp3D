using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class AnimationScript : MonoBehaviour
    {
        private Animator animator;
        private AnimatorStateInfo currentStateInfo;
        private SpriteRenderer currentSprite;
        private Rigidbody rigidbody;

        static int currentState;

        private void Start()
        {
            currentSprite = GetComponent<SpriteRenderer>();
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
            currentState = currentStateInfo.fullPathHash;
        }

        private void FixedUpdate()
        {
            animator.SetFloat("Speed", rigidbody.velocity.sqrMagnitude);
        }
    }
}
