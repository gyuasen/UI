using System;
using UnityEngine;

/// <summary>
/// 装備1個分の「個体」データ（ハクスラ用）
/// </summary>
[Serializable]
public class EquipmentInstance
{
    // ===== Master =====
    public int masterID;   // EquipmentDataSO の EquipmentID
    public int instanceID; // 個体ID（ユニーク）

    // ===== Random Bonus =====
    public int addAttack;
    public int addDefense;
    public int addHP;
    public int addMP;
    public float addCritical;
    public float addCriticalDamage;
    public float addEvasion;

    // ===== Constructor =====
    public EquipmentInstance(int masterID)
    {
        this.masterID = masterID;
        instanceID = Guid.NewGuid().GetHashCode();
        GenerateRandomBonus();
    }

    // ===== Randomize =====
    private void GenerateRandomBonus()
    {
        addAttack = UnityEngine.Random.Range(0, 6);
        addDefense = UnityEngine.Random.Range(0, 5);
        addHP = UnityEngine.Random.Range(0, 21);
        addMP = UnityEngine.Random.Range(0, 11);

        addCritical = UnityEngine.Random.Range(0f, 0.03f);
        addCriticalDamage = UnityEngine.Random.Range(0f, 0.10f);
        addEvasion = UnityEngine.Random.Range(0f, 0.02f);
    }

    // ===== SO参照 =====
    public EquipmentDataSO.EquipmentData GetMaster(EquipmentDataSO so)
    {
        return so != null ? so.GetEquipmentByID(masterID) : null;
    }

    // ===== 合算ステータス =====
    public int TotalAttack(EquipmentDataSO so)
    {
        var m = GetMaster(so);
        return (m != null ? m.Attack : 0) + addAttack;
    }

    public int TotalDefense(EquipmentDataSO so)
    {
        var m = GetMaster(so);
        return (m != null ? m.Defense : 0) + addDefense;
    }

    public int TotalHP(EquipmentDataSO so)
    {
        var m = GetMaster(so);
        return (m != null ? m.HP : 0) + addHP;
    }

    public int TotalMP(EquipmentDataSO so)
    {
        var m = GetMaster(so);
        return (m != null ? m.MP : 0) + addMP;
    }

    public float TotalCritical(EquipmentDataSO so)
    {
        var m = GetMaster(so);
        return (m != null ? m.CriticalRate : 0f) + addCritical;
    }

    public float TotalCriticalDamage(EquipmentDataSO so)
    {
        var m = GetMaster(so);
        return (m != null ? m.CriticalDamage : 0f) + addCriticalDamage;
    }

    public float TotalEvasion(EquipmentDataSO so)
    {
        var m = GetMaster(so);
        return (m != null ? m.EvadeRate : 0f) + addEvasion;
    }
}
