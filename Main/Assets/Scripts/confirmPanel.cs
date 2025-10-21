using UnityEngine;

public class confirmPanel : MonoBehaviour
{
    [SerializeField] private GameObject confirmCanvas;

    public void onQuitClick()
    {
        confirmCanvas.gameObject.SetActive(true);
    }

    public void yesBtnClick ()
    {
        Application.Quit();
        Debug.Log("Quiting...");
    }

    public void noBtnClick()
    {
        confirmCanvas.gameObject.SetActive(false);
    }

}
