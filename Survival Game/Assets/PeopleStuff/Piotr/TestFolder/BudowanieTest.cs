using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BudowanieTest : MonoBehaviour {

    public GameObject [] listaObiektow;
    public bool buduje;
    public GameObject obecnyObiekt;


	void Start ()
    {
		
	}
	
	void Update ()
    {
        if (!buduje)
        {
            buduje = true;
            obecnyObiekt = Instantiate(wybor(), ustawPozycje(), Quaternion.identity);
        }
	}

    GameObject wybor()
    {
        GameObject obecny = listaObiektow[0];
        return obecny;
    }

    Vector3 ustawPozycje()
    {
        Vector3 pozycja = transform.position;
        pozycja.y -= 10f;
        return pozycja;
    }


}
