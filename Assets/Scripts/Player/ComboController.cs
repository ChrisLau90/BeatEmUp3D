using Assets.Scripts.Player.Enums;
using Assets.Scripts.Player.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;



namespace Assets.Scripts.Player
{
    public class ComboController : MonoBehaviour
    {
        [Header("Inputs")]
        public KeyCode lightKey;
        public KeyCode heavyKey;
        public KeyCode specialKey;

        [Header("Attacks")]
        public Attack lightAttack;
        public Attack heavyAttack;
        public Attack specialAttack;
        public List<Combo> combos;
        public float comboLeeway = 0.2f;

        [Header("Components")]
        public Animator animator;

        Attack currentAttack = null;
        ComboInput lastInput = null;
        List<int> currentCombos = new List<int>();
        
        float attackTimer = 0;
        float leewayTimer = 0;
        bool shouldSkipFrame = false;

        private void Start()
        {
            animator = GetComponent<Animator>();
            PrimeCombos();
        }

        private void PrimeCombos()
        {
            for (int i = 0; i < combos.Count; i++)
            {
                Combo combo = combos[i];
                combo.onInput.AddListener(() =>
                {
                    shouldSkipFrame = true;
                    Attack(combo.comboAttack);
                    ResetCombos();
                });
            }
        }

        private void Update()
        {
            if (currentAttack != null)
            {
                if (attackTimer > 0)
                    attackTimer -= Time.deltaTime;
                else
                    currentAttack = null;
                return;
            }

            if (currentCombos.Count > 0)
            {
                leewayTimer += Time.deltaTime;
                if (leewayTimer >= comboLeeway)
                {
                    if (lastInput != null)
                    {
                        Attack(GetAttackFromType(lastInput.attackType));
                        lastInput = null;
                    }

                    ResetCombos();
                }
            }
            else
            {
                leewayTimer = 0;
            }

            ComboInput currentInput = null;
            if (Input.GetKeyDown(lightKey))
                currentInput = new ComboInput(AttackType.Light);
            if (Input.GetKeyDown(heavyKey))
                currentInput = new ComboInput(AttackType.Heavy);
            if (Input.GetKeyDown(specialKey))
                currentInput = new ComboInput(AttackType.Special);

            if (currentInput == null)
                return;

            lastInput = currentInput;

            List<int> removeList = new List<int>();
            for (int i = 0; i < currentCombos.Count; i++)
            {
                Combo combo = combos[currentCombos[i]];
                if (combo.ShouldContinueCombo(currentInput))
                {
                    leewayTimer = 0;
                } 
                else
                {
                    removeList.Add(i);
                }
            }

            if (shouldSkipFrame)
            {
                //to prevent problems with update loop when calling ResetCombos()
                shouldSkipFrame = false;
                return;
            }

            for (int i = 0; i < combos.Count; i++) 
            {
                if (currentCombos.Contains(i))
                    continue;
                
                if (combos[i].ShouldContinueCombo(currentInput))
                {
                    currentCombos.Add(i);
                    leewayTimer = 0;
                }
            }

            foreach (int i in removeList)
            {
                currentCombos.RemoveAt(i);
            }

            if (currentCombos.Count <= 0)
            {
                Attack(GetAttackFromType(currentInput.attackType));
            }
        }

        private void ResetCombos()
        {
            leewayTimer = 0;
            for (int i = 0; i < currentCombos.Count; i++)
            {
                Combo combo = combos[currentCombos[i]];
                combo.ResetCombo();
            }

            currentCombos.Clear();
        }

        private void Attack(Attack attack)
        {
            currentAttack = attack;
            attackTimer = attack.length;
            animator.Play(attack.name, -1, 0);
        }

        private Attack GetAttackFromType(AttackType type)
        {
            if (type == AttackType.Light)
                return lightAttack;
            if (type == AttackType.Heavy)
                return heavyAttack;
            if (type == AttackType.Special)
                return specialAttack;
            return null;
        }
    }
}

