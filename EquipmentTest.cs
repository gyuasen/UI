using UnityEngine;

/// <summary>
/// 装備ランダム取得テスト用
/// </summary>
public class RandomEquipmentTest : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private EquipmentDataSO equipmentDataSO;
    [SerializeField] private PlayerEquipmentInventory inventory;

    [Header("Test")]
    [SerializeField] private int gainCount = 3; // 1回で何個取得するか

    [ContextMenu("Random Gain Equipment")]
    public void GainRandomEquipment()
    {
        if (equipmentDataSO == null || inventory == null)
        {
            Debug.LogError("参照が設定されていません");
            return;
        }

        for (int i = 0; i < gainCount; i++)
        {
            var master = GetRandomMaster();
            if (master == null) continue;

            var instance = new EquipmentInstance(master.EquipmentID);
            inventory.AddEquipment(instance);

            Debug.Log(
                $"装備獲得: {master.EquipmentName} " +
                $"[ATK+{instance.addAttack} DEF+{instance.addDefense} " +
                $"HP+{instance.addHP} MP+{instance.addMP}]"
            );
        }
    }

    /// <summary>
    /// EquipmentDataSO からランダムに1つ取得
    /// </summary>
    private EquipmentDataSO.EquipmentData GetRandomMaster()
    {
        var list = equipmentDataSO.EquipmentList;
        if (list == null || list.Count == 0) return null;

        int index = Random.Range(0, list.Count);
        return list[index];
    }
}
