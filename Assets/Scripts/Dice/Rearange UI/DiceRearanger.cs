using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRearanger : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private DiceSpot[] spots;

    private DiceSpot current = null;

    [SerializeField]
    private int currentEmpty;

    private ActionDie draggingDie = null;

    [SerializeField]
    private Button finishRearrangeButton;

    [SerializeField]
    private DiceManager manager;

    private Coroutine rearangeUpdater;
    private bool isRearanging = false;

    public event Action OnRearrangeFinished;

    public void StartRearrange()
    {
        if(isRearanging)
            return;

        isRearanging = true;
        rearangeUpdater = StartCoroutine(RearrangeUpdater());
        finishRearrangeButton.gameObject.SetActive(true);
        finishRearrangeButton.onClick.AddListener(FinishRearrange);
        for (int i = 0; i < spots.Length; i++)
        {
            spots[i].MouseOver += (spot) =>
            {

                if (draggingDie != null)
                {
                    var spotDifference = spot.Position - currentEmpty;
                    var direction = 1;
                    if (spotDifference != 0)
                        direction = spotDifference / Mathf.Abs(spotDifference);

                    for (int i = 1; i <= Mathf.Abs(spotDifference); i++)
                    {
                        var prevEmpty = currentEmpty + (i - 1) * direction;
                        var move = currentEmpty + i * direction;
                        spots[prevEmpty].SetDie(spots[move].Die);
                        spots[move].SetDie(null);
                    }
                    currentEmpty = spot.Position;
                }
                current = spot;
            };

            spots[i].MouseLeave += (spot) =>
            {
                current = null;
            };
        }
    }

    public void FinishRearrange()
    {
        finishRearrangeButton.gameObject.SetActive(false);
        finishRearrangeButton.onClick.RemoveListener(FinishRearrange);
        StopCoroutine(rearangeUpdater);
        for (int i = 0; i < spots.Length; i++)
        {
            spots[i].MouseOver = null;
            spots[i].MouseLeave = null;

            manager.Dice[i] = spots[i].Die;
        }
        isRearanging = false;
        OnRearrangeFinished?.Invoke();
    }

    private IEnumerator RearrangeUpdater()
    {
        while (true)
        {
            if (current != null && Input.GetMouseButtonDown(0))
            {
                draggingDie = current.Die;
                currentEmpty = current.Position;
            }
            if (draggingDie != null)
            {
                // var position = draggingDie.transform.position;
                // var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                // position.x = mousePos.x;
                // position.y = mousePos.y;
                // draggingDie.transform.position = position;

                draggingDie.transform.position = Input.mousePosition;
            }
            if (draggingDie != null && Input.GetMouseButtonUp(0))
            {
                spots[currentEmpty].SetDie(draggingDie);
                draggingDie = null;
            }
            yield return null;
        }
    }


}
