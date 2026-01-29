using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI tooltipText;
    [SerializeField] private ItemDataSO itemDataSO;
    [SerializeField] private StorageFunction storageFunction;

    private InventorySlotUI slotUI;

    private void Awake()
    {
        slotUI = GetComponent<InventorySlotUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!slotUI || !tooltipText || !itemDataSO || !storageFunction)
            return;

        int slotIndex = slotUI.GetSlotIndex();
        int itemID = storageFunction.GetItem(slotIndex);

        if (itemID == 0)
        {
            tooltipText.text = "";
            return;
        }

        var data = itemDataSO.GetItemByID(itemID);
        tooltipText.text = data != null ? data.Effect : "不明なアイテム";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (tooltipText)
            tooltipText.text = "";
    }
}
