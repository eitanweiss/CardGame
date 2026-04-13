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

    List<GameObject> ObjectsToSwap; // List of objects to show/hide based on tab selection

    /// <summary>
    /// Adds a TabButton to the list of tabs in the TabGroup.
    /// </summary>
    /// <param name="tab">The TabButton to subscribe.</param>
    public void Subscribe(TabButton tab)
    {
        if (tabs == null)
        {
            tabs = new List<TabButton>();
        }
        tabs.Add(tab);
    }

    /// <summary>
    /// Changes the tab appearance to the hover state when the mouse enters.
    /// </summary>
    /// <param name="tab">The TabButton being hovered over.</param>
    public void OnTabEnter(TabButton tab)
    {
        ResetTabs();
        if (selectedTab == null || tab != selectedTab)
        {
            tab.image.sprite = hoverTab;
        }
    }

    /// <summary>
    /// Resets the tab appearance when the mouse exits the tab.
    /// </summary>
    /// <param name="tab">The TabButton that the mouse has exited.</param>
    public void onTabExit(TabButton tab)
    {
        ResetTabs();
    }

    /// <summary>
    /// Selects a tab, sets its appearance to the active state, and swaps the corresponding object.
    /// </summary>
    /// <param name="tab">The TabButton that has been clicked.</param>
    public void OnTabSelected(TabButton tab)
    {
        if (selectedTab != null)
        {
            selectedTab.Deselect();
        }

        selectedTab = tab;
        selectedTab.Select();

        ResetTabs();
        tab.image.sprite = activeTab;

        int index = tab.transform.GetSiblingIndex();
        for (int i = 0; i < ObjectsToSwap.Count; i++)
        {
            ObjectsToSwap[i].SetActive(i == index);
        }
    }

    /// <summary>
    /// Resets all tabs to their idle state, except for the selected tab.
    /// </summary>
    private void ResetTabs()
    {
        foreach (TabButton tabButton in tabs)
        {
            if (selectedTab != null && tabButton == selectedTab)
            {
                continue;
            }
            tabButton.image.sprite = idleTab;
        }
    }
}
