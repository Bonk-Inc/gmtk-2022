using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSelector : MonoBehaviour
{

    [SerializeField]
    private List<DiceSpot> spots;

    [SerializeField]
    private float selectedDiceMoveAmount = 20;

    private List<int> selected;

    public void StartSelecting()
    {
        selected = new List<int>();
        for (int i = 0; i < spots.Count; i++)
        {
            var spot = spots[i];
            var spotIndex = i;
            spot.OnClick += (spot) =>
            {
                if (selected.Contains(spotIndex))
                {
                    spots[spotIndex].Die.transform.Translate(0, -selectedDiceMoveAmount, 0);
                    selected.Remove(spotIndex);
                }
                else
                {
                    spots[spotIndex].Die.transform.Translate(0, selectedDiceMoveAmount, 0);
                    selected.Add(spotIndex);
                }
            };
        }
    }

    public void StopSelecting()
    {
        for (int i = 0; i < spots.Count; i++)
        {
            var spot = spots[i];
            spot.OnClick = null;
            if (selected.Contains(i))
            {
                spots[i].Die.transform.Translate(0, -selectedDiceMoveAmount, 0);
                selected.Remove(i);
            }
        }
    }

    public ActionDie[] GetSelectedDice()
    {
        ActionDie[] dice = new ActionDie[selected.Count];
        for (int i = 0; i < selected.Count; i++)
        {
            dice[i] = spots[selected[i]].Die;
        }
        return dice;
    }

}
