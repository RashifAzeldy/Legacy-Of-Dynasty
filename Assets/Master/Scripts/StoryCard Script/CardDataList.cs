using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Card List", fileName = "Card List")]
public class CardDataList : ScriptableObject
{
    [Header("Config : ")]
    public CardDataBase[] highEffortCards;
    public CardDataBase[] normalEffortCards;
    public CardDataBase[] lowEffortCards;

    /// <summary>
    /// Get length of the specified Cards Data List
    /// </summary>
    /// <param name="cardEffort"> Specifier which Card Data Effort you wanted to get the length </param>
    /// <returns></returns>
    public int getCardsDataLength(BlockEffort cardEffort)
    {
        switch (cardEffort)
        {
            case BlockEffort.High:
                return highEffortCards.Length;
                break;
            case BlockEffort.Normal:
                return normalEffortCards.Length;
                break;
            case BlockEffort.Low:
                return lowEffortCards.Length;
                break;
            default:
                return 0;
                break;
        }
    }

    /// <summary>
    /// Get length of the total list in this card data list
    /// </summary>
    /// <returns> Total length of High, Normal, Low Card Effort List </returns>
    public int getCardDataListTotalLength()
    {
        return getCardsDataLength(BlockEffort.High) + getCardsDataLength(BlockEffort.Normal) + getCardsDataLength(BlockEffort.Low);
    }
}