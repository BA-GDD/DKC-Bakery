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

    [Header("Ʃ�丮�� �̺�Ʈ")]
    public UnityEvent onTutorialStartTrigger;
    public UnityEvent onTutorialEndTrigger;

    [Header("�б� �̺�Ʈ")]
    public Action onQuaterStartTrigger;
    public Action onQuaterEndTrigger;

    [Header("����׿� ����")]
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

        //��ȭâ�� ���ͼ� ������ �Ѵ�.
        //��ȭâ�� ���� ��쿡�� �б� ���Ḧ �˸���.
    }

    public void TutorialEnd()
    {
        EpisodeManager.Instanace.EpisodeEndEvent -= QuaterStart;

        //Ʃ�丮���� ������ ��쿡�� ���Ǽҵ尡 ����Ǿ��� ���� �κ�� �����־���Ѵ�.
        onTutorialEndTrigger?.Invoke();

        PhaseCleared();
        //SceneManager.LoadScene("LobbyScene");
    }

    //�бⰡ ���۵Ǿ��� ������ �غ��� �̺�Ʈ�� �����Ѵ�.
    //1. �����̴� �� ������ �����̴� ����� �˷��ش�.
    //2. ���� ����� �˷��ش�.
    //3. ���� ���´�. ���� ������� ���� �˷��ش�.
    //4. �⺻ �������δ� ���� �ʴ� ���� ���´�.
    //5. �⺻ �����Ǵ� �˱� ��ų�� ����� �� �ֵ��� �������ش�.
    //6. ���ư���?

    //�бⰡ ���۵Ǿ��� ������ ��ȭâ���� ���;��Ѵ�.
    //��ȭâ�� Ʃ�丮�� ������ ���� �� ����� �Ѿ�� ���� ���´ٰ� �� �� �ִ�.
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
