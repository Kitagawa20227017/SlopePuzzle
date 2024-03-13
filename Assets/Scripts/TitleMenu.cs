// ---------------------------------------------------------  
// TitleMenu.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class TitleMenu : MonoBehaviour
{

    #region 変数  

    [SerializeField]
    private GameObject _stageUI = default;

    #endregion

    #region プロパティ  
    #endregion

    #region メソッド  
     
    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start ()
    {
        _stageUI.SetActive(false);
    }


    public void ClickStage()
    {
        _stageUI.SetActive(true);
        gameObject.SetActive(false);
    }

    #endregion

}
