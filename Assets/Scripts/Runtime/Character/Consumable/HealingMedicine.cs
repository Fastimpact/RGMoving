using System;
using UnityEngine;

namespace RunGun.Gameplay
{
    [CreateAssetMenu(fileName = "HealthPotion", menuName = "Consumables/HealthPotion")]
    public class HealingMedicine : ScriptableObject, IConsumable
    {
        [SerializeField] private int _healAmount;
        [SerializeField] private int _maxUses;
        [SerializeField] private int _styleThreshold;

        private IHealth _characterHealth;
        private IStyle _style;

        private int _remainingUses;

        public void Initialize(IHealth characterHealth, IStyle style)
        {
            _characterHealth = characterHealth ?? throw new ArgumentNullException(nameof(characterHealth));
            _style = style ?? throw new ArgumentNullException(nameof(style));
            _remainingUses = _maxUses;
        }

        public void Consume()
        {
            if (_remainingUses > 0 && _style.Value >= _styleThreshold)
            {
                if(_characterHealth.Value < _characterHealth.MaxValue)
                    _characterHealth.Heal(_healAmount);

                _remainingUses--;

                if (_remainingUses == 0 && _style.Value >= _styleThreshold)
                {
                    _style.SpendStyle(4);
                    _remainingUses = 1;
                    Debug.Log("�������� 1 ����� � ����� �� ������� 1 ��");
                }
                Debug.Log(_characterHealth.Value);
            }
            else
            {
                Debug.Log("����� ��� ���� ��������� ������������.");
            }
        }

        public int GetRemainingUses()
        {
            return _remainingUses;
        }

        public void SetRemainingUses(int uses)
        {
            _remainingUses = uses;
        }
    }

}
