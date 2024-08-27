using System.Collections.Generic;
using UnityEngine;

public static class AvatarCache
{
    private static Dictionary<string, Sprite> avatarCache = new();

    public static bool TryGetAvatar(string url, out Sprite avatar)
    {
        return avatarCache.TryGetValue(url, out avatar);
    }

    public static void AddAvatarToCache(string url, Sprite avatar)
    {
        if (!avatarCache.ContainsKey(url))
        {
            avatarCache[url] = avatar;
        }
    }
}