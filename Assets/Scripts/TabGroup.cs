using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TabGroup : MonoBehaviour
{
    [SerializeField] List<TabButton> tabs;
    TabButton selectedTab;

    public Sprite hoverTab;
    public Sprite activeTab;
    public Sprite idleTab;

    List<GameObject> ObjectsToSwap;

    public void Subscribe(TabButton tab)
    {
        if(tabs==null)
        {
            tabs = new List<TabButton>();
        }
        tabs.Add(tab);
    }

    public void OnTabEnter(TabButton tab)
    {
        ResetTabs();
        if (selectedTab == null || tab != selectedTab)
        {
            tab.image.sprite = hoverTab;
        }
    }

    public void onTabExit(TabButton tab)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton tab)
    {
        if (selectedTab!=null)
        {
            selectedTab.Deselect();       
        }

        selectedTab = tab;
        selectedTab.Select();

        ResetTabs();
        tab.image.sprite=activeTab;
        int index = tab.transform.GetSiblingIndex();
        for(int i=0;i< ObjectsToSwap.Count; i++)
        {
            if(i==index)
            {
                ObjectsToSwap[i].SetActive(true);
            }
            else
            {
                ObjectsToSwap[i].SetActive(false);
            }
        }
    }


    private void ResetTabs()
    {
        foreach (TabButton tabButton in tabs)
        {
            if (selectedTab!=null && tabButton == selectedTab) 
            {
                continue;
            }
            tabButton.image.sprite = idleTab;
        }
    }
}
