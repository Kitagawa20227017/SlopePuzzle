// ---------------------------------------------------------  
// Pause.cs  
//   
// ポーズUI処理
//
// 作成日: 2024/3/11
// 作成者: 北川 稔明
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    #region 変数  

    [SerializeField]
    private GameObject _pauseMenu = default;

    [SerializeField]
    private Sprite[] _sprites = default;

    [SerializeField]
    private Button _pauseButton = default;

    // 現在のシーン名格納用
    private string _sceneName = default;

    // 
    private bool _isStop = false;

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
        _sceneName = SceneManager.GetActiveScene().name;
    }

    public void ClickPause()
    {
        if(!_isStop)
        {
            Time.timeScale = 0;
            _isStop = !_isStop;
            _pauseButton.image.sprite = _sprites[0];
            _pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            _isStop = !_isStop;
            _pauseButton.image.sprite = _sprites[1];
            _pauseMenu.SetActive(false);
        }
    }

    public void ClickRetry()
    {
        SceneManager.LoadScene(_sceneName);
    }

    public void ClickHome()
    {
        SceneManager.LoadScene("Title");
    }

    #endregion

}
