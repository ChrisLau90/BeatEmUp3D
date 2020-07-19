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
        public bool isAttacking;

        private void Start()
        {
            isAttacking = false;
        }

        private void Update()
        {
            if (Input.GetButton("LightAttack"))
                isAttacking = true;
            else
                isAttacking = false;
        }
    }
}
