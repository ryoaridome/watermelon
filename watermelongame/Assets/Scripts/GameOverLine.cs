using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using unityroom.Api;
using System;

public class GameOverLine : MonoBehaviour
{
    private float stayTime;
    [SerializeField] private GameObject GameOverText;
    [SerializeField] private GameObject ReStartButton;
    [SerializeField] private GameObject GameOverCanvas;
    [SerializeField] private Text txtScore;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("seed"))
        {
            //時間計測
            stayTime += Time.deltaTime;
            //1秒後にリスタートする
            if (stayTime > 1.0f)
            {
                GameManager.RestartOnClickCount = 1;
                //GameOverTextを非表示
                GameOverText.SetActive(true);
                //GameOverCanvasを非表示
                GameOverCanvas.SetActive(true);
                //GameOverCanvasを非表示
                ReStartButton.SetActive(false);
                // C#スクリプトの冒頭に `using unityroom.Api;` を追加してください。
                // ボードNo1にスコア123.45fを送信する。
                //1つ目の引数はボードNoです。unityroomの設定画面でスコアボード一覧画面で確認できます。
                //2つ目の引数はスコア(float)です。
                //3つ目の引数はスコアの記録ルールで３種類あります。unityroomで作成したスコアボードと同じ設定を選んでください。
                //HighScoreDesc：ハイスコア（降順）として記録する
                //HighScoreAsc：ハイスコア（昇順）として記録する
                //Always：ハイスコア（常に記録）として記録する
                UnityroomApiClient.Instance.SendScore(1, Convert.ToSingle(txtScore.text), ScoreboardWriteMode.Always);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("seed"))
        {
            //初期化
            stayTime = 0;
        }
    }
}
