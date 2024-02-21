using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BakingTutorial : MonoBehaviour
{
    // ó�� �Ѵ� �Ÿ� �������ִ� �г��� ������

    [SerializeField]
    private List<GameObject> _tutorialPanelList;
    private int _curIdx = 0;
    
    private void EntryIntoBaking()
    {
        // ���� ���� ����ũ ó����� ��� ���ϴ� ���� ���
    }

    private void TurnOnThePanel(int currentIdx, int nextIdx)
    {
        _tutorialPanelList[currentIdx].SetActive(false);
        _tutorialPanelList[nextIdx].SetActive(true);
    }

    public void TurnThePanelToBefore()
    {
        if(_curIdx > 0)
        {
            TurnOnThePanel(_curIdx, --_curIdx);
        }
    }

    public void TurnThePanalToNext()
    {
        if(_curIdx < _tutorialPanelList.Count - 1)
        {
            TurnOnThePanel(_curIdx, ++_curIdx);
        }
    }

    private void CloseThePanel()
    {
        _tutorialPanelList[_curIdx].SetActive(false);
    }
}
