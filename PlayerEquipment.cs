using System;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public event Action OnEquipmentChanged;

    [Header("Equip Slots")]
    public EquipmentInstance Weapon;
    public EquipmentInstance Head;
    public EquipmentInstance Body;
    public EquipmentInstance Legs;

    public EquipmentInstance Accessory1;
    public EquipmentInstance Accessory2;

    // =========================
    // 装備
    // =========================
    public void Equip(
        EquipmentInstance item,
        EquipmentDataSO.EquipmentType type,
        int accessorySlot = 0
    )
    {
        if (item == null)
        {
            Unequip(type, accessorySlot);
            return;
        }

        switch (type)
        {
            case EquipmentDataSO.EquipmentType.Weapon:
                Weapon = item;
                break;

            case EquipmentDataSO.EquipmentType.Head:
                Head = item;
                break;

            case EquipmentDataSO.EquipmentType.Body:
                Body = item;
                break;

            case EquipmentDataSO.EquipmentType.Legs:
                Legs = item;
                break;

            case EquipmentDataSO.EquipmentType.Accessory:
                EquipAccessory(item, accessorySlot);
                break;
        }

        OnEquipmentChanged?.Invoke();
    }

    // =========================
    // アクセサリー専用（重複禁止）
    // =========================
    private void EquipAccessory(EquipmentInstance item, int slot)
    {
        // 🔒 すでに反対側に装備されていたら外す
        if (slot == 1 && Accessory2 == item)
            Accessory2 = null;

        if (slot == 2 && Accessory1 == item)
            Accessory1 = null;

        // 装備
        if (slot == 1)
            Accessory1 = item;
        else if (slot == 2)
            Accessory2 = item;
    }

    // =========================
    // 解除
    // =========================
    public void Unequip(
        EquipmentDataSO.EquipmentType type,
        int accessorySlot = 0
    )
    {
        switch (type)
        {
            case EquipmentDataSO.EquipmentType.Weapon:
                Weapon = null;
                break;

            case EquipmentDataSO.EquipmentType.Head:
                Head = null;
                break;

            case EquipmentDataSO.EquipmentType.Body:
                Body = null;
                break;

            case EquipmentDataSO.EquipmentType.Legs:
                Legs = null;
                break;

            case EquipmentDataSO.EquipmentType.Accessory:
                if (accessorySlot == 1)
                    Accessory1 = null;
                else if (accessorySlot == 2)
                    Accessory2 = null;
                break;
        }

        OnEquipmentChanged?.Invoke();
    }

    // =========================
    // 取得
    // =========================
    public EquipmentInstance GetAccessory(int slot)
    {
        return slot == 1 ? Accessory1 :
               slot == 2 ? Accessory2 : null;
    }

    public EquipmentInstance[] GetAll()
    {
        return new[]
        {
            Weapon,
            Head,
            Body,
            Legs,
            Accessory1,
            Accessory2
        };
    }
}
