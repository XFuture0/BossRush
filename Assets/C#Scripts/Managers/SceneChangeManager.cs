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
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync(Nextscene.SceneName,LoadSceneMode.Additive);
        yield return new WaitForSeconds(0.1f);
        switch (Nextscene.RoomType)
        {
            case RoomType.Boss:
                GameManager.Instance.RefreshBossSkill();
                Bosscanvs.ClearBossHealth();
                Bosscanvs.RefreshHealth();
                Boss.SetActive(true);
                GameManager.Instance.BossActive = true;
                Boss.transform.position = new Vector3(25.42f, 7.6f, 0);
                Boss.GetComponent<BossController>().IsStopBoss = true;
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
}
