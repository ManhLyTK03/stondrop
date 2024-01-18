using UnityEngine;

public class bonot : MonoBehaviour
{
    public string boxTag = "box";
    public int numberOfBoxesToDestroy = 10;

    public int currentBoxesCount = 0;
    public GameObject[] boxes;
    void Start(){
        boxes = new GameObject[numberOfBoxesToDestroy];
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(boxTag))
        {
            currentBoxesCount++;
            // Tìm một phần tử null trong mảng boxes để gán giá trị mới
            GameObject newBox = null;
            for (int i = 0; i < boxes.Length; i++)
            {
                if (boxes[i] == null)
                {
                    newBox = other.gameObject;
                    boxes[i] = newBox;
                    break;
                }
            }
            if (currentBoxesCount >= numberOfBoxesToDestroy)
            {
                // Xóa 10 chiếc hộp
                DestroyBoxes();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(boxTag)){
            currentBoxesCount--;
            for (int i = 0; i < boxes.Length; i++){
                if(other.gameObject == boxes[i]){
                    boxes[i] = boxes[boxes.Length - 1];
                }
            }
        }
    }

    private void DestroyBoxes()
    {
        foreach (GameObject box in boxes)
        {
            box.transform.parent.gameObject.GetComponent<Rigidbody2D>().gravityScale -= 0.25f;
            Destroy(box);
            currentBoxesCount = 0;
            boxes = new GameObject[numberOfBoxesToDestroy];
        }
    }
}
