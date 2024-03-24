// OpenAIUtil.image.cs
// Created by Cui Lingzhi
// on 2024 - 03 - 24

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace MyScripts
{
    public static partial class OpenAIUtil
    {
        /// <summary>
        /// 创建访问gpt的消息体
        /// </summary>
        /// <returns></returns>
        private static string CreateImageRequestBody(string prompt)
        {
            var req = new CreateImageApi.Request
            {
                prompt = prompt,
                model = "dall-e-2",
                size = "1024x1024",
                response_format = "b64_json",
                style = "vivid"
            };


            return JsonUtility.ToJson(req);
        }

        public static void InvokeCreateImage(string prompt, Action<string> callBack)
        {
            sCallBack = callBack;
            var content = CreateImageRequestBody(prompt);
            sPost = UnityWebRequest.Post
                (CreateImageApi.Url, content, "application/json");
            // Request timeout setting
            sPost.timeout = TIMEOUT;

            // API key authorization
            sPost.SetRequestHeader("Authorization", "Bearer " + API_KEY);

            // Request start
            sReq = sPost.SendWebRequest();
            Main.instance.RunCoroutine(WaitCreateImageResponse());
        }

        private static IEnumerator WaitCreateImageResponse()
        {
            while (sReq is { isDone: false })
            {
                assistant.imageResponse = $"创建中……";
                yield return 1;
            }


            var json = sPost.downloadHandler.text;
            sPost.Dispose();
            sPost = null;
            var data = JsonUtility.FromJson<CreateImageApi.Response>(json);
            sCallBack.Invoke(data.data[0].b64_json);
        }
    }
}