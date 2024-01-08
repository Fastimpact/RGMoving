using UnityEngine;

namespace RunGun.Gameplay
{
    public class CharacterCamera : MonoBehaviour
    {
        [SerializeField] private Character _character;

        private void Start()
        {
            transform.SetParent(null, true);
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        private void Update()
        {
            Vector3 characterPosition = _character.transform.position;
            transform.position = new Vector3(characterPosition.x, transform.position.y, characterPosition.z);
        }
    }
}
