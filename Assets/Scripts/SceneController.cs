using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class SceneController : MonoBehaviour
{
   [SerializeField] private GameObject levelScene;
   [SerializeField] private GameObject mainScene;
   [SerializeField] private GameObject playScene;
   [SerializeField] private GameObject sceneManager;
   public VideoPlayer videoPlayer; 
   public RawImage rawImage;       
   public GameObject videoPanel; 
   public float delayAfterVideo = 2f;
   
   
   public void PlayGameButton()
   {
      PlayVideo();
      mainScene.SetActive(false);
   }

   public void ReturnLevelScene()
   {
      sceneManager.SetActive(true);
      levelScene.SetActive(true);
      playScene.SetActive(false);
      
   }
   public void LevelSceneStartButtun()
   {
      sceneManager.SetActive(false);
      levelScene.SetActive(false);
      playScene.SetActive(true);
   }

   public void QuitGameButton()
   {
      Application.Quit();
   }
   
   void PlayVideo()
   {
      videoPanel.SetActive(true);
      if (videoPlayer == null || rawImage == null || videoPanel == null )
      {
         Debug.LogError("Bileşenlerden biri atanmadı!");
         return;
      }
      
     
      mainScene.SetActive(false);
      
      if (videoPlayer.targetTexture == null)
      {
         RenderTexture renderTexture = new RenderTexture(1920, 1080, 0);
         videoPlayer.targetTexture = renderTexture;
         rawImage.texture = renderTexture;
      }

      videoPlayer.loopPointReached += OnVideoEnd;

      videoPlayer.Play(); 
   }
   void OnVideoEnd(VideoPlayer vp)
   {
      Debug.Log("Video bitti, işlemler başlıyor...");
      StartCoroutine(HandleVideoEnd());
   }

   IEnumerator HandleVideoEnd()
   {
      yield return new WaitForSeconds(delayAfterVideo);
      
      videoPanel.SetActive(false); 
      levelScene.SetActive(true);  
   }
}
