using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadeAway : MonoBehaviour
{
    public float speed;
    public float fadeTime;
    [SerializeField]
    TextMeshProUGUI fadeAwayText;
    private float alpha;
    private float fadePerSecond;
    //Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        //originalPosition= new Vector3 (0,320,0);
        fadePerSecond = 1 / fadeTime;
        alpha = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeTime > 0)
        {
            fadeAwayText.rectTransform.position = new Vector3(transform.position.x,transform.position.y + speed * Time.deltaTime,transform.position.z);
            alpha -= Time.deltaTime*fadePerSecond;
            fadeAwayText.color = new Color(fadeAwayText.color.r, fadeAwayText.color.g, fadeAwayText.color.b, alpha);
            fadeTime -= Time.deltaTime;
        }
    }
    public void ResetFadeAway()
    {
        //want text to appear a bit more to the top
        fadeAwayText.rectTransform.anchoredPosition = new Vector3(0, 320, 0);
        fadeTime = 2f;
        alpha = 1;
    }
}
