// ---------------------------------------------------------  
// HomeButton.cs  
//   
// ホームボタン処理
//
// 作成日: 2024/3/12
// 作成者: 北川 稔明
// ---------------------------------------------------------  
using UnityEngine;

public class HomeButton : MonoBehaviour
{

    #region 変数  

    [SerializeField,Header("Titleオブジェクト")]
    private GameObject _titleObj = default;

    #endregion

    #region メソッド  

    /// <summary>
    /// ホームボタンを押したときの処理
    /// </summary>
    public void ClickHome()
    {
        _titleObj.SetActive(true);
        gameObject.SetActive(false);
    }

    #endregion

}
