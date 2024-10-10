using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // TextMeshProを使用するために必要


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;  // シングルトンインスタンス
    public int score = 0;                 // スコアの初期値
    public TextMeshProUGUI scoreText;     // スコアを表示するUIテキスト

    void Awake()
    {
        // シングルトンの実装
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // スコア管理オブジェクトをシーン遷移で破棄しない
        }
        else
        {
            Destroy(gameObject);  // 既にインスタンスが存在する場合は、重複しないように削除
        }
    }

    // スコアを加算するメソッド
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();  // UIを更新
    }

    // UI上のスコア表示を更新する
    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
