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

    #region �̱���
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

    #region �����÷��� �ʱ�ȭ �� �α���
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
        //.EnableSavedGames() // ����� ���� Ȱ��ȭ
        .RequestIdToken()
        .Build();

        PlayGamesPlatform.InitializeInstance(config);
        // ���� �÷��� �α׸� Ȯ���ҷ��� Ȱ��ȭ
        PlayGamesPlatform.DebugLogEnabled = false;
        // ���� �÷��� Ȱ��ȭ
        PlayGamesPlatform.Activate();

        auth = FirebaseAuth.DefaultInstance;

#elif UNITY_IOS
        SocialSignIn();
#endif
    }

    // �α���
    public void SocialSignIn()
    {
        if (!Social.localUser.authenticated) // �α��� �Ǿ� ���� �ʴٸ� 
        {
            Social.localUser.Authenticate(success => // �α��� �õ� 
            {
                if (success) // �����ϸ� 
                {
                    Debug.Log("�α��� ����");
#if UNITY_IOS
                GameCenterPlatform.ShowDefaultAchievementCompletionBanner(true);
#endif
                    Txtflag.text = "����";
                    TxtId.text = Social.localUser.id;
                    TxtName.text = Social.localUser.userName;
                    StartCoroutine(TryFirebaseLogin()); // Firebase Login �õ� 
                }
                else // �����ϸ� 
                {
                    Debug.Log("�α��� ����");
                    Txtflag.text = "����";
                }
            });
        }
    }
    // �α׾ƿ�
    public void SocialSingOut()
    {
        if (Social.localUser.authenticated) // �α��� �Ǿ� �ִٸ� 
        { 
            PlayGamesPlatform.Instance.SignOut(); // Google �α׾ƿ� 
            auth.SignOut(); // Firebase �α׾ƿ� 
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

    #region ��������
    public void ShowMainLeaderBoard()
    {
        if (Social.localUser.authenticated == false)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    // Sign In ����
                }
                else
                {
                    // Sign In ���� 
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

    #region ����
    public void ShowAchievement()
    {
        if (Social.localUser.authenticated == false)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    // Sign In ����
                    return;
                }
                else
                {
                    // Sign In ���� ó��
                    return;
                }
            });
        }

        Social.ShowAchievementsUI();
    }
    #endregion
}