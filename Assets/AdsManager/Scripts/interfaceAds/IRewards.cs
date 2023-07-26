public class MyRewardsItem
{
    public int? Amount { get; set; }
    public string Type { get; set; }
}

public class IRewards
{
    public MyRewardsItem rewardsItem;
    public void OnUserEarnedReward(MyRewardsItem rewardsItem) {
        this.rewardsItem = rewardsItem;
    }
}
