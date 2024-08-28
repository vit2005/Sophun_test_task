using UnityEngine;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SimplePopupManager;

public class LeaderboardPopup : MonoBehaviour, IPopupInitialization, IPopupClose
{
    public Transform itemsContainer;
    public GameObject itemPrefab;

    private List<LeaderboardItem> spawnedItems = new();
    private CancellationTokenSource cancellationTokenSource;

    public async Task Init(object param)
    {
        if (param is LeaderboardData[] leaderboardDataArray)
        {
            cancellationTokenSource = new CancellationTokenSource();

            List<Task> initializationTasks = new List<Task>();

            foreach (var data in leaderboardDataArray)
            {
                GameObject itemObject = Instantiate(itemPrefab, itemsContainer);
                LeaderboardItem leaderboardItem = itemObject.GetComponent<LeaderboardItem>();
                spawnedItems.Add(leaderboardItem);

                if (leaderboardItem != null)
                {
                    initializationTasks.Add(leaderboardItem.Init(data, cancellationTokenSource.Token));
                }
            }

            try
            {
                await Task.WhenAll(initializationTasks);
            }
            catch (TaskCanceledException)
            {
                Debug.Log("One or more leaderboard item initializations were canceled.");
            }
        }
    }


    public void ClosePopup()
    {
        cancellationTokenSource?.Cancel();

        foreach (var item in spawnedItems)
        {
            item.CancelLoading();
            Destroy(item.gameObject);
        }

        spawnedItems.Clear();
        Destroy(gameObject);
    }
}
