// ---------------------------------------------------------  
// SearchStage.cs  
//   
// ステージ探索処理
//
// 作成日: 2024/3/9
// 作成者: 北川 稔明
// ---------------------------------------------------------  
using UnityEngine;

public class SearchStage : MonoBehaviour
{

    #region 変数  

    #region const定数

    // 切り上げ
    private const int ROUNDING_UP = 1;

    // 赤ブロック
    private const int RED = 1;

    // 青ブロック
    private const int BULE = 2;

    // 緑ブロック
    private const int GREEN = 3;

    // 黄色ブロック
    private const int YELLOW = 4;

    // 赤ブロックゴール
    private const int RED_GOAL = 5;

    // 青ブロックゴール
    private const int BULE_GOAL = 6;

    // 緑ブロックゴール
    private const int GREEN_GOAL = 7;

    // 黄色ブロックゴール
    private const int YELLOW_GOAL = 8;

    // 壁
    private const int WALL = 9;

    // 閾値
    private const float THRESHOLD = 0.95f;

    #endregion

    // 子オブジェクト取得用
    private Transform _parentTransform = default;
    private StageGenerater _stageGenerater = default;

    // 青ブロックのゴール座標
    private int _blueRuinsXPos = default;
    private int _blueRuinsYPos = default;

    // 赤ブロックのゴール座標
    private int _redRuinsXPos = default;
    private int _redRuinsYPos = default;

    // 黄色ブロックのゴール座標
    private int _yellowRuinsXPos = default;
    private int _yellowRuinsYPos = default;

    // 緑ブロックのゴール座標
    private int _greenRuinsXPos = default;
    private int _greenRuinsYPos = default;

    // ステージの盤面管理
    private int[,] _stage = default;

    // 各ブロックがゴールについたかどうか
    private bool _isBlue = false;
    private bool _isRed = false;
    private bool _isYellow = false;
    private bool _isGreen = false;

    // 各ブロックのゴールが存在しているかどうか
    private bool _isBlueGoal = false;
    private bool _isRedGoal = false;
    private bool _isYellowGoal = false;
    private bool _isGreenGoal = false;

    // ゴール判定
    private bool _isGoal = false;

    #endregion

    #region プロパティ

    public bool IsGoal
    {
        get => _isGoal;
    }

    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start()
    {
        // 初期設定
        _stageGenerater = gameObject.GetComponent<StageGenerater>();
        _stageGenerater.Generate();
        _stage = new int[_stageGenerater.RowLength, _stageGenerater.ColumnLength - 1];
        _parentTransform = this.gameObject.transform;
        FirstSeach();
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update ()
    {
        // 探索
        Seach();

        // ゴール判定
        IsFin();
    }

    /// <summary>
    /// Stage探索処理
    /// </summary>
    private void Seach()
    {
        // 子オブジェクト探索
        foreach (Transform chlid in _parentTransform)
        {
            // 見つけた子オブジェクトのローカル座標を保存
            int verticalAxis = Mathf.FloorToInt(-chlid.localPosition.y);
            int horizontalAxis = Mathf.FloorToInt(chlid.localPosition.x);

            // タグでオブジェクトを判断して配列に格納
            switch (chlid.tag)
            {

                case "Red":
                    _stage[verticalAxis, horizontalAxis] = RED;
                    Red(XPosFairing(chlid.localPosition.x), YPosFairing(-chlid.localPosition.y));
                    break;

                case "Blue":
                    _stage[verticalAxis, horizontalAxis] = BULE;
                    Blue(XPosFairing(chlid.localPosition.x), YPosFairing(-chlid.localPosition.y));
                    break;

                case "Green":
                    _stage[verticalAxis, horizontalAxis] = GREEN;
                    Green(XPosFairing(chlid.localPosition.x), YPosFairing(-chlid.localPosition.y));
                    break;

                case "Yellow":
                    _stage[verticalAxis, horizontalAxis] = YELLOW;
                    Yellow(XPosFairing(chlid.localPosition.x), YPosFairing(-chlid.localPosition.y));
                    break;

                case "Red_Ruins":
                    _stage[verticalAxis, horizontalAxis] = RED_GOAL;
                    break;

                case "Blue_Ruins":
                    _stage[verticalAxis, horizontalAxis] = BULE_GOAL;
                    break;

                case "Green_Ruins":
                    _stage[verticalAxis, horizontalAxis] = GREEN_GOAL;
                    break;

                case "Yellow_Ruins":
                    _stage[verticalAxis, horizontalAxis] = YELLOW_GOAL;
                    break;

                case "Wall":
                    _stage[verticalAxis, horizontalAxis] = WALL;
                    break;

                default:
                    break;

            }
        }
    }

    /// <summary>
    /// 最初の探索
    /// 各ブロックのゴールがあるかどうかやゴールの座標を保存
    /// </summary>
    private void FirstSeach()
    {
        foreach (Transform chlid in _parentTransform)
        {
            // 見つけた子オブジェクトのローカル座標を保存
            int verticalAxis = Mathf.FloorToInt(-chlid.localPosition.y);
            int horizontalAxis = Mathf.FloorToInt(chlid.localPosition.x);

            // タグでオブジェクトを判断して配列に格納
            switch (chlid.tag)
            {

                case "Red":
                    _stage[verticalAxis, horizontalAxis] = RED;
                    _isRedGoal = true;
                    break;

                case "Blue":
                    _stage[verticalAxis, horizontalAxis] = BULE;
                    _isBlueGoal = true;
                    break;

                case "Green":
                    _stage[verticalAxis, horizontalAxis] = GREEN;
                    _isGreenGoal = true;
                    break;

                case "Yellow":
                    _stage[verticalAxis, horizontalAxis] = YELLOW;
                    _isYellowGoal = true;
                    break;

                case "Red_Ruins":
                    _stage[verticalAxis, horizontalAxis] = RED_GOAL;
                    _redRuinsXPos = horizontalAxis;
                    _redRuinsYPos = verticalAxis;
                    break;

                case "Blue_Ruins":
                    _stage[verticalAxis, horizontalAxis] = BULE_GOAL;
                    _blueRuinsXPos = horizontalAxis;
                    _blueRuinsYPos = verticalAxis;
                    break;

                case "Green_Ruins":
                    _stage[verticalAxis, horizontalAxis] = GREEN_GOAL;
                    _greenRuinsXPos = horizontalAxis;
                    _greenRuinsYPos = verticalAxis;
                    break;

                case "Yellow_Ruins":
                    _stage[verticalAxis, horizontalAxis] = YELLOW_GOAL;
                    _yellowRuinsXPos = horizontalAxis;
                    _yellowRuinsYPos = verticalAxis;
                    break;

                case "Wall":
                    _stage[verticalAxis, horizontalAxis] = WALL;
                    break;

                default:
                    break;

            }
        }
    }

    /// <summary>
    /// 赤ブロックのゴール判定処理
    /// </summary>
    /// <param name="xPos">赤ブロックのX座標</param>
    /// <param name="yPos">赤ブロックのY座標</param>
    private void Red(int xPos, int yPos)
    {
        if (_redRuinsXPos == xPos && _redRuinsYPos == yPos)
        {
            _isRed = true;
        }
        else
        {
            _isRed = false;
        }

    }
        /// <summary>
        /// 青ブロックのゴール判定処理
        /// </summary>
        /// <param name="xPos">青ブロックのX座標</param>
        /// <param name="yPos">青ブロックのY座標</param>
        private void Blue(int xPos,int yPos)
    {
        if (_blueRuinsXPos == xPos && _blueRuinsYPos == yPos)
        {
            _isBlue = true;
        }
        else
        {
            _isBlue = false;
        }
    }

    /// <summary>
    /// 緑ブロックのゴール判定処理
    /// </summary>
    /// <param name="xPos">緑ブロックのX座標</param>
    /// <param name="yPos">緑ブロックのY座標</param>
    private void Green(int xPos, int yPos)
    {
        if (_greenRuinsXPos == xPos && _greenRuinsYPos == yPos)
        {
            _isGreen = true;
        }
        else
        {
            _isGreen = false;
        }
    }

    /// <summary>
    /// 黄色ブロックのゴール判定処理
    /// </summary>
    /// <param name="xPos">黄色ブロックのX座標</param>
    /// <param name="yPos">黄色ブロックのY座標</param>
    private void Yellow(int xPos, int yPos)
    {
        if (_yellowRuinsXPos == xPos && _yellowRuinsYPos == yPos)
        {
            _isYellow = true;
        }
        else
        {
            _isYellow = false;
        }
    }

    /// <summary>
    /// X座標の小数点の処理
    /// </summary>
    /// <param name="xPos">加工前のX座標</param>
    /// <returns>加工後のX座標</returns>
    private int XPosFairing(float xPos)
    {
        // 閾値以上なら切り上げ
        if (xPos - Mathf.FloorToInt(xPos) > THRESHOLD)
        {
            xPos = Mathf.FloorToInt(xPos) + ROUNDING_UP;
        }
        // 以下なら切り捨て
        else
        {
            xPos = Mathf.FloorToInt(xPos) ;
        }
        return (int)xPos;
    }

    /// <summary>
    /// Y座標の小数点の処理
    /// </summary>
    /// <param name="yPos">加工前のY座標</param>
    /// <returns>加工後のY座標</returns>
    private int YPosFairing(float yPos)
    {
        // 閾値以上なら切り上げ
        if (yPos - Mathf.FloorToInt(yPos) > THRESHOLD)
        {
            yPos = Mathf.FloorToInt(yPos) + ROUNDING_UP;
        }
        // 以下なら切り捨て
        else
        {
            yPos = Mathf.FloorToInt(yPos);
        }
        return (int)yPos;
    }

    /// <summary>
    /// ゴール判定処理
    /// </summary>
    private void IsFin()
    {
        // 全ての位置が合っていたらゴール
        if(_isRed == _isRedGoal && _isBlue && _isBlueGoal && _isGreen == _isGreenGoal && _isYellow == _isYellowGoal)
        {
            _isGoal = true;
        }
    }

    #endregion

}
