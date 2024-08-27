using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.Networking;
using System.Threading;
using TMPro;

public class LeaderboardItem : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI scoreText;
    public GameObject loadingAvatarText;
    public Image avatarImage;
    public Color diamondColor;
    public Color goldColor;
    public Color silverColor;
    public Color bronzeColor;
    public Color defaultColor;

    private CancellationTokenSource cancellationTokenSource;

    public async Task Init(LeaderboardData data, CancellationToken cancellationToken)
    {
        cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        nameText.text = data.name;
        scoreText.text = data.score.ToString();

        scoreText.color = GetColorForType(data.type);
        int size = GetSize(data.type);
        scoreText.fontSize = size;
        nameText.fontSizeMax = size;

        if (AvatarCache.TryGetAvatar(data.avatar, out Sprite cachedAvatar))
        {
            avatarImage.sprite = cachedAvatar;
            loadingAvatarText.SetActive(false);
        }
        else
        {
            try
            {
                await SetAvatarImage(data.avatar, cancellationTokenSource.Token);
                loadingAvatarText.SetActive(false);
            }
            catch (TaskCanceledException)
            {
                Debug.Log("Avatar loading was canceled.");
            }
        }

    }

    private async Task SetAvatarImage(string url, CancellationToken cancellationToken)
    {
        using (var www = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET))
        {
            www.downloadHandler = new DownloadHandlerTexture();
            var operation = www.SendWebRequest();

            while (!operation.isDone)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    www.Abort();
                    cancellationToken.ThrowIfCancellationRequested();
                }
                await Task.Yield();
            }

            if (www.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(www);
                Sprite avatarSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

                AvatarCache.AddAvatarToCache(url, avatarSprite);

                avatarImage.sprite = avatarSprite;
            }
            else
            {
                Debug.LogError("Failed to download avatar image.");
            }
        }
    }

    public void CancelLoading()
    {
        cancellationTokenSource?.Cancel();
    }

    private Color GetColorForType(string type)
    {
        return type switch
        {
            "Diamond" => diamondColor,
            "Gold" => goldColor,
            "Silver" => silverColor,
            "Bronze" => bronzeColor,
            _ => defaultColor,
        };
    }

    private int GetSize(string type)
    {
        return type switch
        {
            "Diamond" => 38,
            "Gold" => 36,
            "Silver" => 34,
            "Bronze" => 32,
            _ => 30,
        };
    }
}
