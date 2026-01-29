using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/ItemData")]
public class ItemDataSO : ScriptableObject
{
    public List<ItemData> ItemDatasList = new();

    [System.Serializable]
    public class ItemData
    {
        [SerializeField] private int itemID;
        [SerializeField] private string itemName;
        [TextArea]
        [SerializeField] private string effect;
        [SerializeField] private ItemRarity rarity;

        [Header("Heal")]
        [SerializeField] private int healAmount;

        [Header("Buff")]
        [SerializeField] private int attackBuffAmount;
        [SerializeField] private float buffDuration;

        public int ItemID => itemID;
        public string ItemName => itemName;
        public string Effect => effect;
        public ItemRarity Rarity => rarity;

        public int HealAmount => healAmount;
        public int AttackBuffAmount => attackBuffAmount;
        public float BuffDuration => buffDuration;
    }


    public enum ItemRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }


    /// <summary>
    /// ItemID ‚©‚ç ItemData ‚ðŽæ“¾
    /// </summary>
    public ItemData GetItemByID(int id)
    {
        return ItemDatasList.Find(item => item.ItemID == id);
    }
}
