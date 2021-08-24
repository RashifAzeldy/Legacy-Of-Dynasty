using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/CardData/RadiantActivitiesCardData", fileName = "RadiantActivitiesCard")]
public class RadiantActivitiesCardData : CardDataBase
{

    [Tooltip("Player Minimum Age for this card to be spawned")]
    public Age minimumAgeToSpawn;

}
