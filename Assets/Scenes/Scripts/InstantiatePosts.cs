using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class InstantiatePosts : MonoBehaviour
{

    public string Path;

    public GameObject PostPrefab;
    public Transform PostParent;

    public MData data;
    
    void Start()
    {
        //string json = File.ReadAllText(this.Path);
        //this.data = JsonUtility.FromJson<MData>(json);
        //this.instantiatePosts();

        StartCoroutine(this.DownloadJson(Path, onProgress, onComplete));

    }

    void instantiatePosts(){

        
        foreach(MPost PostData in this.data.Posts){

            GameObject PostGameObject=GameObject.Instantiate(PostPrefab, PostParent);
            
            //Username
            PostGameObject.transform.Find("PostHeader/Username").GetComponent<TextMeshProUGUI>().text=PostData.Username;

            StartCoroutine(this.DownloadTexture(PostData.ImageURL,
                (float p)=>{Debug.Log("Downloading Texture "+p);},
                (Texture2D result, string error)=>{
                    if(result!=null){
                        PostGameObject.transform.Find("ImageWrapper/RawImage").GetComponent<Image>().sprite = Sprite.Create(result, new Rect(0.0f, 0.0f, result.width, result.height), new Vector2(0.5f, 0.5f), 100.0f);
                    }
                }

            ));

        }

    }
    void onProgress(float p){
        Debug.Log(p);
    }
    void onComplete( MData d, string error )
    {
        if(d!=null){
            this.data=d;
            this.instantiatePosts();
        }

    }
    private IEnumerator DownloadJson(string url, Action<float> onProgress, Action<MData,string> onComplete)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            var operation= www.SendWebRequest();
            
            while(!operation.isDone)
            {	
                onProgress( www.downloadProgress );
                yield return null;
            }
            
            if (string.IsNullOrEmpty(www.error))
            {

                var json = www.downloadHandler.text;
                var content = JsonUtility.FromJson<MData>(json);

                if (onComplete != null)
                    onComplete(content,"");
            }
            else
            {
                Debug.LogError("Error " + www.error);
                if (onComplete != null)
                    onComplete(null, www.error);
            }	
        }
    }

    private IEnumerator DownloadTexture(string url, Action<float> onProgress, Action<Texture2D,string> onComplete)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        {
            var operation= www.SendWebRequest();
            
            while(!operation.isDone)
            {	
                onProgress( www.downloadProgress );
                yield return null;
            }
            
            if (string.IsNullOrEmpty(www.error))
            {
                
                Texture2D text = DownloadHandlerTexture.GetContent(www);
                if (onComplete != null)
                    onComplete(text,"");
            }
            else
            {
                if (onComplete != null)
                    onComplete(null, www.error);
            }	
        }
    }
}
