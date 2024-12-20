using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // TextMeshProの名前空間

public class CountdownController : MonoBehaviour
{
    public TextMeshProUGUI countdownText;  // TextMeshProのUIテキスト

    public int countdownTime = 3;  // カウントダウンの開始値
    public PlayerAction playerAction;  // PlayerMovementの参照
    public PlayerMovement playerMovement;  // PlayerMovementの参照
    public PlayerJump playerJump;  // PlayerMovementの参照

    // Start is called before the first frame update
    void Start()
    {
        playerMovement.StopMovement(); 
        playerJump.DisableInput();

        // カウントダウンを開始するコルーチンをスタート
        StartCoroutine(CountdownToStart());
    }

    // カウントダウン処理
    IEnumerator CountdownToStart()
    {
        // カウントダウン中
        while (countdownTime > 0)
        {
            // テキストにカウントダウンの数字を表示
            countdownText.text = countdownTime.ToString();
            // 1秒待機
            yield return new WaitForSeconds(1f);
            // カウントダウンを減らす
            countdownTime--;
        }

        // 最後に「GO!」を表示
        countdownText.text = "GO!";

        // 1秒待機して、カウントダウンテキストを非表示
        yield return new WaitForSeconds(1.0f);
        countdownText.gameObject.SetActive(false);

        // プレイヤーの入力を有効にする
        StartGame();
    }

    // ゲーム開始の処理
    void StartGame()
    {
        // プレイヤーの入力を許可
        playerAction.EnableInput();
        playerMovement.ResumeMovement();
        playerJump.EnableInput();

        // タイマーを開始
        GameTimeDisplay.Instance.StartTimer();
    }
}