using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public PlayerStates playerStates;  // ScriptableObject の参照
    private PlayerMovement playerMovement;  // PlayerMovement の参照
    private PlayerJump playerJump;  // PlayerJumpの参照

    private bool canMove = false;    // プレイヤーの入力を受け付けるかどうかを制御するフラグ
    private Animator animator;

    private float dustEffectTimer = 0f; // 土煙エフェクト生成のタイマー
    private float dustEffectInterval = 0.1f; // エフェクト生成の間隔
    private float dustEffectHeightOffset = 1.2f; // エフェクト位置のY座標オフセット
    private float dustEffectZOffset = -0.2f; // エフェクト位置のZ座標オフセット

    // Start is called before the first frame update
    void Start()
    {
        // コンポーネントの取得
        playerMovement = GetComponent<PlayerMovement>();  // PlayerMovement コンポーネントを取得
        playerJump = GetComponent<PlayerJump>();  // PlayerJump コンポーネントを取得
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 移動処理はPlayerMovementに委譲
        playerMovement.Move(canMove);
        // ジャンプ処理もPlayerJumpに委譲
        playerJump.CheckJumpInput();

        // アニメーションのSpeedパラメータに現在のスピードを設定
        animator.SetFloat("Speed", playerMovement.currentSpeed);

        // アニメーションのBlend Treeの速度をチェック
        if (animator.GetFloat("Speed") > 0.1f) // 適切なしきい値を設定
        {
            // タイマーを更新
            dustEffectTimer += Time.deltaTime;
            // 一定間隔で土煙エフェクトを表示
            if (dustEffectTimer >= dustEffectInterval)
            {
                ShowDustEffect();
                dustEffectTimer = 0f; // タイマーをリセット
            }
        }
    }

    private void ShowDustEffect()
    {
        // ジャンプ中でない場合にのみエフェクトを生成
        if (!playerJump.IsJumping)
        {
            // プレイヤーの足元の位置を計算
            Vector3 effectPosition = transform.position + Vector3.up * dustEffectHeightOffset; // 足元のY座標を調整

            // Z軸方向のオフセットを追加（前方にオフセット）
            effectPosition += transform.forward * dustEffectZOffset; // プレイヤーの前方にオフセット

            // EffectManagerを使用してダストエフェクトを表示
            EffectManager.instance.PlayDustEffect(effectPosition);
        }
    }

    // 外部から呼び出して入力を有効にするメソッド
    public void EnableInput()
    {
        canMove = true;
    }

    public void DisableInput()
    {
        canMove = false;  // プレイヤーの入力を無効にする
    }
}
