using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunGun.Gameplay
{
    [RequireComponent(typeof(SphereCollider))]
    public class InteractSensor : MonoBehaviour
    {
        [SerializeField] private InteractView _interactView;
        [SerializeField] private Character _character;
        [SerializeField] private float _detectRadius;

        private SphereCollider _sensor;

        private IInteractable _interactableObject;
        private Transform _intreactObjectTransform;

        public bool IsInteract { get; private set; }
        public IInteractable InteractableObject => _interactableObject;
        public Transform IntreactObjectTransform => _intreactObjectTransform;

        private void Start()
        {
            _sensor = GetComponent<SphereCollider>();
            _sensor.center = _character.GetComponent<BoxCollider>().center;
            _sensor.radius = _detectRadius;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IInteractable interactable))
            {
                IsInteract = true;
                _interactableObject = interactable;
                _intreactObjectTransform = other.transform;
                _interactView.Show(InteractableObject);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IInteractable interactable))
            {
                IsInteract = false;
                _interactView.Hide();
            }
                
        }
    }
}
