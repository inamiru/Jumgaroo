using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSizeController : MonoBehaviour
{
    public RectTransform leftArrow;  // 左矢印
    public RectTransform rightArrow;  // 右矢印
    public Vector3 defaultScale = Vector3.one;  // デフォルトのスケール
    public Vector3 enlargedScale = new Vector3(1.2f, 1.2f, 1.2f);  // 大きくした時のスケール

    // 初期化時に矢印のスケールを設定
    void Start()
    {
        // 両方の矢印のスケールを1に設定する
        ResetArrowScale();
    }

    // 左右の矢印のサイズを選択状態に応じて更新
    public void UpdateArrowSize(int selectedIndex)
    {
        // selectedIndex が 0 なら左矢印を大きくし、右矢印はデフォルトサイズに
        if (selectedIndex == 0)
        {
            leftArrow.localScale = enlargedScale;   // 左矢印を大きく
            rightArrow.localScale = defaultScale;   // 右矢印は元のサイズ
        }
        // selectedIndex が 1 なら右矢印を大きくし、左矢印はデフォルトサイズに
        else if (selectedIndex == 1)
        {
            leftArrow.localScale = defaultScale;   // 左矢印は元のサイズ
            rightArrow.localScale = enlargedScale; // 右矢印を大きく
        }
    }

    // 矢印のスケールをリセットするメソッド
    public void ResetArrowScale()
    {
        leftArrow.localScale = defaultScale;  // 左矢印を初期サイズに
        rightArrow.localScale = defaultScale; // 右矢印を初期サイズに
    }
}
