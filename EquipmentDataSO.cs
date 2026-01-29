using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEquipmentData", menuName = "Item/EquipmentData")]
public class EquipmentDataSO : ScriptableObject
{
    [SerializeField]
    private List<EquipmentData> equipmentList = new();

    private Dictionary<int, EquipmentData> equipmentDict;

    public IReadOnlyList<EquipmentData> EquipmentList => equipmentList;

    private void OnEnable()
    {
        BuildDictionary();
    }

    private void OnValidate()
    {
        BuildDictionary();
    }

    private void BuildDictionary()
    {
        equipmentDict = new Dictionary<int, EquipmentData>();

        foreach (var data in equipmentList)
        {
            if (data == null) continue;

            if (!equipmentDict.ContainsKey(data.EquipmentID))
            {
                equipmentDict.Add(data.EquipmentID, data);
            }
            else
            {
                Debug.LogWarning($"装備IDが重複しています: {data.EquipmentID}", this);
            }
        }
    }

    // =========================
    // 装備マスタ
    // =========================
    [System.Serializable]
    public class EquipmentData
    {
        [Header("Basic")]
        [SerializeField] private int equipmentID;
        [SerializeField] private string equipmentName;

        [TextArea]
        [SerializeField] private string description;

        [Header("Type")]
        [SerializeField] private EquipmentType type;

        [Header("Base Status")]
        [SerializeField] private int attack;
        [SerializeField] private int defense;
        [SerializeField] private int hp;
        [SerializeField] private int mp;
        [SerializeField] private float criticalRate;
        [SerializeField] private float criticalDamage;
        [SerializeField] private float evadeRate;

        // ===== Getter =====
        public int EquipmentID => equipmentID;
        public string EquipmentName => equipmentName;
        public string Description => description;
        public EquipmentType Type => type;

        public int Attack => attack;
        public int Defense => defense;
        public int HP => hp;
        public int MP => mp;
        public float CriticalRate => criticalRate;
        public float CriticalDamage => criticalDamage;
        public float EvadeRate => evadeRate;
    }

    public enum EquipmentType
    {
        Weapon,
        Head,
        Body,
        Legs,
        Accessory
    }

    /// <summary>
    /// EquipmentID から装備データ取得（安全）
    /// </summary>
    public EquipmentData GetEquipmentByID(int id)
    {
        if (equipmentDict == null)
            BuildDictionary();

        equipmentDict.TryGetValue(id, out var data);
        return data;
    }
}
