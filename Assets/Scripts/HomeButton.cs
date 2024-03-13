// ---------------------------------------------------------  
// HomeButton.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class HomeButton : MonoBehaviour
{

    #region 変数  

    [SerializeField]
    private GameObject _titleObj = default;

    #endregion

    #region メソッド  

    public void ClickHome()
    {
        _titleObj.SetActive(true);
        gameObject.SetActive(false);
    }

    #endregion

}
