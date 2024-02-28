using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeySystem
{
    public enum KeyType
    {
        None,
        RustyKey,
        GoldenKey,
        RedKey,
        BlueKey,
        GreenKey,
        ChainsawKey,
        PurpleKey
    }


    public class KeyInventory : MonoBehaviour
    {
        public HashSet<KeyType> collectedKeys = new HashSet<KeyType>();

        public bool HasKey(KeyType keyType)
        {
            return collectedKeys.Contains(keyType);
        }

        //add key to the inventory.
        public void AddKey(KeyType keyType)
        {
            collectedKeys.Add(keyType);
        }
    }
}
