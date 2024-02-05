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
    private List<Action> tutorialActionList;

    [Header("Ʃ�丮�� �̺�Ʈ")]
    public UnityEvent onTutorialStartTrigger;
    public UnityEvent onTutorialEndTrigger;

    [Header("�б� �̺�Ʈ")]
    public Action onQuaterStartTrigger = null;
    public Action onQuaterEndTrigger = null;

    [Header("����׿� ����")]
    public GameObject dummyEnemyPrefab;
    public CinemachineVirtualCamera enemyZoomCam;
    public GameObject triggerVolumeParticlePrefab;
    public GameObject triggerVolumeInParticlePrefab;


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
        tutorialActionList.Add(SkillTutorial);
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

        onTutorialStartTrigger?.Invoke();
        StartCoroutine(DebugInputCoroutine("��ȭâ", () => Input.GetKeyDown(KeyCode.Space), false, true));

        //��ȭâ�� ���ͼ� ������ �Ѵ�.
        //��ȭâ�� ���� ��쿡�� �б� ������ �˸���.
        //������ �׵��� ������ �����ϱ� �׳� ����� ������ �Է¹޾� �Ѱ��ִ� ģ�� �ϳ��� �ȴ�.
    }


    public void TutorialEnd()
    {
        //EpisodeManager.Instanace.EpisodeEndEvent -= QuaterStart;

        //Ʃ�丮���� ������ ��쿡�� ���Ǽҵ尡 ����Ǿ��� ���� �κ�� �����־���Ѵ�.

        onTutorialEndTrigger?.Invoke();

        SceneManager.LoadScene("SampleScene");
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
        print("QuaterStart");
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

        //���⵵ ��ȭâ ����ָ� �ǰڴ�
        StartCoroutine(DebugInputCoroutine("���� ����", () =>
            Keyboard.current.spaceKey.wasPressedThisFrame
        , false));
        onQuaterEndTrigger?.Invoke();
    }

    #region Tutorial Actions

    private void MoveTutorial()
    {
        print("Let's move");

        TutorialTriggerObject tto = new GameObject().AddComponent<TutorialTriggerObject>();
        tto.gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
        tto.gameObject.name = "triggerVolumeMove";
        tto.transform.position = new Vector3(34.01f, -5.51f);
        tto.effect = triggerVolumeInParticlePrefab;

        GameObject obj = Instantiate(triggerVolumeParticlePrefab, tto.transform);

        triggerVolume = tto;

        StartCoroutine(DebugInputCoroutine("��������!", () => triggerVolume.playerIsInTriggered));
    }

    private void JumpTutorial()
    {
        print("Let's jump");

        TutorialTriggerObject tto = new GameObject().AddComponent<TutorialTriggerObject>();
        
        triggerVolume = tto;
        tto.gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
        tto.gameObject.name = "triggerVolumeJump";
        tto.transform.position = new Vector3(58.49f, 2.51f);
        tto.effect = triggerVolumeInParticlePrefab;
        
        GameObject obj = Instantiate(triggerVolumeParticlePrefab, tto.transform);
        
        StartCoroutine(DebugInputCoroutine("����!", () =>
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

        StartCoroutine(DebugInputCoroutine("���� �����߷���!", () =>
        {
            print(_curEnemy.transform.GetComponent<Health>().isDead);
            bool dead = _curEnemy.transform.GetComponent<Health>().isDead;
            if (dead)
            {
                _curEnemy = null;
            }
            return dead;
        }, true, true));
    }


    private void SkillTutorial()
    {
        print("Let's skill");

        _curEnemy = Instantiate(dummyEnemyPrefab);
        _curEnemy.transform.position = new Vector3(106f, -3.13f, 0);

        StartCoroutine(DebugInputCoroutine("��ų�� ����ϼ���!", () =>
            _curEnemy.transform.GetComponent<Health>().isDead
        ,true,true));
    }
    #endregion

    private IEnumerator DebugInputCoroutine(string message, Func<bool> action, bool moveable = true, bool phaseClear = false)
    {
        print(message);
        if(!moveable) GameManager.Instance.Player.PlayerInput._controls.Player.Disable(); 

        yield return new WaitUntil(()=> action.Invoke());
        
        if(phaseClear) curPhaseCleared = true;

        GameManager.Instance.Player.PlayerInput._controls.Player.Enable();
    }
}
