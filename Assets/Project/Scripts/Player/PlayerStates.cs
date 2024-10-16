using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatesSO", menuName = "ScriptableObjects/PlayerStatesScriptableObject", order = 1)]
public class PlayerStates : ScriptableObject
{
    public int maxHits = 3;  // 敵に接触できる最大回数
    public int currentHitCount;   // 現在の接触回数
    public float knockbackForce = 5f;        // ノックバックの力
    public float knockbackDuration = 0.5f;    // ノックバックの持続時間
    
    // 初期化メソッド
    public void InitializeHP()
    {
        currentHitCount = maxHits;  // ゲーム開始時に現在HPを最大値に設定
    }

    // ダメージを受けた場合にHPを減らす
    public void TakeDamage(int damage)
    {
        currentHitCount -= damage;
        currentHitCount = Mathf.Clamp(currentHitCount, 0, maxHits);  // HPは0未満にならないようにする
    }

    // HPが0かどうか確認する
    public bool IsDead()
    {
        return currentHitCount <= 0;
    }


    public float acceleration = 1.0f; // 加速度
    public float maxSpeed = 10.0f;    // 最大速度

    public float jumpForce = 5.0f;      // ジャンプ力
    private int jumpCount = 0;          // ジャンプの回数
    public int maxJumps = 2;            // 最大ジャンプ回数（二段ジャンプを許可するため2に設定）

    public float boostJumpForce = 10.0f;     // ジャンプブーストのジャンプ力
    public float forwardForce = 5f;           // 無限ジャンプ時の前方への力


}