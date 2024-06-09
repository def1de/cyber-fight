using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    [SerializeField] private int maximum;
    [SerializeField] private int current;
    [SerializeField] private Image mask;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetCurrnetFill();
    }

    void GetCurrnetFill()
    {
        float fillAmount = (float)current / (float)maximum;
        mask.fillAmount = fillAmount;
    }
}
