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