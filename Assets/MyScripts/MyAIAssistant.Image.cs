// MyAIWindow.Image.cs
// Created by Cui Lingzhi
// on 2024 - 03 - 23

using System;
using System.IO;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace MyScripts
{
    public partial class MyAIAssistant
    {
        [TabGroup("画图")]
        [TextArea] [LabelText("图片关键词")] public string inputImagePrompt;
        
        [TabGroup("画图")]
        [Multiline(10)] [LabelText("回答")] public string imageResponse;
        
        private bool mIsWaitingImage = false;

        [TabGroup("画图")]
        [HideLabel]
        [ProgressBar(0, 100, r: 1, g: 1, b: 1, Height = 30,DrawValueLabel = false)]
        public float BigColoredProgressBar = 0;

        [TabGroup("画图")]
        [Button("生成", ButtonSizes.Large)]
        public void CreateImage()
        {
            if (mIsWaitingImage)
                return;
            BigColoredProgressBar = 0;
            mIsWaitingImage = true;
            OpenAIUtil.InvokeCreateImage(inputImagePrompt, CreateImageCallBack);
        }

        private void CreateImageCallBack(string result)
        {
            mIsWaitingImage = false;
            BigColoredProgressBar = 100;
            // 解码Base64数据
            byte[] imageData = Convert.FromBase64String(result);

            // 创建新的Texture2D
            Texture2D texture = new Texture2D(512, 512);
            // 将解码后的数据加载到Texture2D中
            texture.LoadImage(imageData);


            // 将Texture2D转换为字节数组
            byte[] bytes = texture.EncodeToPNG();

            string outputPath = Path.Combine("Output", "textures", "temp.png");
            // 将字节数组写入到文件
            UnityEngine.Windows.File.WriteAllBytes(Path.Combine(Application.dataPath,outputPath) ,bytes);

            imageResponse = $"已经成功创建到{outputPath}";
            
            AssetDatabase.Refresh();
        }
    }
}