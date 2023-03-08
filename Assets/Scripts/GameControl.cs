using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;
public class GameControl : MonoBehaviour
{

    [Header("GeneralSettings")]
    public static GameControl instance;
    public bool isHandFull = false;
    public bool isHaveSomething = false;
    public bool isOnTable = false;
    [Header("Parents")]
    public Transform owen;
    public Transform Table;
    [Header("Images")]
    public Image Timer;
    public Image Timer2;
    public Image Timer3;
    [Header("List")]
    public List<GameObject> Stuff = new List<GameObject>();
    public List<GameObject> Stuff2 = new List<GameObject>();
    public List<GameObject> Stuff3 = new List<GameObject>();
    public List<GameObject> Stuff4 = new List<GameObject>();
    public List<GameObject> Stuff5 = new List<GameObject>();
    public List<GameObject> Customers = new List<GameObject>();
    public List<GameObject> CustomersPosition = new List<GameObject>();
    [Header("OtherSettings")]
    public GameObject CharPoint;
    public GameObject DoughCreatePoint;
    public GameObject Dough;
    public GameObject DoughStackPoint;
    public GameObject BreadStackPoint;
    public GameObject CustomerStackPoint;
    public GameObject CustomersGoingPoint;
    public GameObject CustomersLeadPosition;
    Vector3 FirstPosBread;
    Vector3 FirstPosCustomer;
    public TextMeshProUGUI MoneyText;
    private int Dollars = 0;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        FirstPosBread = BreadStackPoint.transform.position;
        MoneyText.text = Dollars.ToString();
    }
    private void OnTriggerStay(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Circle1":
                if (!isHandFull)
                {
                    DoughTimerSetting();
                }
                break;
            case "Circle2":
                if (isHaveSomething)
                {
                    OwenStackPointSettings();
                }
                break;
            case "Circle3":
                BreadGatheringHand();
                break;
            case "Circle4":
                CustomerBread();
                break;
            default:
                break;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Circle1":
                Timer.fillAmount = 0;
                break;
            case "Circle2":
                Timer.fillAmount = 0;
                break;
            case "Circle4":
                CustomerStackPoint.transform.position = new Vector3(1.06f, 0.617f, 0.844f);
                Timer3.fillAmount = 0;
                break;
            default:
                break;
        }
    }

    private void DoughTimerSetting()
    {
        Timer.fillAmount += 0.03f;
        if (Timer.fillAmount == 1 && Stuff.Count == 0)
        {
            GameObject obj = Instantiate(Dough, DoughCreatePoint.transform.position, Quaternion.identity);
            obj.LeanMove(CharPoint.transform.position, 0.1f).setEase(LeanTweenType.clamp);
            CharPoint.transform.position += new Vector3(0, 0.08f, 0);
            Stuff.Add(obj);
            obj.transform.SetParent(transform);
            isHaveSomething = true;
            Timer.fillAmount = 0;

        }
        else if (Timer.fillAmount == 1 && Stuff.Count < 5)
        {

            GameObject obj2 = Instantiate(Dough, DoughCreatePoint.transform.position, Quaternion.identity);
            obj2.LeanMove(CharPoint.transform.position, 0.1f).setEase(LeanTweenType.clamp);
            CharPoint.transform.position += new Vector3(0, 0.08f, 0);
            Stuff.Add(obj2);
            obj2.transform.SetParent(transform);
            isHaveSomething = true;
            Timer.fillAmount = 0;
        }
        if (Stuff.Count == 5)
        {
            isHandFull = true;
            isHaveSomething = true;
            Timer.fillAmount = 0;
        }
    }
    private void OwenStackPointSettings()
    {

        if (Stuff.Count != 0)
        {
            Timer2.fillAmount += 0.015f;
            if (Timer2.fillAmount == 1)
            {
                for (int i = 0; i < Stuff.Count; i++)
                {

                    Stuff[i].transform.parent = owen;
                    Stuff[i].LeanMove(DoughStackPoint.transform.position, 0.7f).setEase(LeanTweenType.clamp);
                    DoughStackPoint.transform.position += new Vector3(0, 0.08f, 0);
                    Stuff2.Add((Stuff[i]));
                    Stuff.Remove(Stuff[i]);
                    isHaveSomething = true;
                }
            }
            if (Stuff.Count == 0)
            {
                CharPoint.transform.localPosition = new Vector3(0, 0, 0.539f);
                Timer2.fillAmount = 0f;
                Invoke("CreateBreed", 1f);
                DoughStackPoint.transform.position = new Vector3(-0.3956f, 0.44f, -1.94f);
            }
        }

        else if (Stuff2.Count == 0)
        {

            isHaveSomething = false;
            Timer.fillAmount = 0;
            DoughStackPoint.transform.position = new Vector3(-0.3956f, 0.44f, -1.94f);
        }
    }
    private void BreadGatheringHand()
    {

        for (int i = 0; i < Stuff3.Count; i++)
        {
            Stuff3[i].transform.position = CharPoint.transform.position;
            Stuff3[i].transform.SetParent(transform);
            CharPoint.transform.position += new Vector3(0, 0.08f + 0);
            Stuff4.Add((Stuff3[i]));
            Stuff3.Remove(Stuff3[i]);
            isHandFull = true;

            if (Stuff3.Count == 0)
            {
                BreadStackPoint.transform.position = FirstPosBread;
            }
        }
    }
    private IEnumerator BreadSettings()
    {
        for (int i = 0; i < Stuff2.Count; i++)
        {
            Stuff2[i].transform.gameObject.GetComponent<Renderer>().material.color = new Color(1f, 0.524f, 0.2028f);
            Stuff2[i].transform.position = BreadStackPoint.transform.position;
            BreadStackPoint.transform.position += new Vector3(0, 0.08f + 0);
            Timer2.fillAmount = 0;
            Stuff3.Add((Stuff2[i]));
            if (i == Stuff2.Count - 1)
            {
                Stuff2.Clear();
                StopCoroutine(BreadSettings());
                Timer2.fillAmount = 0;

            }
            yield return new WaitForSeconds(1f);
        }

    }
    private void CustomerBread()
    {
        if (Stuff4.Count != 0)
        {
            Timer3.fillAmount += 0.015f;
            if (Timer3.fillAmount == 1)
            {

                for (int i = 0; i < Stuff4.Count; i++)
                {

                    Stuff4[i].transform.parent = Table;
                    Stuff4[i].LeanMove(CustomerStackPoint.transform.position, 0.6f).setEase(LeanTweenType.clamp);
                    CustomerStackPoint.transform.position += new Vector3(0, 0, 0.05f);
                    Stuff5.Add((Stuff4[i]));
                    Stuff4.Remove(Stuff4[i]);



                }
            }
            if (Stuff4.Count == 0)
            {
                Timer3.fillAmount = 0f;
                CharPoint.transform.localPosition = new Vector3(0, 0, 0.539f);
                StartCoroutine(BreadMoney());
                isOnTable = true;
                isHandFull = false;


            }
        }
    }
    private void CreateBreed()
    {
        if (Stuff2.Count != 0 && isHaveSomething == true)
        {
            StartCoroutine(BreadSettings());
        }
    }

    private IEnumerator BreadMoney()
    {
        if (Stuff5.Count != 0)
        {

            Customers[0].GetComponent<NavMeshAgent>().SetDestination(CustomersGoingPoint.transform.position);
            Customers[0].GetComponent<Animator>().SetBool("isCustomerWalk", true);
            Destroy(Stuff5[^1]);
            Stuff5.Remove(Stuff5[^1]);
            Dollars += 5;
            MoneyText.text = Dollars.ToString();
            yield return new WaitForSeconds(1.5f);
        }
        else
        {
            for (int i = 0; i < Customers.Count; i++)
            {
                Customers[i].GetComponent<Animator>().SetBool("isCustomerWalk", false);
                Customers[i].transform.rotation = Quaternion.Euler(0, -130.448f, 0);
            }
        }
    }
    public void DestinationSettings()
    {

        Destroy(Customers[0]);
        Customers.Remove(Customers[0]);
        StartCoroutine(LineSettings());
        StartCoroutine(BreadMoney());
    }
    private IEnumerator LineSettings()
    {
        for (int i = 0; i < Customers.Count; i++)
        {

            Customers[i].GetComponent<Animator>().SetBool("isCustomerWalk", true);
            Customers[i].GetComponent<NavMeshAgent>().SetDestination(CustomersPosition[i].transform.position);
            yield return new WaitForSeconds(0.4f);
            Customers[i].GetComponent<Animator>().SetBool("isCustomerWalk", false);
            Customers[i].transform.rotation = Quaternion.Euler(0, -130.448f, 0);
        }
    }
}











