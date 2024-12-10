using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mushroom : MonoBehaviour
{
    public List<Sprite> edible;
    public List<Sprite> poisonous;

    [SerializeField] private GameController controller;

    private Vector2 startPosition = new Vector2(0f, -3.5f);
    private Vector2 endPosition = Vector2.zero;
    private float showDuration = 0.5f;
    private float duration = 1f;

    private SpriteRenderer spriteRenderer;

    private bool hittable = false;
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
        hittable = false;
        transform.localPosition = start;
        controller.currentMushrooms.Remove(controller.mushrooms[mushroomIndex]);
    }

    public void Hide()
    {
        transform.localPosition = startPosition;
    }

    private IEnumerator QuickHide()
    {
        yield return new WaitForSeconds(0.15f);
        // Whilst we were waiting we may have spawned again here, so just
        // check that hasn't happened before hiding it. This will stop it
        // flickering in that case.
        if (!hittable) {
            Hide();
        }
    }

    public void Smashed()
    {
        if (hittable) {
            // Turn off hittable so that we can't keep tapping for score.
            hittable = false;
            switch (mushroomType) {
            case MushroomType.Edible:
                ScoreManager.instance.ReducePoints();
                break;
            case MushroomType.Poisonous:
                ScoreManager.instance.AddPoint();
                break;
            default:
                break;
            }
            StopAllCoroutines();
            StartCoroutine(QuickHide());
            controller.currentMushrooms.Remove(controller.mushrooms[mushroomIndex]);
        }
    }

    private void CreateNext()
    {
        int randomType = Random.Range(0, 2);
        if (randomType == 0)
        {
            mushroomType = MushroomType.Edible;
            int random = Random.Range(0, edible.Count);
            spriteRenderer.sprite = edible[random];
        } else {
            mushroomType = MushroomType.Poisonous;
            int random = Random.Range(0, poisonous.Count);
            spriteRenderer.sprite = poisonous[random];
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
