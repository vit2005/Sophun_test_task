using SimplePopupManager;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MainLoader : MonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject tip;
    private IPopupManagerService _service;
    private Coroutine _coroutine;
    private bool _isOpened = false;

    private const string jsonFileName = "Leaderboard";
    private const string popupTitle = "Leaderboard";

    void Start()
    {
        _service = new PopupManagerServiceService(parent);
    }

    public void ToggleLeaderboardPopUp()
    {
        if (_isOpened)
        {
            _service.ClosePopup(popupTitle);
            StopCoroutine(_coroutine);
            _isOpened = false;
        }
        else
        {
            LeaderboardDataList data = LoadLeaderboardData();
            _coroutine = StartCoroutine(OpenPopupCoroutine(popupTitle, data.leaderboard));
            _isOpened = true;
        }

        tip.SetActive(false);
    }

    private IEnumerator OpenPopupCoroutine(string name, object param)
    {
        var task = _service.OpenPopup(name, param);
        yield return new WaitUntil(() => task.IsCompleted);
    }

    public LeaderboardDataList LoadLeaderboardData()
    {
        return JsonUtility.FromJson<LeaderboardDataList>(Resources.Load<TextAsset>(jsonFileName).text);
    }
}
