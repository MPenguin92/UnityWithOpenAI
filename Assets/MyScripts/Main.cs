// Main.cs
// Created by Cui Lingzhi
// on 2024 - 03 - 23

using System;
using System.Collections;
using UnityEngine;

namespace MyScripts
{
    public class Main : MonoBehaviour
    {
        private static Main sInstance;

        public static Main instance
        {
            get
            {
                if (sInstance == null)
                {
                    sInstance = GameObject.FindFirstObjectByType<Main>();
                }

                return sInstance;
            }
        }

        public void RunCoroutine(IEnumerator enumerator)
        {
            StartCoroutine(enumerator);
        }

    }
}