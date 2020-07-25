using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace Assets.Scripts.Player.Models
{
    [Serializable]
    public class Combo
    {
        public List<ComboInput> inputs;
        public Attack comboAttack;
        public UnityEvent onInput;
        int currentInput = 0;

        public bool ShouldContinueCombo(ComboInput input)
        {
            if (inputs[currentInput].isSameAs(input))
            {
                currentInput++;
                if (currentInput >= inputs.Count) //finished the inputs and we should do the attack
                {
                    onInput.Invoke();
                    currentInput = 0;
                }
                return true;
            }
            else
            {
                currentInput = 0;
                return false;
            }
        }

        public ComboInput GetCurrentComboInput()
        {
            if (currentInput >= inputs.Count)
                return null;
            return inputs[currentInput];
        }

        public void ResetCombo()
        {
            currentInput = 0;
        }
    }
}
