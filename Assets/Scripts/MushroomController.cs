using System.Collections;
using UnityEngine;
using System.IO.Ports;

public class MushroomController : MonoBehaviour
{
    public GameObject mushroom1;
    public GameObject mushroom2;
    public GameObject mushroom3;

    private SerialPort sp;

    private bool isMushroom1AtPeak = false;
    private bool isMushroom2AtPeak = false;
    private bool isMushroom3AtPeak = false;
    private bool isMushroom1Moving = false;
    private bool isMushroom2Moving = false;
    private bool isMushroom3Moving = false;

    private Vector2 mushroom1PeakPosition = new Vector2(-3f, -0.5f);
    private Vector2 mushroom1StartPosition = new Vector2(-3f, -3.5f);
    private Vector2 mushroom2PeakPosition = new Vector2(0f, -0.5f);
    private Vector2 mushroom2StartPosition = new Vector2(0f, -3.5f);
    private Vector2 mushroom3PeakPosition = new Vector2(3f, -0.5f);
    private Vector2 mushroom3StartPosition = new Vector2(3f, -3.5f);

    private float moveDuration = 1f;

    void Start()
    {
        sp = new SerialPort("COM3", 9600);
        sp.Open();
        sp.ReadTimeout = 1;

        mushroom1.transform.position = mushroom1StartPosition;
        mushroom2.transform.position = mushroom2StartPosition;
        mushroom3.transform.position = mushroom3StartPosition;

        StartCoroutine(StaggeredInitialMovement());
    }

    private IEnumerator StaggeredInitialMovement()
    {
        yield return StartCoroutine(MoveUpAndDown(mushroom1, mushroom1StartPosition, mushroom1PeakPosition));
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(MoveUpAndDown(mushroom2, mushroom2StartPosition, mushroom2PeakPosition));
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(MoveUpAndDown(mushroom3, mushroom3StartPosition, mushroom3PeakPosition));
    }

    void Update()
    {
        if (sp != null && sp.IsOpen)
        {
            try
            {
                int command = sp.ReadByte();

                // Mushroom 1 movement
                if (command == 1)
                {
                    if (!isMushroom1Moving)
                    {
                        if (!isMushroom1AtPeak)
                        {
                            StartCoroutine(MoveUp(mushroom1, mushroom1StartPosition, mushroom1PeakPosition));
                        }
                        else
                        {
                            StartCoroutine(MoveDownAndUp(mushroom1, mushroom1PeakPosition, mushroom1StartPosition, mushroom1PeakPosition));
                        }
                    }
                }

                // Mushroom 2 movement
                if (command == 2)
                {
                    if (!isMushroom2Moving)
                    {
                        if (!isMushroom2AtPeak)
                        {
                            StartCoroutine(MoveUp(mushroom2, mushroom2StartPosition, mushroom2PeakPosition));
                        }
                        else
                        {
                            StartCoroutine(MoveDownAndUp(mushroom2, mushroom2PeakPosition, mushroom2StartPosition, mushroom2PeakPosition));
                        }
                    }
                }

                // Mushroom 3 movement
                if (command == 3)
                {
                    if (!isMushroom3Moving)
                    {
                        if (!isMushroom3AtPeak)
                        {
                            StartCoroutine(MoveUp(mushroom3, mushroom3StartPosition, mushroom3PeakPosition));
                        }
                        else
                        {
                            StartCoroutine(MoveDownAndUp(mushroom3, mushroom3PeakPosition, mushroom3StartPosition, mushroom3PeakPosition));
                        }
                    }
                }
            }
            catch (System.Exception) {}
        }
    }

    private IEnumerator MoveUpAndDown(GameObject mushroom, Vector2 startPosition, Vector2 peakPosition)
    {
        Vector2 currentPosition = mushroom.transform.position;
        yield return StartCoroutine(MoveSmoothly(mushroom, currentPosition, peakPosition));

        if (mushroom == mushroom1) isMushroom1AtPeak = true;
        else if (mushroom == mushroom2) isMushroom2AtPeak = true;
        else if (mushroom == mushroom3) isMushroom3AtPeak = true;
    }

    private IEnumerator MoveSmoothly(GameObject mushroom, Vector2 from, Vector2 to)
    {
        if (mushroom == mushroom1) isMushroom1Moving = true;
        else if (mushroom == mushroom2) isMushroom2Moving = true;
        else isMushroom3Moving = true;

        float elapsed = 0f;
        while (elapsed < moveDuration)
        {
            mushroom.transform.position = Vector2.Lerp(from, to, elapsed / moveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        mushroom.transform.position = to;

        if (mushroom == mushroom1) isMushroom1Moving = false;
        else if (mushroom == mushroom2) isMushroom2Moving = false;
        else isMushroom3Moving = false;
    }

    private IEnumerator MoveDownAndUp(GameObject mushroom, Vector2 peakPosition, Vector2 startPosition, Vector2 finalPeakPosition)
    {
        Vector2 currentPosition = mushroom.transform.position;
        yield return StartCoroutine(MoveSmoothly(mushroom, currentPosition, startPosition));
        yield return StartCoroutine(MoveSmoothly(mushroom, startPosition, finalPeakPosition));

        if (mushroom == mushroom1) isMushroom1AtPeak = true;
        else if (mushroom == mushroom2) isMushroom2AtPeak = true;
        else if (mushroom == mushroom3) isMushroom3AtPeak = true;
    }

    private IEnumerator MoveUp(GameObject mushroom, Vector2 startPosition, Vector2 peakPosition)
    {
        Vector2 currentPosition = mushroom.transform.position;
        yield return StartCoroutine(MoveSmoothly(mushroom, currentPosition, peakPosition));

        if (mushroom == mushroom1) isMushroom1AtPeak = true;
        else if (mushroom == mushroom2) isMushroom2AtPeak = true;
        else if (mushroom == mushroom3) isMushroom3AtPeak = true;
    }

    void OnApplicationQuit()
    {
        if (sp != null && sp.IsOpen)
        {
            sp.Close();
        }
    }
}