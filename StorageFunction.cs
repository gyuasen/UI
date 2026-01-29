using System;
using UnityEngine;

public class StorageFunction : MonoBehaviour
{
    [SerializeField] private int[] itemFunction;
    [SerializeField] private ItemUseProcessor itemUseProcessor;

    public event Action OnInventoryChanged;
    public int SlotCount => itemFunction.Length;

    private void Start()
    {
        ClearAll();
    }

    public void ClearAll()
    {
        for (int i = 0; i < itemFunction.Length; i++)
            itemFunction[i] = 0;

        OnInventoryChanged?.Invoke();
    }

    public int GetItem(int index)
    {
        if (index < 0 || index >= itemFunction.Length)
            return 0;
        return itemFunction[index];
    }

    public void UseItem(int index)
    {
        if (index < 0 || index >= itemFunction.Length)
            return;

        int itemID = itemFunction[index];
        if (itemID == 0)
            return;

        // 🔴 ここが NullReference の原因
        if (itemUseProcessor == null)
        {
            Debug.LogError("ItemUseProcessor が設定されていません");
            return;
        }

        // ★ 使用できたか？
        bool used = itemUseProcessor.UseItem(itemID);

        if (!used)
            return; // 使用条件NG（HP満タンなど）

        // ★ 成功したら削除
        itemFunction[index] = 0;
        OnInventoryChanged?.Invoke();
    }
    public bool AddItemRandom(int itemID)
    {
        var empty = new System.Collections.Generic.List<int>();

        for (int i = 0; i < itemFunction.Length; i++)
        {
            if (itemFunction[i] == 0)
                empty.Add(i);
        }

        if (empty.Count == 0)
            return false;

        int rand = UnityEngine.Random.Range(0, empty.Count);
        itemFunction[empty[rand]] = itemID;

        OnInventoryChanged?.Invoke();
        return true;
    }
}
