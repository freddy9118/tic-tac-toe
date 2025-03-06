using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.SocialPlatforms.Impl;

public class NetworkManage : Singleton<NetworkManage>
{
    protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
    }
    
    public IEnumerator Signup(SignupData signupData, Action success, Action failure)
    {
        string jsonString = JsonUtility.ToJson(signupData);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);

        using (UnityWebRequest www =
               new UnityWebRequest(Constants.ServerURL + "/users/signup", UnityWebRequest.kHttpVerbPOST))
        {
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError ||
                www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("Error: " + www.error);

                if (www.responseCode == 409)
                {
                    // TODO: 중복 사용자 생성 팝업 표시
                    Debug.Log("중복사용자");
                    GameManager.Instance.OpenConfirmPanel("이미 존재하는 사용자입니다.", () =>
                    {
                        failure?.Invoke();
                    });
                }
            }
            else
            {
                var result = www.downloadHandler.text;
                Debug.Log("Result: " + result);
                
                // 회원가입 성공 팝업 표시
                GameManager.Instance.OpenConfirmPanel("회원 가입이 완료 되었습니다.", () =>
                {
                    success?.Invoke();
                });
            }
        }
    }
    
    public IEnumerator Signin(SigninData signinData, Action success, Action<int> failure)
    {
        string jsonString = JsonUtility.ToJson(signinData);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);

        using (UnityWebRequest www =
               new UnityWebRequest(Constants.ServerURL + "/users/signin", UnityWebRequest.kHttpVerbPOST))
        {
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError ||
                www.result == UnityWebRequest.Result.ProtocolError)
            {
                
            }
            else
            {
<<<<<<< HEAD
                var cookie = www.GetResponseHeader("Set-Cookie");
=======
                var cookie = www.GetResponseHeader("set-cookie");
>>>>>>> upstream/main
                if (!string.IsNullOrEmpty(cookie))
                {
                    int lastIndex = cookie.LastIndexOf(";");
                    string sid = cookie.Substring(0, lastIndex);
<<<<<<< HEAD
                    Debug.Log(sid);
                    PlayerPrefs.SetString("sid", sid);
=======
                    PlayerPrefs.SetString("sid", sid); 
>>>>>>> upstream/main
                }
                
                var resultString = www.downloadHandler.text;
                var result = JsonUtility.FromJson<SigninResult>(resultString);

                if (result.result == 0)
                {
                    // 유저네임 유효하지 않음
                    GameManager.Instance.OpenConfirmPanel("유저네임이 유효하지 않습니다.", () =>
                    {
                        failure?.Invoke(0);
                    });
                }
                else if (result.result == 1)
                {
                    // 패스워드가 유효하지 않음
                    GameManager.Instance.OpenConfirmPanel("패스워드가 유효하지 않습니다.", () =>
                    {
                        failure?.Invoke(1);
                    });
                }
                else if (result.result == 2)
                {
                    // 성공
                    GameManager.Instance.OpenConfirmPanel("로그인에 성공하였습니다.", () =>
                    {
                        success?.Invoke();
                    });
                }
            }
        }
    }

    public IEnumerator GetScore(Action<ScoreResult> success, Action failure)
    {
        using (UnityWebRequest www =
               new UnityWebRequest(Constants.ServerURL + "/users/score", UnityWebRequest.kHttpVerbGET))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
<<<<<<< HEAD
=======
            
>>>>>>> upstream/main
            string sid = PlayerPrefs.GetString("sid", "");
            if (!string.IsNullOrEmpty(sid))
            {
                www.SetRequestHeader("Cookie", sid);
            }
<<<<<<< HEAD
            yield return www.SendWebRequest();
=======

            yield return www.SendWebRequest();

>>>>>>> upstream/main
            if (www.result == UnityWebRequest.Result.ConnectionError ||
                www.result == UnityWebRequest.Result.ProtocolError)
            {
                if (www.responseCode == 403)
                {
                    Debug.Log("로그인이 필요합니다.");
                }
<<<<<<< HEAD
=======
                
>>>>>>> upstream/main
                failure?.Invoke();
            }
            else
            {
                var result = www.downloadHandler.text;
                var userScore = JsonUtility.FromJson<ScoreResult>(result);
<<<<<<< HEAD
                Debug.Log(userScore.score);
                Debug.Log("자동 로그인 성공");
=======
                
                Debug.Log(userScore.score);
                
>>>>>>> upstream/main
                success?.Invoke(userScore);
            }
        }
    }

<<<<<<< HEAD
    public IEnumerator SetScore(AddScore addScore, Action success, Action failure)
    {
        string jsonString = JsonUtility.ToJson(addScore);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);

        using (UnityWebRequest www =
               new UnityWebRequest(Constants.ServerURL + "/users/addscore", UnityWebRequest.kHttpVerbPOST))
        {
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
=======
    public IEnumerator GetLeaderboard(Action<Scores> success, Action failure)
    {
        using (UnityWebRequest www =
               new UnityWebRequest(Constants.ServerURL + "/leaderboard", UnityWebRequest.kHttpVerbGET))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            
            string sid = PlayerPrefs.GetString("sid", "");
            if (!string.IsNullOrEmpty(sid))
            {
                www.SetRequestHeader("Cookie", sid);
            }
>>>>>>> upstream/main

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError ||
                www.result == UnityWebRequest.Result.ProtocolError)
            {
<<<<<<< HEAD
                Debug.Log("Error: " + www.error);

                if (www.responseCode == 400)
                {
                    // 중복 사용자 생성 팝업 표시
                    Debug.Log("유효한 정보를 입력하세요");
                    failure?.Invoke();
                }
            }
            else
            {
                var resultString = www.downloadHandler.text;
                Debug.Log("점수 : " );
=======
                if (www.responseCode == 403)
                {
                    Debug.Log("로그인이 필요합니다.");
                }
                
                failure?.Invoke();
            }
            else
            {
                var result = www.downloadHandler.text;
                var scores = JsonUtility.FromJson<Scores>(result);
                
                success?.Invoke(scores);
>>>>>>> upstream/main
            }
        }
    }
}