using UnityEngine;

public class ItemPickupTester : MonoBehaviour
{
    [SerializeField] private StorageFunction storage;
    [SerializeField] private int testItemID = 1; // 薬草などのID

    void Update()
    {
        // Pキーでアイテム獲得
        if (Input.GetKeyDown(KeyCode.P))
        {
            bool success = storage.AddItemRandom(testItemID);

            if (success)
                Debug.Log($"アイテム取得: ID={testItemID}");
            else
                Debug.Log("インベントリが満杯です");
        }
    }
}