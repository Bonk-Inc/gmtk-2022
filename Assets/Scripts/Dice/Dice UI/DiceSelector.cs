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

    public List<DiceSpot> Spots => spots;

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
                    ((RectTransform)spots[spotIndex].transform).sizeDelta = ((RectTransform)spots[spotIndex].transform).sizeDelta - new Vector2(0, selectedDiceMoveAmount);
                    spots[spotIndex].Die.transform.position += spots[spotIndex].transform.up * -selectedDiceMoveAmount  / 100;
                    selected.Remove(spotIndex);
                }
                else
                {
                    ((RectTransform)spots[spotIndex].transform).sizeDelta = ((RectTransform)spots[spotIndex].transform).sizeDelta + new Vector2(0, selectedDiceMoveAmount);
                    spots[spotIndex].Die.transform.position += spots[spotIndex].transform.up * selectedDiceMoveAmount  / 100 ;
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
                ((RectTransform)spots[i].transform).sizeDelta = ((RectTransform)spots[i].transform).sizeDelta - new Vector2(0, selectedDiceMoveAmount);
                spots[i].Die.transform.position += spots[i].transform.up * -selectedDiceMoveAmount / 100; 
                selected.Remove(i);
            }
        }
    }

    public (DieVisual dice, DiceSpot spot)[] GetSelectedDice()
    {
        (DieVisual dice, DiceSpot spot)[] dice = new (DieVisual dice, DiceSpot spot)[selected.Count];
        for (int i = 0; i < selected.Count; i++)
        {
            dice[i] = (spots[selected[i]].Die, spots[selected[i]]);//TODO
        }
        return dice;
    }

}
