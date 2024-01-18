using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public float speed = -0.25f;

    public float[] cloudSpeeds;
    float screenWidth;
    float worldScreenWidth;
    void Start(){
        cloudSpeeds = new float[transform.childCount];
        for(int i = 0; i < cloudSpeeds.Length;i++){
            cloudSpeeds[i] = speed*(i/2+1);
        }
        // Lấy kích thước chiều rộng màn hình
        screenWidth = Screen.width;

        // Chuyển đổi kích thước từ pixel sang đơn vị đo lường của Unity
        worldScreenWidth = Camera.main.ScreenToWorldPoint(new Vector3(screenWidth, 0, 0)).x;
    }
    void Update()
    {
        for (int i = 0; i < cloudSpeeds.Length; i++)
        {
            // Lấy vị trí hiện tại của đám mây
            Vector3 cloudPosition = transform.GetChild(i).position;

            // Di chuyển đám mây theo tốc độ tương ứng
            cloudPosition.x += cloudSpeeds[i] * Time.deltaTime;

            // Kiểm tra và đặt lại vị trí nếu đám mây đi ra khỏi màn hình
            if (cloudPosition.x + transform.GetChild(i).GetComponent<Renderer>().bounds.size.x/2 < Camera.main.transform.position.x - worldScreenWidth -0.5f)
            {
                cloudPosition.x += 2f*transform.GetChild(i).GetComponent<Renderer>().bounds.size.x;
            }

            // Gán vị trí mới cho đám mây
            transform.GetChild(i).position = cloudPosition;
        }
    }
}
