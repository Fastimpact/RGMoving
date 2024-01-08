using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace RunGun.Gameplay
{
    public class CharacterStyleView : MonoBehaviour ,IStyleView
    {
        [SerializeField] private Image[] _fill;
        [SerializeField] private Image _fillOnStyle;

        [SerializeField] private Image _styleName;

        [SerializeField] private Image[] isStyleLine;

        [SerializeField] private List<Sprite> _styleBarsFilled;
        [SerializeField] private GameObject[] _styleBarsFull;
        [SerializeField] private Image[] _styleBarsFullProcess;
        [SerializeField] private List<Sprite> _styleNames;

        public void Show(float style, float maxStyle)
        {
            for(int i = 0; i < _fill.Length; i++)
            {
                float partStyle = (float)maxStyle * (i + 1) / _fill.Length;
                if (style <= partStyle)
                {
                    ChangeOtherBar(i);
                    CheckStyleLine(i, 1 - (partStyle - style)/(partStyle / (i + 1)));
                    break;
                }
            }
        }

        private void ChangeOtherBar(int indexBar)
        {
            CheckLast(indexBar);
            for (int i = 0; i < indexBar; i++)
            {
                _styleBarsFull[i].SetActive(true);
            }
            for (int i = indexBar; i < _fill.Length; i++)
            {
                _fill[i].fillAmount = 0;
                _fill[i].sprite = _styleBarsFilled[0];
                _styleBarsFull[i].SetActive(false);
            }
        }

        private void CheckStyleLine(int indexBar, float fillStatus)
        {
            _fill[indexBar].fillAmount = fillStatus;
            if (indexBar == 0 && fillStatus != 1)
            {
                isStyleLine[0].enabled = true;
                isStyleLine[1].enabled = false;
                _fillOnStyle.fillAmount = 0;
                _styleName.enabled = false;
            }
            else
            {
                _fillOnStyle.fillAmount = fillStatus;
                if (fillStatus == 1 && (indexBar == 0 || indexBar + 1 == _fill.Length))
                {
                    _styleBarsFull[indexBar].SetActive(true);
                    _styleName.sprite = _styleNames[indexBar];
                    _fillOnStyle.fillAmount = 0;
                }
                else if (fillStatus == 1)
                    _styleBarsFull[indexBar].SetActive(true);
                else
                    _styleName.sprite = _styleNames[indexBar - 1];
                _styleName.enabled = true;
                isStyleLine[1].enabled = true;
                isStyleLine[0].enabled = false;
            }
        }

        private void CheckLast(int indexBar)
        {
            Sprite styleBarsFilled = _styleBarsFilled[0];
            if (indexBar + 1 == _fill.Length)
                styleBarsFilled = _styleBarsFilled[1];

            for (int i = 0; i < _fill.Length; i++)
            {
                _styleBarsFullProcess[i].sprite = styleBarsFilled;
                _fill[i].sprite = styleBarsFilled;
            }
        }
    }
}
