using UnityEngine;

namespace RunGun.Gameplay
{
    public class CharacterCanvas : MonoBehaviour
    {
        [SerializeField] private Character _character;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Vector3 _offset;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            //Vector3 characterPosition = _camera.WorldToScreenPoint(_character.transform.position);
            //RectTransformUtility.ScreenPointToWorldPointInRectangle(_rectTransform, characterPosition, _camera, out Vector3 movePosition);
            _rectTransform.position = new Vector3(_character.transform.position.x, _character.transform.position.y, _character.transform.position.z) + _offset;
        }
    }
}