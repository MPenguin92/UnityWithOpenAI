// OpenAIData.cs
// Created by Cui Lingzhi
// on 2024 - 03 - 22

using System.Collections.Generic;

namespace MyScripts
{
    /// <summary>
    /// API reference : https://platform.openai.com/docs/api-reference/chat
    /// </summary>
    public static class ChatApi
    {
        public const string Url = "https://api.openai.com/v1/chat/completions";
        
        [System.Serializable]
        public struct RequestMessage
        {
            /// <summary>
            /// [Required] 提示词(内容)
            /// </summary>
            public string content;
            /// <summary>
            /// [Required] 角色(system/user/....)
            /// </summary>
            public string role;
            /// <summary>
            /// [Optional] 一个可选字段,用来区分是不同的用户还是相同的用户
            /// </summary>
            public string name;
        }

        public static List<RequestMessage> requestMessageHistory = new List<RequestMessage>();

        [System.Serializable]
        public struct Request
        {
            /// <summary>
            ///  [Required] 对话消息列表
            /// </summary>
            public RequestMessage[] messages;
            /// <summary>
            /// [Required] 选择模型
            /// </summary>
            public string model;
                
            //还有很多很多可选字段,详见API reference

        }
        
        [System.Serializable]
        public struct ResponseMessage
        {
            public string role;
            public string content;
        }
        
        [System.Serializable]
        public struct ResponseChoice
        {
            public int index;
            public ResponseMessage message;
            //停止的原因,返回stop为正常停止,其他情况详见API reference
            public string finish_reason;
        }

        [System.Serializable]
        public struct Response
        {
            //A unique identifier for the chat completion.
            public string id;
            //回复的内容,数组是因为可能回复多条数据,取决于请求时的可选参数n..
            public ResponseChoice[] choices;
        }
    }
}