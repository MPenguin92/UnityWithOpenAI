// OpenAIData.Image.cs
// Created by Cui Lingzhi
// on 2024 - 03 - 23

namespace MyScripts
{
    /// <summary>
    /// API reference : https://platform.openai.com/docs/api-reference/images
    /// </summary>
    public static class CreateImageApi
    {
        public const string Url = "https://api.openai.com/v1/images/generations";
        
        [System.Serializable]
        public struct Request
        {
            /// <summary>
            ///  [Required] 图片描述
            /// </summary>
            public string prompt;
            /// <summary>
            /// [Optional] 选择模型,默认是dall-e-2
            /// </summary>
            public string model;
            /// <summary>
            /// [Optional] 图片尺寸,默认是1024x1024
            /// </summary>
            public string size;
            /// <summary>
            /// [Optional] 生成的图片源,默认是url,我们应该用b64_json,再转码成图片
            /// </summary>
            public string response_format;

            /// <summary>
            /// [Optional] 风格,默认是vivid,可选natural
            /// </summary>
            public string style;
            
            //还有很多很多可选字段,详见API reference
        }

        [System.Serializable]
        public struct Data
        {
            public string b64_json;
            public string url;
        }

        [System.Serializable]
        public struct Response
        {
            public string created;
            public Data[] data;

        }
    }
}