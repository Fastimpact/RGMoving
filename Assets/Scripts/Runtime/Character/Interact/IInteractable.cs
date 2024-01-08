using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RunGun.Gameplay
{
    public interface IInteractable
    {
        string Name { get; }
        Sprite Icon { get; }
        UnityEvent OnInteract { get; }
        void Interact();
    }
}
