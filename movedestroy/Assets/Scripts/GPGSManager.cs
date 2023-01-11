using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GPGSManager : MonoBehaviour
{
    public TextMeshProUGUI txtResult;
    public TextMeshProUGUI txtVersion;
    public TextMeshProUGUI txtIdToken;

    private void Start()
    {
        this.txtVersion.text = Application.version;
        GPGSInit();
    }

    #region GPGS 로그인
    private void GPGSInit()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        // enables saving game progress.
        //.EnableSavedGames()
        // requests the email address of the player be available.
        // Will bring up a prompt for consent.
        //.RequestEmail()
        // requests a server auth code be generated so it can be passed to an
        //  associated back end server application and exchanged for an OAuth token.
        //.RequestServerAuthCode(false)
        // requests an ID token be generated.  This OAuth token can be used to
        //  identify the player to other services such as Firebase.
        .RequestIdToken()
        .Build();

        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
    }

    // gpgs로그인시도
    private void GPGSSignIn()
    {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) => {
            // handle results
            this.txtResult.text = result.ToString();
            this.txtIdToken.text = ((PlayGamesLocalUser)Social.localUser).GetIdToken();
        });
    }

    // gpgs로그아웃 시도
    private void GPGSSingOut()
    {
        PlayGamesPlatform.Instance.SignOut();
    }
   
    #endregion
}
