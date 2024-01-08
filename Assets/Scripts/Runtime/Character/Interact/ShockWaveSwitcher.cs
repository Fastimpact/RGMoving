using UnityEngine;
using UnityEngine.Events;

namespace RunGun.Gameplay
{
    public class ShockWaveSwitcher : MonoBehaviour, IInteractable
    {
        [SerializeField] private UnityEvent _onInteract;
        
        [field: SerializeField] public string Name { get; private set; }
     
        [field: SerializeField] public Sprite Icon { get; private set; }
   
        public UnityEvent OnInteract => _onInteract;

        public void Interact()
        {
            Debug.Log("Interact");
        }
    }
}