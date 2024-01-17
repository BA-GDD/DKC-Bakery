using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
using UnityEngine.SceneManagement;

public class BattleTutorial : Stage
{
    private List<Action> tutorialActionList;

    [Header("튜토리얼 이벤트")]
    public UnityEvent onTutorialStartTrigger;
    public UnityEvent onTutorialEndTrigger;

    [Header("분기 이벤트")]
    public Action onQuaterStartTrigger;
    public Action onQuaterEndTrigger;

    [Header("디버그용 변수")]
    public GameObject dummyEnemyPrefab;
    public CinemachineVirtualCamera enemyZoomCam;

    protected override void Awake()
    {
        base.Awake();

        tutorialActionList = new List<Action>();

        #region Add Action to List
        tutorialActionList.Add(MoveTutorial);
        tutorialActionList.Add(JumpTutorial);
        tutorialActionList.Add(AttackTutorial);
        tutorialActionList.Add(SkillTutorial);
        #endregion

        onQuaterStartTrigger += tutorialActionList[0];
    }

    public void TutorialStart()
    {
        //EpisodeManager.Instanace.StartEpisode();
        EpisodeManager.Instanace.EpisodeEndEvent += QuaterStart;
        onTutorialStartTrigger?.Invoke();

        //대화창이 나와서 설명을 한다.
        //대화창이 끝날 경우에는 분기 종료를 알린다.
    }

    public void TutorialEnd()
    {
        EpisodeManager.Instanace.EpisodeEndEvent -= QuaterStart;

        //튜토리얼이 끝났을 경우에는 에피소드가 종료되었을 때에 로비로 보내주어야한다.
        onTutorialEndTrigger?.Invoke();

        PhaseCleared();
        //SceneManager.LoadScene("LobbyScene");
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
        onQuaterStartTrigger?.Invoke();

        onQuaterStartTrigger = null;
        onQuaterStartTrigger += tutorialActionList[curPhase];
    }


    #region Tutorial Actions

    private void MoveTutorial()
    {
        print("Let's move");
    }

    private void JumpTutorial()
    {
        print("Let's jump");
    }

    private void AttackTutorial()
    {
        print("Let's attack");
    }

    private void SkillTutorial()
    {
        print("Let's skill");
    }

    #endregion
}
