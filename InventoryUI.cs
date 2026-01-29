using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private StorageFunction storage;
    [SerializeField] private InventorySlotUI[] slots;
    [SerializeField] private int itemsPerPage = 8;

    [Header("Page Buttons")]
    [SerializeField] private Button prevButton;
    [SerializeField] private Button nextButton;

    private int currentPage = 0;

    private void Start()
    {
        Refresh();
        storage.OnInventoryChanged += Refresh;
    }

    private void OnDestroy()
    {
        storage.OnInventoryChanged -= Refresh;
    }

    // ▶ 次ページ
    public void NextPage()
    {
        currentPage = Mathf.Min(currentPage + 1, GetMaxPage());
        Refresh();
    }

    // ◀ 前ページ
    public void PrevPage()
    {
        currentPage = Mathf.Max(currentPage - 1, 0);
        Refresh();
    }

    private void Refresh()
    {
        // 全スロット非表示
        foreach (var slot in slots)
        {
            slot.gameObject.SetActive(false);
            slot.Clear();
        }

        List<int> indices = GetItemIndices();
        int start = currentPage * itemsPerPage;

        for (int i = 0; i < itemsPerPage; i++)
        {
            int idx = start + i;
            if (idx >= indices.Count) break;

            slots[i].gameObject.SetActive(true);
            slots[i].SetSlotIndex(indices[idx]);
        }

        UpdatePageButtons();
    }

    private void UpdatePageButtons()
    {
        int maxPage = GetMaxPage();

        if (prevButton)
            prevButton.interactable = currentPage > 0;

        if (nextButton)
            nextButton.interactable = currentPage < maxPage;
    }

    private int GetMaxPage()
    {
        int itemCount = GetItemIndices().Count;
        return Mathf.Max(0, (itemCount - 1) / itemsPerPage);
    }

    // ★ 所持中アイテムの Storage index 一覧
    private List<int> GetItemIndices()
    {
        List<int> list = new();

        for (int i = 0; i < storage.SlotCount; i++)
        {
            if (storage.GetItem(i) != 0)
                list.Add(i);
        }
        return list;
    }
}
