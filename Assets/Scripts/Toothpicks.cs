using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Toothpicks : MonoBehaviour
{
    // vv Private Exposed vv //

    [Range(10, 10000)]
    [SerializeField]
    private int iterations;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private GameObject toothpickPrefab;

    [SerializeField]
    private TextMeshProUGUI iterationText, sticksText;

    // vv Private vv //

    private bool orientation = true;
    private int sticks = 0;
    private GameObject toothpick;
    private List<GameObject> toothpicks = new List<GameObject>();
    private List<GameObject> toAdd = new List<GameObject>();

    ////////////////////////////////////////

    #region Private Functions

    private void Start()
    {
        toothpick = Instantiate(toothpickPrefab, Vector3.zero, Quaternion.Euler(0, 0, 90));
        toothpicks.Add(toothpick);
        sticks++;

        StartCoroutine(IterationRoutine());
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            cam.orthographicSize -= 3000 * Time.deltaTime;
        }

        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            cam.orthographicSize += 3000 * Time.deltaTime;
        }   
    }

    private IEnumerator IterationRoutine()
    {
        for (int i = 0; i < iterations; i++)
        {
            yield return new WaitForSeconds(0.1f);

            iterationText.text = $"Iteration {i.ToString()}";
            sticksText.text = $"Sticks {sticks.ToString()}";

            // For each end of every toothpick, create a new one and add to list to be used for next iteration
            foreach (GameObject toothpick in toothpicks)
            {
                foreach (Transform end in toothpick.transform)
                {
                    this.toothpick = Instantiate(toothpickPrefab, end.position, Quaternion.Euler(GetOppositeOrientationVector()));
                    toAdd.Add(this.toothpick);
                    sticks++;
                }
            }

            orientation = !orientation;
            toothpicks.Clear();
            toothpicks.AddRange(toAdd);
            toAdd.Clear();
        }
    }

    private Vector3 GetOppositeOrientationVector()
    {
        if (orientation == true)
        {
            return Vector3.zero;
        }

        return new Vector3(0, 0, 90);
    }
    #endregion

    #region Public Functions

    public void OnExitButtonClicked()
    {
        Application.Quit();
    }
    #endregion
}
