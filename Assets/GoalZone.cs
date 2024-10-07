using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // TextMeshProを使用するための名前空間

public class GoalZone : MonoBehaviour
{
    public TextMeshProUGUI goalText;  // TextMeshProのUIテキスト
    public string playerTag = "Player";  // プレイヤーのタグ

    // Start is called before the first frame update
    void Start()
    {
        // 初期状態ではゴールテキストを非表示に設定
        goalText.enabled = false;
    }

    // プレイヤーが範囲内（トリガー）に入ったら
    void OnTriggerEnter(Collider other)
    {
        // プレイヤー（タグが一致するオブジェクト）が範囲に入ったとき
        if (other.CompareTag(playerTag))
        {
            // ゴールテキストを表示
            goalText.enabled = true;
        }
    }

        // プレイヤーが範囲を離れたらゴールテキストを非表示にする場合
    void OnTriggerExit(Collider other)
    {
        // プレイヤーが範囲から出たとき
        if (other.CompareTag(playerTag))
        {
            // ゴールテキストを非表示
            goalText.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
