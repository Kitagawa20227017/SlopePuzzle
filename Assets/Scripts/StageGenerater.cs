// ---------------------------------------------------------  
// StageGenerater.cs  
//   
// ステージ生成処理
//
// 作成日: 2024/3/11
// 作成者: 北川 稔明
// ---------------------------------------------------------  
using UnityEngine;

public class StageGenerater : MonoBehaviour
{

    #region 変数  

    #region const定数

    // 加工しないままだと配列外になるため調整する
     private const int MINUS_ONE = 1;

    #endregion

    [SerializeField,Header("テキストファイル")]
    private TextAsset _textAsset = default;

    [SerializeField,Header("ブロックオブジェクト")]
    private GameObject[] _blocks = default;

    [SerializeField,Header("Stageオブジェクト")]
    GameObject _stage = default;

    // テキストファイルの中身を格納
    private string[] _stageDetaText; 

    // テキストファイルを解析した結果を格納
    private int[,] _stageDeta;  

    // テキストの行数
    private int _rowLength; 

    // テキストの列数
    private int _columnLength;

    #endregion

    #region プロパティ  

    /// <summary>
    /// テキストの行数
    /// </summary>
    public int RowLength 
    { 
        get => _rowLength; 
        set => _rowLength = value; 
    }

    /// <summary>
    /// テキストの列数
    /// </summary>
    public int ColumnLength 
    {
        get => _columnLength; 
        set => _columnLength = value; 
    }
    
    #endregion

    #region メソッド  

    /// <summary>
    /// ステージ生成処理
    /// </summary>
    public void Generate()
    {
        // テキストファイルを読み込めるようにする
        string textName = _textAsset.name;
        _textAsset = new TextAsset();
        _textAsset = Resources.Load(textName, typeof(TextAsset)) as TextAsset;

        // テキストファイルの中身取得
        string TextLines = _textAsset.text;

        // 改行で区切って配列に格納
        _stageDetaText = TextLines.Split('\n');

        // 行、列数を取得
        ColumnLength = _stageDetaText[0].Length;
        RowLength = _stageDetaText.Length;

        // 配列の大きさを決める
        _stageDeta = new int[RowLength, ColumnLength - MINUS_ONE];

        // 1文字ずつ区切ってint型にした配列に代入
        for (int i = 0; i < RowLength; i++)
        {
            // 配列から文字を取り出す
            string s = _stageDetaText[i];
            for (int j = 0; j < ColumnLength - MINUS_ONE; j++)
            {
                // 文字からj番目を指定してstring型にしてからint型にする
                _stageDeta[i, j] = int.Parse(s[j].ToString());
            }
        }

        // ステージ生成
        for (int i = 0; i < _stageDeta.GetLength(0); i++)
        {
            for (int j = 0; j < _stageDeta.GetLength(1); j++)
            {
                // 0(空白)以外からその数字に合うオブジェクトを生成
                if (_stageDeta[i, j] - MINUS_ONE >= 0)
                {
                    Instantiate(_blocks[_stageDeta[i, j] - MINUS_ONE], new Vector2(_stage.transform.position.x + j, _stage.transform.position.y - i),
                        Quaternion.identity, _stage.transform);
                }
            }
        }
    }

    #endregion

}
