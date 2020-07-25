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

        public Transform lightAttackBox;
        public LayerMask enemyLayers;

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
            //play animation - MOVE TO ANIMATIONCONTROLLER?
            animator.SetTrigger("LightAttack");

            //detect enimies within range of attack
            var hitEnemies = Physics.OverlapBox(
                lightAttackBox.transform.position, 
                lightAttackBox.localScale,
                Quaternion.identity, 
                enemyLayers
            );

            //damage them
            foreach(var enemy in hitEnemies)
            {
                Debug.Log(enemy.name + " attacked");
            }
        }

        //private void OnDrawGizmosSelected()
        //{
        //    if (lightAttackBox == null)
        //        return;

        //    Gizmos.DrawWireCube(
        //        lightAttackBox.transform.position,
        //        lightAttackBox.localScale / 2
        //    );
        //}
    }
}
