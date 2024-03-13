// ---------------------------------------------------------  
// StagePause.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class StagePause : MonoBehaviour
{

    #region 変数  

    [SerializeField]
    private GameObject _pauseMenu = default;

    private bool _isStop;

    #endregion

    #region メソッド  

    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    private void Awake()
    {
    }
     
    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start ()
    {
    }

    /// <summary>  
    /// 更新処理  
    /// </summary>  
    private void Update ()
    {
    }

    public void ClickHome()
    {

    }

    #endregion

}
