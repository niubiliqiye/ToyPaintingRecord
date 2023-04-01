using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Script
{
    public class Mouseevent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public static Mouseevent Instance;
        private Animator animator;
        private Button button;
        private LoadManager loadManager;
        private Achievement achivevment;
        private Install install;

        // Start is called before the first frame update
        void Awake()
        {
            Instance = this;
            animator = GetComponent<Animator>();
            button = GetComponent<Button>();
            loadManager = FindObjectOfType<LoadManager>();
            achivevment = FindObjectOfType<Achievement>();
            install = FindObjectOfType<Install>();
        }
        
        /// <summary>
        /// 鼠标移入事件
        /// </summary>
        public void OnPointerEnter(PointerEventData eventData)
        {
            animator.SetBool("IsEnter",true);
        }
        
        /// <summary>
        /// 鼠标移出事件
        /// </summary>
        public void OnPointerExit(PointerEventData eventData)
        {
            animator.SetBool("IsEnter",false);
        }

        public void ButtonOnClick()
        {
            switch (gameObject.tag)
            {
                case "始":
                    loadManager.sceneIndex = 1;
                    loadManager.LoadNextLevel();
                    break;
                case "续":
                    loadManager.sceneIndex = 1;
                    loadManager.LoadNextLevel();
                    break;
                case "选":
                    loadManager.sceneIndex = 3;
                    loadManager.LoadNextLevel();
                    break;
                case "藏":
                    achivevment.GetComponent<Achievement>().Achievement_Open();
                    achivevment.GetComponent<Achievement>().achievement.SetActive(true);
                    break;
                case "设":
                    install.GetComponent<Install>().Install_Open();
                    install.GetComponent<Install>().install.SetActive(true);
                    break;
                case "退":
                    Application.Quit();
                    break;
            }
        }
    }
}
