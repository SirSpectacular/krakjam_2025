using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Visibility : MonoBehaviour
{
public GameObject square;
public GameObject Sphere;

// Start is called before the first frame update
void Start()
{

}
// Update is called once per frame
void update()
{

}
public void whenButtonClicked()
{
if (square.activeInHierarchy == true)
square.SetActive(false);
else
square.SetActive(true);
}
}