using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentInventory : MonoBehaviour
{
    public event Action OnInventoryChanged;

    [SerializeField] private List<EquipmentInstance> equipments = new();

    public IReadOnlyList<EquipmentInstance> Equipments => equipments;

    public void AddEquipment(EquipmentInstance instance)
    {
        equipments.Add(instance);
        OnInventoryChanged?.Invoke();
    }

    public void RemoveEquipment(EquipmentInstance instance)
    {
        equipments.Remove(instance);
        OnInventoryChanged?.Invoke();
    }
}
