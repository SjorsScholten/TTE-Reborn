using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestScript : MonoBehaviour
{
    #region Editor Variables

    #region Sinterklaas
    [Header("Sinterklaas")]
    [SerializeField]
    [Tooltip("De Pakjesboot.")]
    private Stoomboot pakjesboot;

    [SerializeField]
    [Tooltip("Aantal dagen tot pakjes avond.")]
    private int dagenTotPakjesAvond = 21;
    #endregion

    #region Kerst
    [Header("Kerst")]
    [SerializeField]
    [Range(0, 2000)]
    private float leeftijdKerstman = 75;

    [SerializeField]
    [Tooltip("Wat zegt de kerstman.")]
    private string roept = "Ho Ho Ho";

    [SerializeField]
    [Tooltip("Laat zien of het al tijd is om naar kerstliedjes te luisteren.")]
    private bool isHetTijdVoorKerstLiedjes = true;
    #endregion

    #region Events
    [Header("Events")]
    [SerializeField]
    private UnityEvent StoombootArriveert;

    [SerializeField]
    private UnityEvent PakjesAvond;
    #endregion

    #endregion



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="getal"></param>
    /// <returns></returns>
    private int TestMethod(int getal)
    {
        return getal;
    }
}

public class Stoomboot : MonoBehaviour
{

}