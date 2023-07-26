using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mkey;
using MkeyFW;

public class ShopRewards : MonoBehaviour
{

    int rewardsCoin = 0;

    public void WatchVideoRewards(int amount) {
        if(AdsManagerWrapper.INSTANCE) {
            rewardsCoin = amount;
            AdsManagerWrapper.INSTANCE.ShowRewards(onAdLoded => {}, onAdFailedToLoad => {}, iRewards => {
                 SlotPlayer.Instance.AddCoins(rewardsCoin);
                 SlotSoundController.Instance.SoundPlayEarnCoin(0, null);
                 FindAnyObjectByType<ShopWindowController>().CloseWindow();
            });
        }
    }

    void handleRewardsCoin(bool isSuccess) {
        if(isSuccess) {
            SlotPlayer.Instance.AddCoins(rewardsCoin);
        }
    }
    
}
