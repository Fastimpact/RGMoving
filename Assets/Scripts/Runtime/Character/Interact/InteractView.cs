using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RunGun.Gameplay
{
    public class InteractView : MonoBehaviour, IInteractView
    {
        [SerializeField] private GameObject _interactPanel;
        [SerializeField] private Image _itemImage;
        [SerializeField] private TMP_Text _panelText;

        private bool _isShowed = false;

        public bool IsShowed => _isShowed;

        public void Show(IInteractable item)
        {
            _isShowed = true;
            _itemImage.sprite = item.Icon;
            _panelText.text = item.Name;
            _interactPanel.SetActive(true);
        }
        public void Hide()
        {
            _isShowed = false;
            _interactPanel.SetActive(false);
        }
    }
}
