using UnityEngine;

namespace KeySystem
{
    public class KeyItemController : MonoBehaviour
    {
        [SerializeField] private KeyInventory _keyInventory = null;

        [SerializeField] private KeyType keyType = KeyType.None;

        private KeyDoorController doorObject;

        private void Start()
        {
            doorObject = GetComponent<KeyDoorController>();
        }

        public void objectInteraction()
        {
            if (doorObject != null)
            {
                doorObject.PlayAnimation();
            }
            else if (keyType != KeyType.None)
            {
                _keyInventory.AddKey(keyType);
                gameObject.SetActive(false);
            }
        }
    }
}
