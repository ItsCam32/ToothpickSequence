using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Toothpicks : MonoBehaviour
{
    public GameObject toothpickPrefab;
    public TextMeshProUGUI iterationText, sticksText;

    bool orientation = true;
    int sticks = 0;
    GameObject pick;
    List<GameObject> toothpicks = new List<GameObject>();
    List<GameObject> toAdd = new List<GameObject>();

    void Start()
    {
        pick = Instantiate(toothpickPrefab, Vector3.zero, Quaternion.Euler(0, 0, 90));
        toothpicks.Add(pick);
        sticks++;

        StartCoroutine(IterationRoutine());
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Camera.main.orthographicSize += 150 * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.Mouse1))
        {
            Camera.main.orthographicSize -= 150 * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }    
    }

    IEnumerator IterationRoutine()
    {
        for (int i = 0; i < 5000; i++)
        {
            yield return new WaitForSeconds(0.15f);

            iterationText.text = "Iteration " + i.ToString();
            sticksText.text = "Sticks " + sticks.ToString();

            Iterate();
            toothpicks.Clear();
            toothpicks.AddRange(toAdd);
            toAdd.Clear();
        }
    }

    void Iterate()
    {
        foreach (GameObject toothpick in toothpicks)
        {
            foreach (Transform end in toothpick.transform)
            {
                pick = Instantiate(toothpickPrefab, end.position, Quaternion.Euler(GetOppositeOrientationVector()));
                toAdd.Add(pick);
                sticks++;
            }
        }

        orientation = !orientation;
    }

    Vector3 GetOppositeOrientationVector()
    {
        if (orientation == true)
        {
            return new Vector3(0, 0, 0);
            
        }

        return new Vector3(0, 0, 90);
    }
}
