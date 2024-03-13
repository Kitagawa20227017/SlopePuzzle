// ---------------------------------------------------------  
// GameManager.cs  
//   
// ゲームマネージャー
// 
// 作成日: 2024/3/11
// 作成者: 北川 稔明
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    #region 変数  

    [SerializeField, Header("クリアオブジェクト")]
    private GameObject _clearObj = default;

    [SerializeField,Header("ポーズオブジェクト")]
    private GameObject _pauseObj = default;

    [SerializeField, Header("一時停止オブジェクト")]
    private GameObject _stopObj = default;

    // SearchStage格納
    private SearchStage _searchStage = default;

    #endregion

    #region プロパティ  
    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start ()
    {
        // 初期化処理
        _searchStage = GameObject.Find("Stage").GetComponent<SearchStage>();
        Time.timeScale = 1;
        _clearObj.SetActive(false);
        _pauseObj.SetActive(false);
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update ()
    {
        // クリア処理
        if(_searchStage.IsGoal)
        {
            Time.timeScale = 0;
            _stopObj.SetActive(false);
            _clearObj.SetActive(true);
        }
    }

    #endregion

}
