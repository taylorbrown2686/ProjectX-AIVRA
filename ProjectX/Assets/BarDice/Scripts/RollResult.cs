using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollResult
{
    private List<int> diceValues = new List<int>();
    private int wilds;
    public int score;

    public string GetResult(List<GameObject> activeDice) {
      diceValues.Clear();
      wilds = 0;

      foreach (GameObject die in activeDice) {
        if (die.GetComponent<Die>().Value == 1) {
          wilds += 1;
          continue;
        }
        diceValues.Add(die.GetComponent<Die>().Value);
      }

      diceValues.Sort();
      var cnt = new Dictionary<int, int>();
      foreach (int value in diceValues) {
         if (cnt.ContainsKey(value)) {
            cnt[value]++;
         } else {
            cnt.Add(value, 1);
         }
      }
      int mostCommonValue = 0;
      int highestCount = 0;
      foreach (KeyValuePair<int, int> pair in cnt) {
        if (pair.Value >= highestCount) {
          mostCommonValue = pair.Key;
          highestCount = pair.Value;
        }
      }
      score = (highestCount + wilds) * 100 + mostCommonValue;
      return (highestCount + wilds) + " " + mostCommonValue + "'s!";
    }
}
