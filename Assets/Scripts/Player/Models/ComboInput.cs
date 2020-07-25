using Assets.Scripts.Player.Enums;
using System;

namespace Assets.Scripts.Player.Models
{
    [Serializable]
    public class ComboInput
    {
        public AttackType attackType;

        public ComboInput(AttackType _attackType)
        {
            attackType = _attackType;
        }

        public bool isSameAs(ComboInput input)
        {
            return attackType == input.attackType;
        }
    }
}
