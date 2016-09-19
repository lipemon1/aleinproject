using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PensamentosManager : MonoBehaviour {

    string pensamentoAtual;
    public Text pensamentoText;
    public GameObject pensamentoIcon;
    public GameObject pensamentoHolder;

    public string PensamentoAtual
    {
        get
        {
            return pensamentoAtual;
        }

        set
        {
            pensamentoAtual = value;
        }
    }

    
    public bool PensamentoHolderAtivo;
    public bool MostrarPanelText;

    public GameObject pensamentoPanel;

    // Use this for initialization
    void Start () {
	    if(pensamentoIcon == null)
        {
            pensamentoIcon = GameObject.FindGameObjectWithTag("Pensamento/Icon");
        }
        if (pensamentoHolder == null)
        {
            pensamentoHolder = GameObject.FindGameObjectWithTag("Pensamento/Holder");
        }
        if (pensamentoText == null)
        {
            pensamentoText = GameObject.FindGameObjectWithTag("Pensamento/Text").GetComponent<Text>();
        }
        if (pensamentoPanel == null)
        {
            pensamentoPanel = GameObject.FindGameObjectWithTag("Pensamento/Panel");
        }
        //if(pensamentoHolder.active == true)
        //{
        //    //pensamentoHolder.SetActive(false);
        //    PensamentoHolderAtivo = true;
        //}
        EsconderPensamentos();
    }
	
	// Update is called once per frame
	void Update () {	  
        
        if(PensamentoHolderAtivo == true) {             
            if(pensamentoIcon.GetComponent<PensamentoBehaviour>().EstaSendoClicado == true)
            {
                MostrarPanelText = true;
            }
            else
            {
                MostrarPanelText = false;
            }
        }

        if (MostrarPanelText)
            ShowPanelText();
        else
            HidePanelText();

    }

    public void MostrarPensamento(string textoPensamento)
    {
        pensamentoText.text = "" + textoPensamento;
        ShowHolder();
    }
    public void EsconderPensamentos()
    {
        HideHolder();
    }

    public void ShowPanelText()
    {
        pensamentoPanel.SetActive(true);
    }
    public void HidePanelText()
    {
        pensamentoPanel.SetActive(false);
    }

    public void ShowHolder()
    {
        pensamentoHolder.SetActive(true);
        PensamentoHolderAtivo = true;
    }
    public void HideHolder()
    {
        pensamentoHolder.SetActive(false);
        PensamentoHolderAtivo = false;
    }
}
