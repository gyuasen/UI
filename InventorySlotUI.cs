using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private ItemDataSO itemDataSO;
    [SerializeField] private StorageFunction storageFunction;

    private int slotIndex;

    public void SetSlotIndex(int index)
    {
        slotIndex = index;
        Refresh();
    }

    public int GetSlotIndex()
    {
        return slotIndex;
    }

    public void Clear()
    {
        buttonText.text = "";
    }

    public void Refresh()
    {
        int itemID = storageFunction.GetItem(slotIndex);

        if (itemID == 0)
        {
            Clear();
            return;
        }

        var data = itemDataSO.GetItemByID(itemID);
        buttonText.text = data != null ? data.ItemName : "ïsñæ";
    }

    // Åö É{É^ÉìÇ©ÇÁåƒÇŒÇÍÇÈ
    public void OnClickUse()
    {
        storageFunction.UseItem(slotIndex);
    }
}