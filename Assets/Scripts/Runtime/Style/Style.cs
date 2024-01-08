using System;

namespace RunGun.Gameplay
{
    public class Style : IStyle
    {
        private readonly CharacterStyleView _view;
        private readonly int _styleMax;

        public Style(CharacterStyleView view, int style_max)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _styleMax = style_max;
            _view.Show(Value, MaxValue);
        }

        public bool HasStyle => Value > 0;

        public float Value { get; private set; } = 20;

        public float MaxValue => _styleMax;

        public void IncreaseStyle(float value)
        {
            if (Value + value > MaxValue)
            {
                Value = MaxValue;
            }
            else
            {
                Value += value;
            }

            _view.Show(Value, MaxValue);
        }

        public void SpendStyle(float styleCost)
        {
            if (Value - styleCost < 0)
                return;

            Value -= styleCost;
            _view.Show(Value, MaxValue);
        }
    }
}
