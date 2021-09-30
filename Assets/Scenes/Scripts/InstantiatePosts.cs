using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstantiatePosts : MonoBehaviour
{

    public int PostsNumber=1;
    public GameObject PostPrefab;
    public Transform PostParent;

    public List<Texture2D> SampleTextures;
    void Start()
    {
        this.instantiatePosts();
    }

    void instantiatePosts(){

        for(int i=0; i<PostsNumber; i++){

            GameObject post=GameObject.Instantiate(PostPrefab, PostParent);

            Texture2D randText = this.SampleTextures[Random.Range(0, SampleTextures.Count)];
            Texture2D randProfile = this.SampleTextures[Random.Range(0, SampleTextures.Count)];
            
            post.transform.Find("ImageWrapper/RawImage").GetComponent<Image>().sprite = Sprite.Create(randText, new Rect(0.0f, 0.0f, randText.width, randText.height), new Vector2(0.5f, 0.5f), 100.0f);
            post.transform.Find("PostHeader/ImageMask/Profile").GetComponent<Image>().sprite = Sprite.Create(randProfile, new Rect(0.0f, 0.0f, randProfile.width, randProfile.height), new Vector2(0.5f, 0.5f), 100.0f);

            post.transform.Find("PostHeader/Username").GetComponent<TextMeshProUGUI>().text="CiaoCiaoBauBau";

        }

    }
    
}
