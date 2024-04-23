using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    bool isP1 = true;
    [SerializeField] GameObject LilyImage;
    [SerializeField] GameObject Lily;

    [SerializeField] GameObject WilliamImage;

    [SerializeField] GameObject William;

    [SerializeField] GameObject NextScreen;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleCharSelect()
    {
        if (isP1 == true)
        {
            isP1 = false;
            LilyImage.SetActive(false);
            Lily.SetActive(false);

            WilliamImage.SetActive(true);
            William.SetActive(true);

        }
        else
        {
            isP1 = true;
            LilyImage.SetActive(true);
            Lily.SetActive(true);

            WilliamImage.SetActive(false);
            William.SetActive(false);

        }
    }

    public void SelectChar()
    {
        NextScreen.SetActive(true);
        gameObject.SetActive(false);
    }
}
