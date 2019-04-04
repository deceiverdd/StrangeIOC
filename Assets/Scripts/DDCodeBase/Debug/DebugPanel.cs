using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPanel : MonoBehaviour
{
    public delegate void HandleApplyEyesDistanceChange(float value);
    public static event HandleApplyEyesDistanceChange EventApplyEyesDistanceChange; 
    public GameObject DebugModePanel;

    public Slider SliderEyesDistance;
    public InputField InputEyesDistance;

    // Use this for initialization
    void Start()
    {
        if (!DebugModeCheck())
            return;

        InitData();
    }

    private bool DebugModeCheck()
    {
        if(!GameConfig.Instance.isDebugMode)
        {
            gameObject.SetActive(false);
            return false;
        }
        else
        {
            DebugModePanel.SetActive(true);
            return true;
        }
    }

    private void InitData()
    {

    }

    public void OnClickBtnOpenDebugPanel()
    {
        if (DebugModePanel.activeSelf)
            DebugModePanel.SetActive(false);
        else
            DebugModePanel.SetActive(true);
    }

    public void OnSliderEyesDistanceValueChanged()
    {
        InputEyesDistance.text = SliderEyesDistance.value.ToString();
    }

    public void OnInputEyesDistanceEditEnd()
    {
        try
        {
            float inputValue = float.Parse(InputEyesDistance.text);

            if (0f < inputValue && inputValue < 1f)
            {
                SliderEyesDistance.value = inputValue;
            }
            else
            {
                InputEyesDistance.text = SliderEyesDistance.value.ToString();
            }
        }
        catch

        {
            InputEyesDistance.text = SliderEyesDistance.value.ToString();
        }       
    }

    public void OnClickApplyChangedConfigData()
    {

    }

    public void OnClickSaveChangedConfigData()
    {
        OnClickApplyChangedConfigData();


    }
}
