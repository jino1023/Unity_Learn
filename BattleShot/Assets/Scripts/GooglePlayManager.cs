using System.Collections;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
using UnityEngine.UI;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using Firebase.Auth;
#if UNITY_IOS
using UnityEngine.SocialPlatforms.GameCenter;
#endif

public class GooglePlayManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Txtflag;
    [SerializeField]
    private TextMeshProUGUI TxtName;
    [SerializeField]
    private TextMeshProUGUI TxtId;

    private FirebaseAuth auth;
    public string FireBaseId = string.Empty;

    #region 싱글톤
    private static GooglePlayManager _instance = null;

    public static GooglePlayManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (GooglePlayManager)FindObjectOfType(typeof(GooglePlayManager));
                if (_instance == null)
                {
                    Debug.Log("There's no active ManagerClass object");
                }
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    #region 구글플레이 초기화 및 로그인
    private void Start()
    {
        CheckSocialLogin();
    }

    public bool isProcessing
    {
        get;
        private set;
    }
    public string loadedData
    {
        get;
        private set;
    }

    public bool IsLogined => Social.localUser.authenticated;

    private void CheckSocialLogin()
    {
#if UNITY_ANDROID
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        //.EnableSavedGames() // 저장된 게임 활성화
        .RequestIdToken()
        .Build();

        PlayGamesPlatform.InitializeInstance(config);
        // 구글 플레이 로그를 확인할려면 활성화
        PlayGamesPlatform.DebugLogEnabled = false;
        // 구글 플레이 활성화
        PlayGamesPlatform.Activate();

        auth = FirebaseAuth.DefaultInstance;

#elif UNITY_IOS
        SocialSignIn();
#endif
    }

    // 로그인
    public void SocialSignIn()
    {
        if (!Social.localUser.authenticated) // 로그인 되어 있지 않다면 
        {
            Social.localUser.Authenticate(success => // 로그인 시도 
            {
                if (success) // 성공하면 
                {
                    Debug.Log("로그인 성공");
#if UNITY_IOS
                GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true);
#endif
                    Txtflag.text = "성공";
                    TxtId.text = Social.localUser.id;
                    TxtName.text = Social.localUser.userName;
                    StartCoroutine(TryFirebaseLogin()); // Firebase Login 시도 
                }
                else // 실패하면 
                {
                    Debug.Log("로그인 실패");
                    Txtflag.text = "실패";
                }
            });
        }
    }
    // 로그아웃
    public void SocialSingOut()
    {
        if (Social.localUser.authenticated) // 로그인 되어 있다면 
        { 
            PlayGamesPlatform.Instance.SignOut(); // Google 로그아웃 
            auth.SignOut(); // Firebase 로그아웃 
        } 
    }

    //try FriebaseLogin
    IEnumerator TryFirebaseLogin()
    {
        while (string.IsNullOrEmpty(((PlayGamesLocalUser)Social.localUser).GetIdToken())) 
            yield return null; 
        string idToken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();
        
        Credential credential = GoogleAuthProvider.GetCredential(idToken, null);
        auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            if (task.IsCanceled) 
            { 
                //SystemMessageManager.Instance.AddMessage("SignInWithCredentialAsync was canceled!!"); 
                return; 
            }
            if (task.IsFaulted) 
            { 
                //SystemMessageManager.Instance.AddMessage("SignInWithCredentialAsync encountered an error: " + task.Exception); 
                return; 
            }
            Firebase.Auth.FirebaseUser newUser = task.Result; 
            FireBaseId = newUser.UserId; //Debug.Log("Success!"); //SystemMessageManager.Instance.AddMessage("firebase Success!!");
            
        });
    }
    #endregion

    #region 리더보드
    public void ShowMainLeaderBoard()
    {
        if (Social.localUser.authenticated == false)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    // Sign In 성공
                }
                else
                {
                    // Sign In 실패 
                    return;
                }
            });
        }

#if UNITY_ANDROID
        //PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_sum_of_all_stage_score);
#elif UNITY_IOS
        GameCenterPlatform.ShowLeaderboardUI(GPGSIds.leaderboard_sum_of_all_stage_score, UnityEngine.SocialPlatforms.TimeScope.AllTime);
#endif
    }
    #endregion

    #region 업적
    public void ShowAchievement()
    {
        if (Social.localUser.authenticated == false)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    // Sign In 성공
                    return;
                }
                else
                {
                    // Sign In 실패 처리
                    return;
                }
            });
        }

        Social.ShowAchievementsUI();
    }
    #endregion
}