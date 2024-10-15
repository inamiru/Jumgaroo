using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    public int areaID;  // エリアID
    public PauseWithTextDisplay pauseWithTextDisplay;  // ImageDisplayAndPauseの参照

    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーがエリアに侵入した場合
        if (other.CompareTag("Player"))
        {
            pauseWithTextDisplay.OnPlayerEnterArea(areaID);  // イメージを表示してゲームを停止
        }
    }
}
