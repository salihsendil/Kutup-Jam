using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
   [SerializeField] private GameObject levelScene;
   [SerializeField] private GameObject mainScene;

   public void PlayGameButton()
   {
      levelScene.SetActive(true);
      mainScene.SetActive(false);
   }

   public void QuitGameButton()
   {
      Application.Quit();
   }
}
