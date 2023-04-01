using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class DialogSystem : MonoBehaviour
    {
        // Start is called before the first frame update
        [Header("对话内容显示框")] public Text textLabel;

        [Header("对话框人物名称")] public Text textName;

        [Header("对话框选项一")] public Button optionOne;

        [Header("对话框选项二")] public Button optionTwo;

        [Header("对话文件")] public TextAsset textFile;
        
        [Header("玩家名字")] public string playerName;
        
        [Header("NPC姓名")] public string npcName;

        [Header("对话显示速度")] public float textSpeed;

        /// <summary>
        /// 对话当前行数
        /// </summary>
        private int _index;

        /// <summary>
        /// 判断是否完成打字  true:打完   false:没打完
        /// </summary>
        private bool _textFinished;

        /// <summary>
        /// 是否取消打字   true:是    false:否
        /// </summary>
        private bool _cancelTyping;

        /// <summary>
        /// 存放对话每行内容的合集
        /// </summary>
        private readonly List<string> _textList = new List<string>();
        
        /// <summary>
        /// 存放人物姓名
        /// </summary>
        private readonly List<string> _textNameList = new List<string>();

        /// <summary>
        /// 选项是否显示
        /// </summary>
        private bool _isOptionShow;

        /// <summary>
        /// 是否进行下一条对话
        /// </summary>
        private bool _isNextDialog;

        /// <summary>
        /// 存放相对一的对话的位置
        /// </summary>
        private int _nextDialog;

        void Awake()
        {
            GetTextFormFile(textFile);
            _textFinished = true;
        }
        
        private void OnEnable()
        {
            _textNameList.Add(playerName);
            _textNameList.Add(npcName);
            if (_textList[_index] == "Option")
            {
                ShowOption();
            }
            else
                StartCoroutine(SetTextUI());
        }

        // Update is called once per frame
        void Update()
        {
            if (!_isOptionShow)
            {
                if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && _index == _textList.Count)
                {
                    gameObject.SetActive(false);
                    _index = 0;
                    return;
                }

                if (((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))) || _isNextDialog)
                {
                    _isNextDialog = false;
                    if (_textFinished && !_cancelTyping)
                    {
                        if (_textList[_index] == "Option")
                        {
                            ShowOption();
                        }
                        else
                            StartCoroutine(SetTextUI());
                    }
                    else if (!_textFinished)
                    {
                        _cancelTyping = !_cancelTyping;
                    }
                }
            }
        }

        /// <summary>
        /// 从文件中读取对话内容
        /// </summary>
        /// <param name="file">对话文件</param>
        /// <param name="fileName">对话人物名称文件</param>
        void GetTextFormFile(TextAsset file)
        {
            _textList.Clear(); //释放_textList中的内容
            _index = 0;
            var lineDate = file.text.Split('\n'); //将文件种内容拆分
            foreach (var line in lineDate) //将文件拆分出的内容依次放入_textList
            {
                _textList.Add(line);
            }
        }

        /// <summary>
        /// 携程实现对话逐字显示
        /// </summary>
        /// <returns></returns>
        IEnumerator SetTextUI()
        {
            _textFinished = false;
            textLabel.text = "";

            GainName();
            int letter = 0;
            //存放分割的字符串
            var aster = _textList[_index].Split(':');
            while (!_cancelTyping && letter < aster[aster.Length - 1].Length - 1)
            {
                textLabel.text += aster[aster.Length - 1][letter];
                letter++;
                yield return new WaitForSeconds(textSpeed);
            }

            textLabel.text = aster[aster.Length - 1];
            _cancelTyping = false;
            _textFinished = true;
            if (_index + 1 <= _textList.Count - 1)
            {
                var aster2 = _textList[_index + 1].Split(':');
                if (aster2[0] == "答2")
                {
                    _index += _nextDialog;
                }
                else
                    _index++;
            }
            else
                _index++;
        }

        /// <summary>
        /// 玩家选择选项后跳转相应内容
        /// </summary>
        public void NextDialogue()
        {
            string buttonName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
            switch (buttonName)
            {
                case "optionOne":
                    _index++;
                    optionOne.gameObject.SetActive(false);
                    optionTwo.gameObject.SetActive(false);
                    _isOptionShow = false;
                    _isNextDialog = true;
                    break;
                case "optionTwo":
                    _index += _nextDialog + 1;
                    optionOne.gameObject.SetActive(false);
                    optionTwo.gameObject.SetActive(false);
                    _isOptionShow = false;
                    _isNextDialog = true;
                    break;
            }
        }

        /// <summary>
        /// 获取人物名称并显示
        /// </summary>
        void GainName()
        {
            print(_textNameList.Count);
            for (int i = 0; i < _textNameList.Count; i++)
            {
                print(3);
                if (_textList[_index] == _textNameList[i])
                {
                    print(1);
                    textName.text = _textNameList[i];
                    _index++;
                }
            }
        }

        /// <summary>
        /// 获取玩家选项内容并将其显示出来
        /// </summary>
        void ShowOption()
        {
            _index++;
            textName.text = _textList[_index];
            optionOne.gameObject.SetActive(true);
            optionTwo.gameObject.SetActive(true);
            _index++;
            optionOne.GetComponentInChildren<Text>().text = _textList[_index];
            _index++;
            var aster = _textList[_index].Split(':');
            optionTwo.GetComponentInChildren<Text>().text = aster[0];
            _nextDialog = Convert.ToInt32((aster[1]));
            textLabel.text = "";
            _isOptionShow = true;
        }
    }
}