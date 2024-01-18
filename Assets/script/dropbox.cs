using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropbox : MonoBehaviour
{
    public GameObject[] boxToSpawn;  // Đối tượng cần tạo
    public GameObject[] boxToNext;  // Đối tượng cần tạo next
    public Transform nextBox; //vị trí next
    public float speedRotate = 50.0f;
    private bool isDragging = false;
    private Vector3 offset;
    private int randomBox;

    void Start(){
        //tạo khối mới khi vào game
        randomBox = Random.Range(0, boxToSpawn.Length * 100);
        taoKhoi();
    }
    void Update(){
        // Lấy game object có tag là "box"
        GameObject box = GameObject.FindGameObjectWithTag("khoi");

        if (box != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Đánh dấu là bắt đầu nhấn chuột
                isDragging = true;
                // Lấy khoảng cách chuột với khối
                offset = box.transform.position - GetMouseWorldPos();
            }
            if(Input.GetMouseButtonUp(0) && isDragging)
            {
                // Lấy game object có tag là "next"
                GameObject next = GameObject.FindGameObjectWithTag("next");
                Destroy(next);
                isDragging = false;
                // Kiểm tra xem có Rigidbody2D không trước khi sử dụng
                Rigidbody2D boxRigidbody = box.GetComponent<Rigidbody2D>();
                if (boxRigidbody != null)
                {
                    boxRigidbody.gravityScale = 1f;
                    box.tag = "khoiDrop";
                    Invoke("taoKhoi", 2f);
                }
            }
            if (isDragging)
            {
                // Kiểm tra nếu box không null thì thiết lập tọa độ x
                Vector3 mousePos = GetMouseWorldPos();
                // Di chuyển box theo chuột dựa trên khoảng cách tới chuột
                box.transform.position = new Vector3(mousePos.x + offset.x, transform.position.y, transform.position.z);
                // Thay đổi góc xoay của GameObject
                box.transform.Rotate(Vector3.forward, speedRotate * Time.deltaTime);
            }
        }
    }
    // lấy tọa độ chuột
    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
        return mousePos;
    }
    void taoKhoi(){
        Instantiate(boxToSpawn[randomBox/100], transform.position, Quaternion.identity);
        randomBox = Random.Range(0, boxToSpawn.Length * 100);
        Instantiate(boxToNext[randomBox/100], nextBox.position, Quaternion.identity);
    }
}
