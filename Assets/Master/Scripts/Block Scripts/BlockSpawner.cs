using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{

    #region Serialized Property Field

    [Header("Spawning Config : ")]
    [SerializeField] int spawnLimit = 15;
    [SerializeField] Vector2 spawnIntervalRange = new Vector2(0, 10);

    [Space]

    [SerializeField] List<CardDataList> cardList = new List<CardDataList>();
    [SerializeField] List<EffortPosition> spawnPositions = new List<EffortPosition>();

    [Space]

    [Header("Block Config : ")]
    [SerializeField] BlockController blockNeutral;
    [SerializeField] BlockController blockPositive;
    [SerializeField] BlockController blockNegative;
    [SerializeField] BlockController blockMystery;
    [Space]
    [SerializeField] float blockSpeed;

    #endregion

    [SerializeField] List<GameObject> spawnedBlock = new List<GameObject>();

    public List<GameObject> GetSpawnedBlock { get { return spawnedBlock; } set { spawnedBlock = value; } }

    PlayerState player;
    PlayerStatus status;
    bool _startCheck = true;

    private void Start()
    {
        player = FindObjectOfType<PlayerState>();
        status = FindObjectOfType<PlayerStatus>();
    }

    private void Update()
    {
        //spawnedBlock.Count < spawnLimit &&
        if (_startCheck && Time.timeScale == 1)
        {
            SpawnBlock(Random.Range(spawnIntervalRange.x, spawnIntervalRange.y), gameObject.transform, 1);
        }
    }

    int SpawnPosIndex(BlockEffort effort)
    {

        foreach (EffortPosition item in spawnPositions)
        {
            if (item.GetEffort == effort)
                return spawnPositions.IndexOf(item);

        }
        return 0;
    }

    Vector3 getSpawnPosition(BlockEffort spawnAtEffort)
    {

        foreach (EffortPosition item in spawnPositions)
        {
            if (item.GetEffort == spawnAtEffort)
            {
                int posIndex = SpawnPosIndex(spawnAtEffort);

                //Change This Code
                return spawnPositions[posIndex].GetSpawnPos[Random.Range(0, spawnPositions[posIndex].GetSpawnPos.Count)].position;
            }
        }

        return Vector3.zero;
    }

    public void SpawnBlock(float delay, Transform parent, int quantity)
    {

        _startCheck = false;
        StartCoroutine(SpawnDelay(delay, parent, quantity, BlockEffort.High));
        StartCoroutine(SpawnDelay(delay, parent, quantity, BlockEffort.Medium));
        StartCoroutine(SpawnDelay(delay, parent, quantity, BlockEffort.Low));
        StartCoroutine(CheckingCountdown(delay * quantity));
    }

    IEnumerator SpawnDelay(float time, Transform parent, int objQuantity, BlockEffort effort)
    {
        for (int i = 0; i < objQuantity; i++)
        {
            GameObject _cacheBlock;
            BlockController _cacheBController;
            CardDataBase _cacheCard = GetStoryCard(CheckRequirement(status, effort, cardList));

            yield return new WaitForSeconds(time);

            if (_cacheCard)
            {
                switch (_cacheCard.cardValue)
                {
                    case CardValue.Positive:
                        _cacheBlock = ObjectPoolerManager.Instance.SpawnObjectFromPool("BlockPositive", getSpawnPosition(effort), Quaternion.identity);
                            //Instantiate(blockPositive.gameObject, getSpawnPosition(effort), Quaternion.identity, parent);
                        break;

                    case CardValue.Negative:
                        _cacheBlock = ObjectPoolerManager.Instance.SpawnObjectFromPool("BlockNegative", getSpawnPosition(effort), Quaternion.identity);
                        //Instantiate(blockNegative.gameObject, getSpawnPosition(effort), Quaternion.identity, parent);
                        break;

                    case CardValue.Mystery:
                        _cacheBlock = ObjectPoolerManager.Instance.SpawnObjectFromPool("BlockMystery", getSpawnPosition(effort), Quaternion.identity);
                        //Instantiate(blockMystery.gameObject, getSpawnPosition(effort), Quaternion.identity, parent);
                        break;

                    case CardValue.Neutral:
                        _cacheBlock = ObjectPoolerManager.Instance.SpawnObjectFromPool("BlockNeutral", getSpawnPosition(effort), Quaternion.identity);
                        //Instantiate(blockNeutral.gameObject, getSpawnPosition(effort), Quaternion.identity, parent);
                        break;

                    default:
                        _cacheBlock = ObjectPoolerManager.Instance.SpawnObjectFromPool("BlockNeutral", getSpawnPosition(effort), Quaternion.identity);
                        //Instantiate(blockNeutral.gameObject, getSpawnPosition(effort), Quaternion.identity, parent);
                        break;
                }

                _cacheBController = _cacheBlock.GetComponent<BlockController>();
                _cacheBController.blockSpeed = blockSpeed;

                _cacheBController.cardData = _cacheCard;

                _cacheBController.InitBlock();

                spawnedBlock.Add(_cacheBlock);
            }
        }
    }

    public IEnumerator CheckingCountdown(float time)
    {
        yield return new WaitForSeconds(time);
        _startCheck = true;
    }

    List<CardDataBase> CheckRequirement(PlayerStatus status, BlockEffort effort, List<CardDataList> baseCardList)
    {
        List<CardDataBase> cacheSpawnableCards = new List<CardDataBase>();
        // Here.
        switch (effort)
        {
            case BlockEffort.High:
                if (player.GetPlayerCurrentState == Age.Child)
                {
                    foreach (CardDataBase item in baseCardList[0].highEffortCards)

                        if (LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement) || item.spawnRequirement.EducationStage == EducationStage.None
                        || item.spawnRequirement.JobData.jobType == JobType.None || item.spawnRequirement.JobData.jobLevel == JobLevel.None || item.spawnRequirement.LoverStage == LoverStage.None)
                            cacheSpawnableCards.Add(item);
                }
                else if (player.GetPlayerCurrentState == Age.Teen)
                {
                    foreach (CardDataBase item in baseCardList[1].highEffortCards)

                        if (LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement) || item.spawnRequirement.EducationStage == EducationStage.None
                        || item.spawnRequirement.JobData.jobType == JobType.None || item.spawnRequirement.JobData.jobLevel == JobLevel.None || item.spawnRequirement.LoverStage == LoverStage.None)
                            cacheSpawnableCards.Add(item);
                }
                else if (player.GetPlayerCurrentState == Age.Adult)
                {
                    foreach (CardDataBase item in baseCardList[2].highEffortCards)

                        if (LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement) || item.spawnRequirement.EducationStage == EducationStage.None
                        || item.spawnRequirement.JobData.jobType == JobType.None || item.spawnRequirement.JobData.jobLevel == JobLevel.None || item.spawnRequirement.LoverStage == LoverStage.None)
                            cacheSpawnableCards.Add(item);
                }
                else if (player.GetPlayerCurrentState == Age.Elder)
                {
                    foreach (CardDataBase item in baseCardList[3].highEffortCards)

                        if (LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement) || item.spawnRequirement.EducationStage == EducationStage.None
                        || item.spawnRequirement.JobData.jobType == JobType.None || item.spawnRequirement.JobData.jobLevel == JobLevel.None || item.spawnRequirement.LoverStage == LoverStage.None)
                            cacheSpawnableCards.Add(item);
                }

                break;

            case BlockEffort.Medium:
                if (player.GetPlayerCurrentState == Age.Child)
                {
                    foreach (CardDataBase item in baseCardList[0].normalEffortCards)
                        if (LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement) || item.spawnRequirement.EducationStage == EducationStage.None
                        || item.spawnRequirement.JobData.jobType == JobType.None || item.spawnRequirement.JobData.jobLevel == JobLevel.None || item.spawnRequirement.LoverStage == LoverStage.None)
                            cacheSpawnableCards.Add(item);
                }
                else if (player.GetPlayerCurrentState == Age.Teen)
                {
                    foreach (CardDataBase item in baseCardList[1].normalEffortCards)
                        if (LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement) || item.spawnRequirement.EducationStage == EducationStage.None
                        || item.spawnRequirement.JobData.jobType == JobType.None || item.spawnRequirement.JobData.jobLevel == JobLevel.None || item.spawnRequirement.LoverStage == LoverStage.None)
                            cacheSpawnableCards.Add(item);
                }
                else if (player.GetPlayerCurrentState == Age.Adult)
                {
                    foreach (CardDataBase item in baseCardList[2].normalEffortCards)
                        if (LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement) || item.spawnRequirement.EducationStage == EducationStage.None
                        || item.spawnRequirement.JobData.jobType == JobType.None || item.spawnRequirement.JobData.jobLevel == JobLevel.None || item.spawnRequirement.LoverStage == LoverStage.None)
                            cacheSpawnableCards.Add(item);
                }
                else if (player.GetPlayerCurrentState == Age.Elder)
                {
                    foreach (CardDataBase item in baseCardList[3].normalEffortCards)
                        if (LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement) || item.spawnRequirement.EducationStage == EducationStage.None
                        || item.spawnRequirement.JobData.jobType == JobType.None || item.spawnRequirement.JobData.jobLevel == JobLevel.None || item.spawnRequirement.LoverStage == LoverStage.None)
                            cacheSpawnableCards.Add(item);
                }

                break;

            case BlockEffort.Low:
                if (player.GetPlayerCurrentState == Age.Child)
                {
                    foreach (CardDataBase item in baseCardList[0].lowEffortCards)
                        if (LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement) || item.spawnRequirement.EducationStage == EducationStage.None
                        || item.spawnRequirement.JobData.jobType == JobType.None || item.spawnRequirement.JobData.jobLevel == JobLevel.None || item.spawnRequirement.LoverStage == LoverStage.None)
                            cacheSpawnableCards.Add(item);
                }
                else if (player.GetPlayerCurrentState == Age.Teen)
                {
                    foreach (CardDataBase item in baseCardList[1].lowEffortCards)
                        if (LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement) || item.spawnRequirement.EducationStage == EducationStage.None
                        || item.spawnRequirement.JobData.jobType == JobType.None || item.spawnRequirement.JobData.jobLevel == JobLevel.None || item.spawnRequirement.LoverStage == LoverStage.None)
                            cacheSpawnableCards.Add(item);
                }
                else if (player.GetPlayerCurrentState == Age.Adult)
                {
                    foreach (CardDataBase item in baseCardList[2].lowEffortCards)
                        if (LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement) || item.spawnRequirement.EducationStage == EducationStage.None
                        || item.spawnRequirement.JobData.jobType == JobType.None || item.spawnRequirement.JobData.jobLevel == JobLevel.None || item.spawnRequirement.LoverStage == LoverStage.None)
                            cacheSpawnableCards.Add(item);
                }
                else if (player.GetPlayerCurrentState == Age.Elder)
                {
                    foreach (CardDataBase item in baseCardList[3].lowEffortCards)
                        if (LODFunctionLibrary.ComparePlayerStatusData(status.playerStatusData, item.spawnRequirement) || item.spawnRequirement.EducationStage == EducationStage.None
                        || item.spawnRequirement.JobData.jobType == JobType.None || item.spawnRequirement.JobData.jobLevel == JobLevel.None || item.spawnRequirement.LoverStage == LoverStage.None)
                            cacheSpawnableCards.Add(item);
                }

                break;
        }
        return cacheSpawnableCards;
    }

    // Child Index State = 0
    // Teen Index State = 1
    // Adult Index State = 2
    // Elder Index State = 3
    CardDataBase GetStoryCard(List<CardDataBase> cardList)
    {
        if (cardList.Count != 0)
            return cardList[Random.Range(0, cardList.Count)];

        return null;
    }

}

public enum BlockEffort
{
    High,
    Medium,
    Low
}

[System.Serializable]
public class EffortPosition
{
    [SerializeField] BlockEffort effort;

    [SerializeField] List<Transform> spawnPos = new List<Transform>(3);

    public BlockEffort GetEffort { get { return effort; } }
    public List<Transform> GetSpawnPos { get { return spawnPos; } }
}
