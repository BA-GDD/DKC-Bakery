using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BattleTutorial : Stage
{
    [SerializeField] private EpisodeDataListSO _battleTutorialSO;
    private List<Action> tutorialActionList;

    [Header("튜토리얼 이벤트")]
    public UnityEvent onTutorialStartTrigger;
    public UnityEvent onTutorialEndTrigger;

    [Header("분기 이벤트")]
    public Action onQuaterStartTrigger = null;
    public Action onQuaterEndTrigger = null;

    [Header("디버그용 변수")]
    public GameObject dummyEnemyPrefab;
    public CinemachineVirtualCamera enemyZoomCam;
    public TutorialTriggerObject triggerVolumeParticlePrefab;


    [HideInInspector]
    public TutorialTriggerObject triggerVolume = null;

    private GameObject _curEnemy;

    protected override void Awake()
    {
        base.Awake();

        tutorialActionList = new List<Action>();

        #region Add Action to List
        
        tutorialActionList.Add(MoveTutorial);
        tutorialActionList.Add(JumpTutorial);
        tutorialActionList.Add(AttackTutorial);
        tutorialActionList.Add(DashTutorial);
        tutorialActionList.Add(TutorialEnd);

        #endregion

        onQuaterStartTrigger += tutorialActionList[0];
        onQuaterEndTrigger += QuaterStart;

        
        TutorialStart();
    }

    public void TutorialStart()
    {
        //EpisodeManager.Instanace.StartEpisode();
        //EpisodeManager.Instanace.EpisodeEndEvent += QuaterStart;

        QuaterStart();
        int[] pauseIdx = { 14, 18, 22, 28, 36 };
        EpisodeManager.Instanace.StartEpisode(_battleTutorialSO, pauseIdx);
        //대화창이 나와서 설명을 한다.
        //대화창이 끝날 경우에는 분기 시작을 알린다.
        //하지만 그딴거 지금은 없으니까 그냥 디버그 용으로 입력받아 넘겨주는 친구 하나면 된다.
    }


    public void TutorialEnd()
    {
        //EpisodeManager.Instanace.EpisodeEndEvent -= QuaterStart;

        //튜토리얼이 끝났을 경우에는 에피소드가 종료되었을 때에 로비로 보내주어야한다.

        onTutorialEndTrigger?.Invoke();

        SceneManager.LoadScene("SampleScene");
    }

    //분기가 시작되었을 때에는 준비한 이벤트를 실행한다.
    //1. 움직이는 맵 내에서 움직이는 방법을 알려준다.
    //2. 점프 방법을 알려준다.
    //3. 적이 나온다. 같이 전투방법 또한 알려준다.
    //4. 기본 공격으로는 죽지 않는 적이 나온다.
    //5. 기본 제공되는 검기 스킬을 사용할 수 있도록 유도해준다.
    //6. 돌아간다?

    //분기가 시작되었을 때에는 대화창또한 나와야한다.
    //대화창은 튜토리얼 내용이 끝난 뒤 페이즈를 넘어갔을 때에 나온다고 볼 수 있다.
    private void QuaterStart()
    {
        Debug.Log(1);
        onQuaterStartTrigger?.Invoke();

        if (curPhase >= 1)
        {
            onQuaterStartTrigger -= tutorialActionList[curPhase - 1];
            onQuaterStartTrigger += tutorialActionList[curPhase];
        }
        else
        {
            onQuaterStartTrigger -= tutorialActionList[0];
            onQuaterStartTrigger += tutorialActionList[1];
        }
        
    }

    public void QuaterEnd(string message)
    {
        print(message);

        EpisodeManager.Instanace.SetPauseEpisode(false);
        StartCoroutine(DebugInputCoroutine("쿼터 종료", () =>
            Keyboard.current.spaceKey.wasPressedThisFrame
        , false));
        onQuaterEndTrigger?.Invoke();
    }

    #region Tutorial Actions

    private void MoveTutorial()
    {
        Debug.Log("Let's move");

        triggerVolume = Instantiate(triggerVolumeParticlePrefab, new Vector3(34.01f, -5.51f), Quaternion.identity);
        StartCoroutine(DebugInputCoroutine("움직이자!", () => triggerVolume.playerIsInTriggered));
    }

    private void JumpTutorial()
    {
        print("Let's jump");

        triggerVolume = Instantiate(triggerVolumeParticlePrefab, new Vector3(58.49f, 2.51f), Quaternion.identity);
        
        StartCoroutine(DebugInputCoroutine("뛰자!", () =>
            triggerVolume.playerIsInTriggered
        ));
    }

    private void AttackTutorial()
    {
        print("Let's attack");

        _curEnemy = Instantiate(dummyEnemyPrefab);
        _curEnemy.transform.position = new Vector3(82f, -3.13f, 0);

        StartCoroutine(ZoomToEnemy());
    }

    private void DashTutorial()
    {
        triggerVolume = Instantiate(triggerVolumeParticlePrefab, new Vector3(58.49f, 2.51f), Quaternion.identity);

        StartCoroutine(DebugInputCoroutine("뛰자!", () =>
            triggerVolume.playerIsInTriggered
        ));
    }

    private IEnumerator ZoomToEnemy()
    {
        int front = 15;
        int behind = 10;

        GameManager.Instance.Player.PlayerInput._controls.Player.Disable();

        enemyZoomCam.m_Priority = front;
        vCam.m_Priority = behind;

        yield return new WaitUntil(() => !Camera.main.GetComponent<CinemachineBrain>().IsBlending);
        yield return new WaitForSeconds(2.0f);

        enemyZoomCam.m_Priority = behind;
        vCam.m_Priority = front;

        yield return new WaitUntil(() => !Camera.main.GetComponent<CinemachineBrain>().IsBlending);

        GameManager.Instance.Player.PlayerInput._controls.Player.Disable();

        StartCoroutine(DebugInputCoroutine("적을 쓰러뜨려라!", () =>
        {
            print(_curEnemy.transform.GetComponent<Health>().isDead);
            bool dead = _curEnemy.transform.GetComponent<Health>().isDead;
            if (dead)
            {
                _curEnemy = null;
            }
            return dead;
        }, true));
    }
    #endregion

    private IEnumerator DebugInputCoroutine(string message, Func<bool> action, bool phaseClear = true)
    {
        print(message);
        GameManager.Instance.Player.PlayerInput._controls.Player.Disable();

        yield return new WaitUntil(() => EpisodeManager.Instanace.isInPause);
        GameManager.Instance.Player.PlayerInput._controls.Player.Enable();

        yield return new WaitUntil(()=> action.Invoke());
        Debug.Log("clear");
        if(phaseClear) curPhaseCleared = true;
    }
}
