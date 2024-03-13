// ---------------------------------------------------------  
// PlayerInput.cs  
//  
// スマートフォン入力処理
//
// 作成日: 2024/3/7
// 作成者: 北川 稔明
// ---------------------------------------------------------  
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    #region 変数  

    #region const定数

    // RayCastの位置
    private const float RAY_POS_ADVENT_SUITED = 0.5f;
    private const float RAY_POS_ADVENT_OPPOSITE = 0.25f;

    // RayCastの長さ
    private const float RAY_LENGTH = 0.025f;

    #endregion

    [SerializeField, Header("レイヤー指定")]
    private LayerMask _groundLayer;

    [SerializeField, Header("移動スピード")]
    private float _speed = 10.0f;

    // プレイヤーの入力値格納
    private float xPos = default;
    private float yPos = default;

    // めり込み防止のRayCast
    private RaycastHit2D _plusRightXPos = default;
    private RaycastHit2D _plusLeftXPos = default;

    private RaycastHit2D _minusRightXPos = default;
    private RaycastHit2D _minusLeftXPos = default;

    private RaycastHit2D _plusRightYPos = default;
    private RaycastHit2D _plusLeftYPos = default;

    private RaycastHit2D _minusRightYPos = default;
    private RaycastHit2D _minusLeftYPos = default;

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update ()
    {
        // 加速度センサーの取得
        xPos = _speed * Input.acceleration.x;
        yPos = _speed * Input.acceleration.y;

        // Rayが壁に当たっていたら移動量を0にする
        if (xPos > 0 && PlusXPos())
        {
            xPos = 0;
        }
        else if (xPos < 0 && MinusXPos())
        {
            xPos = 0;
        }

        // Rayが壁に当たっていたら移動量を0にする
        if (yPos > 0 && MinusYPos())
        {
            yPos = 0;
        }
        else if (yPos < 0 && PlusYPos())
        {
            yPos = 0;
        }

        // 移動処理
        transform.localPosition = new Vector2(transform.localPosition.x + xPos * Time.deltaTime, transform.localPosition.y + yPos * Time.deltaTime);

    }

    /// <summary>
    /// RayCast処理
    /// </summary>
    /// <returns>壁に当たっているかどうか</returns>
    private bool PlusYPos()
    {
        // 移動できるかどうか
        bool isMove = false;

        // RayCastの出現位置
        Vector2 rayPosPlus = new Vector2(transform.position.x + RAY_POS_ADVENT_OPPOSITE, transform.position.y - RAY_POS_ADVENT_SUITED);
        Vector2 rayPosMinus = new Vector2(transform.position.x - RAY_POS_ADVENT_OPPOSITE, transform.position.y - RAY_POS_ADVENT_SUITED);


        // RayCast出現処理
        _plusRightYPos = Physics2D.Raycast(rayPosPlus, Vector2.down, RAY_LENGTH, _groundLayer);
        _plusLeftYPos = Physics2D.Raycast(rayPosMinus, Vector2.down, RAY_LENGTH, _groundLayer);

        // RayCastが壁に当たっていたら移動できない
        if (_plusRightYPos.collider != null || _plusLeftYPos.collider != null)
        {
            // 移動できない
            isMove = true;
        }

        return isMove;
    }

    /// <summary>
    /// RayCast処理
    /// </summary>
    /// <returns>壁に当たっているかどうか</returns>
    private bool MinusYPos()
    {
        // 移動できるかどうか
        bool isMove = false;

        // RayCastの出現位置
        Vector2 rayPosPlus = new Vector2(transform.position.x + RAY_POS_ADVENT_OPPOSITE, transform.position.y + RAY_POS_ADVENT_SUITED);
        Vector2 rayPosMinus = new Vector2(transform.position.x - RAY_POS_ADVENT_OPPOSITE, transform.position.y + RAY_POS_ADVENT_SUITED);


        // RayCast出現処理
        _minusRightYPos = Physics2D.Raycast(rayPosPlus, Vector2.up, RAY_LENGTH, _groundLayer);
        _minusLeftYPos = Physics2D.Raycast(rayPosMinus, Vector2.up, RAY_LENGTH, _groundLayer);

        // RayCastが壁に当たっていたら移動できない
        if (_minusRightYPos.collider != null || _minusLeftYPos.collider != null)
        {
            // 移動できない
            isMove = true;
        }
        return isMove;
    }

    /// <summary>
    /// RayCast処理
    /// </summary>
    /// <returns>壁に当たっているかどうか</returns>
    private bool PlusXPos()
    {
        // 移動できるかどうか
        bool isMove = false;

        // RayCastの出現位置
        Vector2 rayPosPlus = new Vector2(transform.position.x + RAY_POS_ADVENT_SUITED, transform.position.y + RAY_POS_ADVENT_OPPOSITE);
        Vector2 rayPosMinus = new Vector2(transform.position.x + RAY_POS_ADVENT_SUITED, transform.position.y - RAY_POS_ADVENT_OPPOSITE);


        // RayCast出現処理
        _plusRightXPos = Physics2D.Raycast(rayPosPlus, Vector2.right, RAY_LENGTH, _groundLayer);
        _plusLeftXPos = Physics2D.Raycast(rayPosMinus, Vector2.right, RAY_LENGTH, _groundLayer);

        // RayCastが壁に当たっていたら移動できない
        if (_plusRightXPos.collider != null || _plusLeftXPos.collider != null)
        {
            // 移動できない
            isMove = true;
        }
        return isMove;
    }

    /// <summary>
    /// RayCast処理
    /// </summary>
    /// <returns>壁に当たっているかどうか</returns>
    private bool MinusXPos()
    {
        // 移動できるかどうか
        bool isMove = false;

        // RayCastの出現位置
        Vector2 rayPosPlus = new Vector2(transform.position.x - RAY_POS_ADVENT_SUITED, transform.position.y + RAY_POS_ADVENT_OPPOSITE);
        Vector2 rayPosMinus = new Vector2(transform.position.x - RAY_POS_ADVENT_SUITED, transform.position.y - RAY_POS_ADVENT_OPPOSITE);

        // RayCast出現処理
        _minusRightXPos = Physics2D.Raycast(rayPosPlus, Vector2.left, RAY_LENGTH, _groundLayer);
        _minusLeftXPos = Physics2D.Raycast(rayPosMinus, Vector2.left, RAY_LENGTH, _groundLayer);


        // RayCastが壁に当たっていたら移動できない
        if (_minusRightXPos.collider != null || _minusLeftXPos.collider != null)
        {
            // 移動できない
            isMove = true;
        }
        return isMove;
    }

    #endregion

}
