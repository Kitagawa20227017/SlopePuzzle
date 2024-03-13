// ---------------------------------------------------------  
// MyScript2.cs  
//   
// 作成日:  
// 作成者:  
// ---------------------------------------------------------  
using UnityEngine;
using System.Collections;

public class MyScript2 : MonoBehaviour
{

    #region 変数  

    private Gyroscope m_gyro;
    #endregion

    #region プロパティ  
    #endregion

    #region メソッド  

    /// <summary>  
    /// 初期化処理  
    /// </summary>  
    private void Awake()
    {
        Input.gyro.enabled = true;
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
        m_gyro = Input.gyro;
        Debug.Log("X : " + m_gyro.rotationRate.x + "Y : " + m_gyro.rotationRate.y + "Z : " + m_gyro.rotationRate.z);
    }

    #endregion

}
