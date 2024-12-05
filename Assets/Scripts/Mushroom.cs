using UnityEngine;
using System.Collections;

public class Mushroom : MonoBehaviour
{
    // TODO: add sprites

    [SerializeField] private GameController controller;

    private Vector2 startPosition = new Vector2(0f, -3.5f);
    private Vector2 endPosition = Vector2.zero;
    private float showDuration = 0.5f;
    private float duration = 1f;

    private SpriteRenderer spriteRenderer;

    private bool hittable = true;
    public enum MushroomType { Edible, Poisonous };
    private MushroomType mushroomType;
    private int mushroomIndex = 0;

    private IEnumerator ShowHide(Vector2 start, Vector2 end)
    {
        transform.localPosition = start;

        // Show the mushroom
        float elapsed = 0f;
        while (elapsed < showDuration) {
            transform.localPosition = Vector2.Lerp(start, end, elapsed / showDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = end;

        yield return new WaitForSeconds(duration);

        // Hide the mushroom
        elapsed = 0f;
        while (elapsed < showDuration) {
            transform.localPosition = Vector2.Lerp(end, start, elapsed / showDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = start;
        controller.currentMushrooms.Remove(controller.mushrooms[mushroomIndex]);
    }

    public void Hide()
    {
        transform.localPosition = startPosition;
    }

    private IEnumerator QuickHide()
    {
        yield return new WaitForSeconds(0.25f);
        // Whilst we were waiting we may have spawned again here, so just
        // check that hasn't happened before hiding it. This will stop it
        // flickering in that case.
        if (!hittable) {
            Hide();
        }
    }

    public void Smashed()
    {
        print ("smashed");
        if (hittable) {
            switch (mushroomType) {
            case MushroomType.Edible:
                print ("edible");
                ScoreManager.instance.ReducePoints();
                StopAllCoroutines();
                StartCoroutine(QuickHide());
                // Turn off hittable so that we can't keep tapping for score.
                hittable = false;
                break;
            case MushroomType.Poisonous:
                print ("poisonous");
                ScoreManager.instance.AddPoint();
                StopAllCoroutines();
                StartCoroutine(QuickHide());
                // Turn off hittable so that we can't keep tapping for score.
                hittable = false;
                break;
            default:
                break;
            }
            controller.currentMushrooms.Remove(controller.mushrooms[mushroomIndex]);
        }
    }

    private void CreateNext()
    {
        int random = Random.Range(0, 10);
        if (random < 5)
        {
            mushroomType = MushroomType.Edible;
        } else {
            mushroomType = MushroomType.Poisonous;
        }
        hittable = true;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Activate()
    {
        CreateNext();
        StartCoroutine(ShowHide(startPosition, endPosition));
    }

    public void SetIndex(int index)
    {
        mushroomIndex = index;
    }

    public void StopGame()
    {
        hittable = false;
        StopAllCoroutines();
    }
}
