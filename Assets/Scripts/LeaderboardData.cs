[System.Serializable]
public class LeaderboardData
{
    public string name;
    public int score;
    public string avatar;
    public string type;
}

[System.Serializable]
public class LeaderboardDataList
{
    public LeaderboardData[] leaderboard;
}