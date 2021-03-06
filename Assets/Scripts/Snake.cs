using System.Collections.Generic;
using UnityEngine;


public class Snake : MonoBehaviour {
    private Vector2 direction = Vector2.down;
    private Vector2 oldDirection;

    [SerializeField]
    private GameObject snakeSegmentPrefab;

    protected List<GameObject> snakeSegments;

    public bool enemy = false;

    public GameController GameController;
    
    protected void Start() {
        snakeSegments = new List<GameObject>();
        snakeSegments.Add(this.gameObject);
    }

    private void Update() {
        Control();
    }

    protected void FixedUpdate() { 
        for (int i = snakeSegments.Count - 1; i > 0; i--) {
            snakeSegments[i].gameObject.transform.position = snakeSegments[i - 1].gameObject.transform.position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + direction.x,
            Mathf.Round(this.transform.position.y) + direction.y,
            0.0f
            );

        
    }

    protected void Control(){
        oldDirection = direction;

        if (enemy == false) {
            if (Input.GetKeyDown(KeyCode.W)) {
                direction = Vector2.up;
            }
            if (Input.GetKeyDown(KeyCode.S)) {
                direction = Vector2.down;
            }
            if (Input.GetKeyDown(KeyCode.A)) {
                direction = Vector2.left;
            }
            if (Input.GetKeyDown(KeyCode.D)) {
                direction = Vector2.right;
            }
        } else {
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                direction = Vector2.up;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                direction = Vector2.down;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                direction = Vector2.left;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                direction = Vector2.right;
            }
        }

        this.transform.Rotate(Vector3.forward, Vector2.SignedAngle(oldDirection, direction));
    }

    protected virtual void Grow() {
        GameObject segment = Instantiate(this.snakeSegmentPrefab);
        
        GrowBase(segment);
    }

    protected void GrowBase(GameObject segment)
    {
        if (enemy)
        {
            segment.GetComponent<SpriteRenderer>().color = Color.red;
        }
        
        segment.gameObject.transform.position = snakeSegments[snakeSegments.Count - 1].gameObject.transform.position;
        segment.SetActive(true);
        snakeSegments.Add(segment);
    }

    protected virtual void ResetGame() {
        for(int i = 1; i < snakeSegments.Count; i++) {
            Destroy(snakeSegments[i].gameObject);
        }
        ResetGameBase();
    }

    protected void ResetGameBase()
    {
        snakeSegments.Clear();
        snakeSegments.Add(this.gameObject);
        
        if (enemy)
        {
            this.transform.position = Vector3.zero;
        }
        else
        {
            this.transform.position = new Vector3(10f, 10f, 0.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        Debug.Log("TriggerEnter");
        if (collision.tag == "Food")
        {
            Grow();
            
        }
        else
        {
            Debug.Log("PrePoint");
            if (collision.tag == "Snake" || collision.tag == "SnakeSegment")
            {
                GameController.AddPointAndCheckForWinner(enemy);
            }

            ResetGame();
        }
    }
}
