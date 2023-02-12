using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    private float scrollSpeed;

    private float offsetX;

    private Material material;
    private Rigidbody2D rigidbody;

    private void Start()
    {
        rigidbody = FindObjectOfType<PlayerInput>().GetComponent<Rigidbody2D>();
        material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        UpdateScrollSpeed();
        UpdateOffset();


        material.SetTextureOffset("_MainTex",new Vector2(offsetX, 0));
    }

    private void UpdateScrollSpeed()
    {
        scrollSpeed = rigidbody.velocity.x/10;
    }

    private void UpdateOffset()
    {
        offsetX += (Time.deltaTime * scrollSpeed) / 10f;
    }
}
