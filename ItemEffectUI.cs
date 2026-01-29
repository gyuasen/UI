using UnityEngine;
using TMPro;
using System.Collections;

public class ItemEffectUI : MonoBehaviour
{
    [SerializeField] private ItemUseProcessor itemUseProcessor;
    [SerializeField] private TextMeshProUGUI effectText;
    [SerializeField] private float displayTime = 2f;

    private Coroutine currentRoutine;

    private void OnEnable()
    {
        itemUseProcessor.OnItemEffectMessage += ShowMessage;
    }

    private void OnDisable()
    {
        itemUseProcessor.OnItemEffectMessage -= ShowMessage;
    }

    private void ShowMessage(string message)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(ShowRoutine(message));
    }

    private IEnumerator ShowRoutine(string message)
    {
        effectText.text = message;
        effectText.gameObject.SetActive(true);

        yield return new WaitForSeconds(displayTime);

        effectText.gameObject.SetActive(false);
        currentRoutine = null;
    }
}
