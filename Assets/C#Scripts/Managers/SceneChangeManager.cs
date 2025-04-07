using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChangeManager : SingleTons<SceneChangeManager>
{
    public GameObject Boss;
    private SceneData Nowscene;
    private SceneData Nextscene;
    public PlayerCanvs playerCanvs;
    public BossCanvs Bosscanvs;
    public FadeCanvs Fadecanvs;
    [Header("广播")]
    public BallTypeEventSO BallTypeEvent;
    public VoidEventSO CloseBossDoorEvent;
    public VoidEventSO CloseCardDoorEvent;
    [Header("临时场景保存")]
    public SceneData TempNextScene;
    private void OnEnable()
    {
        ChangeScene(TempNextScene);
    }
    public void ChangeScene(SceneData NowScene, SceneData NextScene)
    {
        Nowscene = NowScene;
        Nextscene = NextScene;
        StartCoroutine(OnChangeScene());
    }
    private IEnumerator OnChangeScene()
    {
        KeyBoardManager.Instance.StopAnyKey = true;
        Fadecanvs.FadeIn();
        yield return new WaitForSeconds(0.5f);
        switch (Nowscene.SceneName)
        {
            case "BossRoom":
                CloseBossDoorEvent.RaiseEvent();
                GameManager.Instance.AddBossHealth();
                Bosscanvs.gameObject.SetActive(false);
                break;
            case "CardRoom":
                CloseCardDoorEvent.RaiseEvent();
                break;
            case "StartRoom":
                playerCanvs.gameObject.SetActive(true);
                Bosscanvs.gameObject.SetActive(true);
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(0.1f);
        SceneManager.UnloadSceneAsync(Nowscene.SceneName,UnloadSceneOptions.None);
        SceneManager.LoadSceneAsync(Nextscene.SceneName,LoadSceneMode.Additive);
        yield return new WaitForSeconds(0.1f);
        switch (Nextscene.SceneName)
        {
            case "BossRoom":
                GameManager.Instance.RefreshBossSkill();
                Bosscanvs.gameObject.SetActive(true);
                Bosscanvs.ClearBossHealth();
                Bosscanvs.RefreshHealth();
                Boss.SetActive(true);
                GameManager.Instance.BossActive = true;
                Boss.transform.position = new Vector3(25.42f, 7.6f, 0);
                Boss.GetComponent<BossController>().IsStopBoss = true;
                break;
            case "CardRoom":
                BallTypeEvent.BallTypeRaiseEvent(GameManager.Instance.ThisBallType);
                break;
            case "StartRoom":
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(0.4f);
        Fadecanvs.FadeOut();
        yield return new WaitForSeconds(0.5f);
        KeyBoardManager.Instance.StopAnyKey = false;
        Boss.GetComponent<BossController>().IsStopBoss = false;
    }
    public void ChangeScene(SceneData NextScene)
    {
        Nextscene = NextScene;
        SceneManager.LoadSceneAsync(Nextscene.SceneName, LoadSceneMode.Additive);
    }
}
