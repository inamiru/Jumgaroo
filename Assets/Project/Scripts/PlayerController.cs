using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public int maxHits = 3;  // 敵に接触できる最大回数
    public int currentHitCount; // 現在の接触回数
    private bool isKnockedBack = false; // ノックバック状態のフラグ
    
    TakeDamage takeDamage;

    void Start ()
    {
        currentHitCount = maxHits;  // 接触回数を最大で初期化
        takeDamage = GetComponent<TakeDamage>();
    }
    public void SetKnockbackState(bool state)
    {
        isKnockedBack = state; // ノックバック状態を設定
    }
    public void IncreaseHitCount(int amount)
    {
        currentHitCount -= amount; // 接触回数を減少
        Debug.Log("Current Hit Count: " + currentHitCount); // デバッグ用メッセージ

        // 接触回数が0になったらゲームオーバー処理を呼び出し
        if (currentHitCount <= 0)
        {
            takeDamage.GameOver(); // ゲームオーバー処理を呼び出す
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        // 敵と接触したかどうかをタグで判定
        if (other.gameObject.CompareTag("Enemy"))
        {
            // ダメージ処理を呼び出し
            takeDamage.Damage(transform); // ダメージ管理スクリプトにダメージ処理を委譲
            currentHitCount--; // 接触回数を1減らす
 
            // 接触回数が0になったらゲームオーバー処理を呼び出し
            if (currentHitCount <= 0)
            {
                takeDamage.GameOver(); // ゲームオーバー処理を呼び出す
            }
        }
    }
}
