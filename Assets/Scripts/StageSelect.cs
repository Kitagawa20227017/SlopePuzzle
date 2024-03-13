// ---------------------------------------------------------  
// StageSelect.cs  
//   
// ステージ選択処理
//
// 作成日: 2024/3/11
// 作成者: 北川 稔明
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{

    #region 変数  
    
    // ステージUI格納
    private GameObject _stageObj = default;

    #endregion

    #region メソッド  

    private void Start()
    {
        // 初期設定
        _stageObj = this.gameObject;
    }

    // ボタンがクリックされた時の処理
    public void OnButtonClick()
    {
        // ステージにシーン遷移
        SceneManager.LoadScene(_stageObj.name);
    }
    #endregion

}
