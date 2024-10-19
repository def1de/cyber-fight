using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    [SerializeField] private int maximum = 4;
    [SerializeField] private int current;
    [SerializeField] private Image mask;
    [SerializeField] private GameObject enemies;

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
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        float fillAmount = (float)enemyCount / (float)maximum;
        mask.fillAmount = fillAmount;
    }
}
