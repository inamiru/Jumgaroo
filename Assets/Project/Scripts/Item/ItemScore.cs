using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;    // TextMeshProを使用するための名前空間

public class ItemScore : MonoBehaviour
{
    // シングルトンインスタンス
    public static ItemScore Instance { get; private set; }

    public TextMeshProUGUI scoreText;   // スコアを表示するTextMeshProのUI要素
    private int initialScore = 0;        // スコアの初期値
    private int currentScore;           // 現在のスコア
    private int finalScore;  // ゴール時のスコア

    void Awake()
    {
        // シングルトンパターンの適用
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // シーンをまたいでオブジェクトを保持する
        }
        else
        {
            Destroy(gameObject);  // 既にインスタンスが存在する場合は新しいものを破棄
        }
    }

    void Start()
    {
        // 初期スコアを設定
        currentScore = initialScore;

        // 初期スコアを表示
        UpdateScoreDisplay();
    }

    // スコアを更新するメソッド（例: アイテム取得時などに呼ぶ）
    public void AddScore(int scoreValue)
    {
        currentScore += scoreValue;
        UpdateScoreDisplay();
    }

    // スコア表示を更新する
    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString();
        }
    }

    public void SetFinalScore()
    {
        finalScore = currentScore;
    }

    public int GetFinalScore()
    {
        return finalScore;
    }
    // スコアのリセット（ゲームリスタート時に使用）
    public void ResetScore()
    {
        currentScore = initialScore;

        UpdateScoreDisplay();
    }

    // ゲーム終了時のスコアをリセットするメソッド
    public void ResetFinalScore()
    {
        finalScore = 0;
    }
}
