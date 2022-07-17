using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class RoundUI : MonoBehaviour
{
    [SerializeField]
    private RoundTracker tracker;

    [SerializeField]
    private GameObject roundUI;
    [SerializeField]
    private TextMeshProUGUI roundText;
    
    [SerializeField]
    private CanvasGroup group;

    [SerializeField]
    private float speed = 1f, margin = 0.01f;

    private Coroutine currentCoroutine;

    public event Action OnCanvasFaded;

    public void ShowUI()
    {
        roundUI.SetActive(true);
        roundText.text = "Round " + tracker.Round;
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(nameof(FadeCanvas), true);
    }

    IEnumerator FadeCanvas(bool fadeIn)
    {
        float fadeGoal = fadeIn ? 1 : 0;
        while (!group.alpha.Equals(fadeGoal))
        {
            group.alpha = Mathf.Lerp(group.alpha, fadeGoal, speed * Time.deltaTime);

            if (Mathf.Abs(group.alpha - fadeGoal) <= margin)
            {
                group.alpha = fadeGoal;
            }

            yield return null;
        }
        OnCanvasFaded?.Invoke();
        OnCanvasFaded = null;
    }

    public void HideUI()
    {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(nameof(FadeCanvas), false);
        OnCanvasFaded += DeactivateUI;
    }

    private void DeactivateUI()
    {
        roundUI.SetActive(false);
    }
}
