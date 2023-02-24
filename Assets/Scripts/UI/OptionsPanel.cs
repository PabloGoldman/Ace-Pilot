using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsPanel : MonoBehaviour
{
    public GameObject settingsPanel;

    Animator settingsPanelAnimator;

    bool inSettings = false;

    bool firstTrigger = true;

    // Start is called before the first frame update
    void Start()
    {
        settingsPanelAnimator = settingsPanel.GetComponent<Animator>();
    }

    public void TriggerSettingsPanel()
    {
        if (!firstTrigger)
        {
            if (inSettings)
            {
                settingsPanelAnimator.SetTrigger("Close");
                inSettings = false;
            }
            else
            {
                settingsPanelAnimator.SetTrigger("Open");
                inSettings = true;
            }
        }
        else
        {
            settingsPanel.SetActive(true);
            inSettings = true;
            firstTrigger = false;
        }
    }
}
