using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Send : MonoBehaviour
{
    public GameObject settings;
    public GameObject panel;
    public TMP_InputField userEmail;
    public TMP_Dropdown dropdown;
    public TMP_InputField content;
    private Button button;
    void Start()
    {
        button=GetComponent<Button>();
        button.onClick.AddListener(SendEmail);
    }


    void SendEmail()
    {
        userEmail.text = "";
        content.text = "";
        dropdown.value = 0;
        panel.SetActive(false);
        settings.SetActive(true);

        Debug.Log("sent");
    }
}
