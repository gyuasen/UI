using UnityEngine;
using System;

public class ItemUseProcessor : MonoBehaviour
{
    [SerializeField] private ItemDataSO itemDataSO;
    [SerializeField] private PlayerStatus playerStatus;

    // ★ UIやログ用
    public event Action<string> OnItemEffectMessage;

    public bool UseItem(int itemID)
    {
        if (itemID == 0) return false;

        var data = itemDataSO.GetItemByID(itemID);
        if (data == null) return false;

        return ApplyEffect(data);
    }

    private bool ApplyEffect(ItemDataSO.ItemData data)
    {
        // ===== 使用条件：HP満タンなら不可 =====
        if (data.HealAmount > 0)
        {
            if (playerStatus.CurrentHP >= playerStatus.MaxHP)
            {
                SendMessage($"HPはすでに満タンです");
                return false;
            }

            playerStatus.HealHP(data.HealAmount);

            // ★ 効果文を送信
            SendMessage(data.Effect);
            return true;
        }

        return false;
    }

    private void SendMessage(string message)
    {
        Debug.Log($"[ItemEffect] {message}");
        OnItemEffectMessage?.Invoke(message);
    }
}
