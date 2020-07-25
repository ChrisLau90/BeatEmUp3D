using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    //Rename this
    public class AttackController : MonoBehaviour
    {
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetButtonDown("LightAttack"))
            {
                LightAttack();
            }
        }

        private void LightAttack()
        {
            animator.SetTrigger("LightAttack");
        }
    }
}
