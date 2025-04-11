using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChangeManager : SingleTons<SceneChangeManager>
{
    public GameObject Player;
    public GameObject Boss;
    private SceneData Nextscene;
    private Vector3 PlayerPosition;
    public BossCanvs Bosscanvs;
    public FadeCanvs Fadecanvs;
    public void ChangeScene(SceneData NextScene)
    {
        Nextscene = NextScene;
        StartCoroutine(OnChangeScene());
    }
    private IEnumerator OnChangeScene()
    {
        KeyBoardManager.Instance.StopAnyKey = true;
        Fadecanvs.FadeIn();
        ColorManager.Instance.ChangeColor();
     //   SceneManager.LoadSceneAsync(Nextscene.SceneName,LoadSceneMode.Additive);
        GameManager.Instance.RefreshBossSkill();
        Boss.SetActive(true);
        GameManager.Instance.BossActive = true;
        Boss.transform.position = new Vector3(-27.2f, 0.97f, 0);
        Player.transform.position = new Vector3(-15.04f, -0.4f, 0);
        Boss.GetComponent<BossController>().IsStopBoss = true;
        yield return new WaitForSeconds(0.5f);
        Fadecanvs.FadeOut();
        yield return new WaitForSeconds(0.5f);
        KeyBoardManager.Instance.StopAnyKey = false;
        Boss.GetComponent<BossController>().IsStopBoss = false;
    }
    public void StartGame()
    {
        StartCoroutine(OnStartGame());
    }
    private IEnumerator OnStartGame()
    {
        KeyBoardManager.Instance.StopAnyKey = true;
        Fadecanvs.FadeIn();
        ColorManager.Instance.ChangeColor();
        GameManager.Instance.RefreshPlayer();
        GameManager.Instance.RefreshBoss();
        GameManager.Instance.RefreshBossSkill();
        Boss.SetActive(true);
        GameManager.Instance.BossActive = true;
        Boss.transform.position = new Vector3(-27.2f, 0.97f, 0);
        Player.transform.position = new Vector3(-15.04f, -0.4f, 0);
        Boss.GetComponent<BossController>().IsStopBoss = true;
        yield return new WaitForSeconds(0.5f);
        Fadecanvs.FadeOut();
        yield return new WaitForSeconds(0.5f);
        KeyBoardManager.Instance.StopAnyKey = false;
        Boss.GetComponent<BossController>().IsStopBoss = false;
    }
}
