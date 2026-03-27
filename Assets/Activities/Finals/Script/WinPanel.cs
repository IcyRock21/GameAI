using UnityEngine;

public class WinPanel : MonoBehaviour
{
    [SerializeField] GameObject bluePanel;
    [SerializeField] GameObject redPanel;

    public void BlueWin()
    {
        bluePanel.SetActive(true);
        redPanel.SetActive(false);
    }

    public void RedWin()
    {
        bluePanel.SetActive(false);
        redPanel.SetActive(true);
    }
}
