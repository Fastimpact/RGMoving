using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunGun.Gameplay
{
    [RequireComponent(typeof(Camera))]
    public class BlendCamera : MonoBehaviour
    {
        [SerializeField] private RenderTexture _cameraTexture;
        private void Awake()
        {
            _cameraTexture.height = Screen.height;
            _cameraTexture.width = Screen.width;
            _cameraTexture.volumeDepth = 8;

            Camera camera = GetComponent<Camera>();
            camera.targetTexture = _cameraTexture;
        }
    }
}
