using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunGun.Gameplay
{
    public interface IInteractView
    {
        void Show(IInteractable item);
    }
}
