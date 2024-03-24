// MyChaiWindow.cs
// Created by Cui Lingzhi
// on 2024 - 03 - 22

using System;
using System.IO;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace MyScripts
{
    public partial class MyAIAssistant : OdinEditorWindow
    {
        [MenuItem("Window/My AI Assistant")]
        private static void OpenWindow()
        {
            GetWindow<MyAIAssistant>().Show();
            ChatApi.requestMessageHistory.Clear();
        }
        [TabGroup("聊天")]
        [TextArea] [LabelText("输入")] public string inputText;
        [TabGroup("聊天")]
        [Multiline(10)] [LabelText("回答")] public string response;

        private bool mIsWaitingChat = false;
        [TabGroup("聊天")]
        [Button("发送", ButtonSizes.Large)]
        public void Send()
        {
            if (mIsWaitingChat)
                return;
            mIsWaitingChat = true;
            OpenAIUtil.InvokeChat(inputText, ChatCallBack);
        }

        private void ChatCallBack(string result)
        {
            mIsWaitingChat = false;
            response = result;
            EditorUtility.ClearProgressBar();
        }

        protected override void OnImGUI()
        {
            base.OnImGUI();

            if (mIsWaitingImage)
            {
                BigColoredProgressBar += 0.1f;
                if (BigColoredProgressBar >= 100)
                    BigColoredProgressBar  = 0;
            }
        }
    }
}