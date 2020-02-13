using UnityEngine;
using System.Collections;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    public void ToggleLoadingPanel(bool activate)
    {
        panel.SetActive(activate);
    }
}
