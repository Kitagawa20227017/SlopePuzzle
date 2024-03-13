// ---------------------------------------------------------  
// Clear.cs  
//   
// クリアUI処理
//
// 作成日: 2024/3/12 
// 作成者: 北川 稔明
// ---------------------------------------------------------  
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{

    #region 変数  

    #region const定数

    // 「Stage」の5文字分
    private const int STAGE_WORD_COUNT = 5;

    // タイトルシーン分
    private const int TITLE_SCENE = 1;

    #endregion

    // 遷移するシーン名
    private string _sceneName = default;


    #endregion

    #region メソッド  

    /// <summary>  
    /// 更新前処理  
    /// </summary>  
    private void Start ()
    {
        // 初期設定
        _sceneName = SceneManager.GetActiveScene().name;
        NextSceneName(SceneManager.sceneCountInBuildSettings);
    }

    /// <summary>
    /// Nextが押されたとき
    /// </summary>
    public void ClickNext()
    {
        SceneManager.LoadScene(_sceneName);
    }

    /// <summary>
    /// ホームボタンが押されたとき
    /// </summary>
    public void ClickHome()
    {
        SceneManager.LoadScene("Title");
    }

    /// <summary>
    /// 次のステージの検索処理
    /// </summary>
    /// <param name="sceneConut">シーンの合計数</param>
    /// <returns>次に遷移するシーン名</returns>
    private string NextSceneName(int sceneConut)
    {
        // シーン名の長さを入れる
        int nameConut = _sceneName.Length;

        // 全体文字数から「Stage」の５文字引いた数を代入
        int stageConut = int.Parse(_sceneName.Substring(STAGE_WORD_COUNT, nameConut - STAGE_WORD_COUNT));

        // 次のステージの数字
        stageConut++;

        // タイトルシーンを除いたシーンの合計との比較
        if (stageConut <= sceneConut - TITLE_SCENE)
        {
            _sceneName = _sceneName.Substring(0, STAGE_WORD_COUNT) + stageConut.ToString();
        }
        else
        {
            _sceneName = "Title";
        }

        return _sceneName;
    }
    #endregion

}
