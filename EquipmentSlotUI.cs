using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EquipmentDropdownUI : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private EquipmentDataSO equipmentDataSO;
    [SerializeField] private PlayerEquipmentInventory inventory;
    [SerializeField] private EquipmentDataSO.EquipmentType targetType;
    [SerializeField] private PlayerEquipment playerEquipment;

    [Header("Accessory Only")]
    [SerializeField] private int accessorySlot = 0; // 1 or 2（Accessory用）

    private readonly List<EquipmentInstance> list = new();
    private bool isRefreshing;

    private void OnEnable()
    {
        inventory.OnInventoryChanged += Refresh;

        if (playerEquipment != null)
            playerEquipment.OnEquipmentChanged += Refresh; 

        Refresh();
    }

    private void OnDisable()
    {
        inventory.OnInventoryChanged -= Refresh;

        if (playerEquipment != null)
            playerEquipment.OnEquipmentChanged -= Refresh; 
    }


    // ================================
    // Dropdown Callback
    // ================================
    public void OnValueChanged(int index)
    {
        if (isRefreshing) return;
        if (playerEquipment == null) return;

        if (index == 0)
        {
            playerEquipment.Unequip(targetType, accessorySlot);
            return;
        }

        if (index - 1 < 0 || index - 1 >= list.Count) return;

        playerEquipment.Equip(list[index - 1], targetType, accessorySlot);
    }

    // ================================
    // Refresh
    // ================================
    public void Refresh()
    {
        isRefreshing = true;

        dropdown.ClearOptions();
        list.Clear();

        List<string> options = new() { "未装備" };

        foreach (var inst in inventory.Equipments)
        {
            if (inst == null) continue;

            var data = equipmentDataSO.GetEquipmentByID(inst.masterID);
            if (data == null || data.Type != targetType) continue;

            list.Add(inst);
            options.Add($"{data.EquipmentName} +{inst.addAttack}");
        }

        dropdown.AddOptions(options);

        // 🔑 現在装備している装備を反映
        EquipmentInstance equipped = GetEquipped();
        int index = 0;

        if (equipped != null)
        {
            int found = list.IndexOf(equipped);
            if (found >= 0)
                index = found + 1;
        }

        dropdown.SetValueWithoutNotify(index);
        isRefreshing = false;
    }

    // ================================
    // 現在装備取得
    // ================================
    private EquipmentInstance GetEquipped()
    {
        return targetType switch
        {
            EquipmentDataSO.EquipmentType.Weapon => playerEquipment.Weapon,
            EquipmentDataSO.EquipmentType.Head => playerEquipment.Head,
            EquipmentDataSO.EquipmentType.Body => playerEquipment.Body,
            EquipmentDataSO.EquipmentType.Legs => playerEquipment.Legs,
            EquipmentDataSO.EquipmentType.Accessory =>
                playerEquipment.GetAccessory(accessorySlot),
            _ => null
        };
    }

}
