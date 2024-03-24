// OpenAIUtil.cs
// Created by Cui Lingzhi
// on 2024 - 03 - 22

using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace MyScripts
{
    public static partial class OpenAIUtil
    {
        private const string API_KEY = "改成自己的";
        private const int TIMEOUT = 90;

        private static MyAIAssistant assistant => EditorWindow.GetWindow<MyAIAssistant>();

        /// <summary>
        /// 创建访问gpt的消息体
        /// </summary>
        /// <returns></returns>
        private static string CreateChatRequestBody(string prompt)
        {
            var msg = new ChatApi.RequestMessage
            {
                role = "user",
                content = prompt,
                name = "test"
            };

            var req = new ChatApi.Request
            {
                model = "gpt-3.5-turbo",
            };
            
            //每次都把对话记录一起发过去,AI才能联系上下文
            var history = ChatApi.requestMessageHistory;
            req.messages = new ChatApi.RequestMessage[history.Count + 1];
            for (int i = 0; i < history.Count; i++)
            {
                req.messages[i] = history[i];
            }
            req.messages[^1] = msg;
            history.Add(msg);
            
            return JsonUtility.ToJson(req);
        }

        private static UnityWebRequest sPost = null;
        private static UnityWebRequestAsyncOperation sReq =null;
        private static Action<string> sCallBack;
        public static void InvokeChat(string prompt,Action<string> callBack)
        {     
            sCallBack = callBack;
            var content = CreateChatRequestBody(prompt);
            // POST
              sPost =  UnityWebRequest.Post
                                       (ChatApi.Url, content, "application/json");

            // Request timeout setting
            sPost.timeout = TIMEOUT;

            // API key authorization
            sPost.SetRequestHeader("Authorization", "Bearer " + API_KEY);
  
            // Request start
            sReq = sPost.SendWebRequest();

            Main.instance.RunCoroutine(WaitChatResponse());
        }
        
        

        static IEnumerator WaitChatResponse()
        {
            while (sReq is { isDone: false })
            {
                assistant.response = $"思考中……";
                yield return 1;
            }
         
           
            var json = sPost.downloadHandler.text;
            sPost.Dispose();
            sPost = null;
            var data = JsonUtility.FromJson<ChatApi.Response>(json);
            var content = data.choices[0].message.content;
            sCallBack.Invoke(content);
            
            var msg = new ChatApi.RequestMessage
            {
                role = "assistant",
                content = content,
                name = "gpt",
            };
            ChatApi.requestMessageHistory.Add(msg);
        }
    }
}