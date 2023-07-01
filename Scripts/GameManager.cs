using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int itemCount;
    public int totalItemCount;
    public int stage;
    public Text stageCountText;
    public Text playerCountText;
    // Start is called before the first frame update
    void Start(){
    
    }
    void Awake()
    {
        itemCount=0;
        stageCountText.text="/ " + totalItemCount;
    }

    // Update is called once per frame

     public void GetItem(int count){
        playerCountText.text = count.ToString();
    }

    public void MoveNextStage(){//다음 스테이지
        if(itemCount==totalItemCount){
            SceneManager.LoadScene(stage+1);
        }
        else{
            SceneManager.LoadScene(stage);
        }
    }
}
