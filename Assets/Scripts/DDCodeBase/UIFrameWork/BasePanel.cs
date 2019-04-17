using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasePanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    public virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if(!canvasGroup)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public virtual void OnEnter()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public virtual void OnPause()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = false;
    }

    public virtual void OnResume()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public virtual void OnExit()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

    protected Button FindBtn(string btnName)
    {
        Button btn = null;

        foreach (var item in GetComponentsInChildren<Button>())
        {
            if(item.name == btnName)
            {
                btn = item;
            }
        }

        return btn;
    }
}
