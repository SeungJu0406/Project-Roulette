using System;
using System.Collections.Generic;
using System.Text;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utility
{
    public static class Util
    {
        private static Dictionary<float, WaitForSeconds> _delayDic = new Dictionary<float, WaitForSeconds>();
        private static Dictionary<float, WaitForSecondsRealtime> _realDelayDic = new Dictionary<float, WaitForSecondsRealtime>();

        private static StringBuilder _sb = new StringBuilder();

        public static WaitForSeconds Second(this float delay)
        {
            float normalize = Mathf.Round(delay * 100f) * 0.01f;

            if (_delayDic.ContainsKey(normalize) == false)
            {
                _delayDic.Add(normalize, new WaitForSeconds(normalize));
            }
            return _delayDic[normalize];
        }


        public static WaitForSecondsRealtime RealSecond(this float delay)
        {
            float normalize = Mathf.Round(delay * 100f) * 0.01f;

            if (_realDelayDic.ContainsKey(normalize) == false)
            {
                _realDelayDic.Add(normalize, new WaitForSecondsRealtime(normalize));
            }
            return _realDelayDic[normalize];
        }

        public static StringBuilder GetSB(this string text)
        {
            _sb.Clear();
            _sb.Append(text);
            return _sb;
        }

        public static int ToIndex<TEnum>(TEnum e) where TEnum : Enum
        {
#if UNITY_EDITOR
            return Convert.ToInt32(e);
#else
            return Unity.Collections.LowLevel.Unsafe.UnsafeUtility.As<TEnum, int>(ref e);  
#endif
        }

        /// <summary>
        /// �÷����� ���İ�(����) ������ ���
        /// </summary>
        public static Color GetColor(this Color color, float a)
        {
            color.a = a;
            return color;
        }
        /// <summary>
        /// ������Ʈ�� GameObject�� �߰��ϰų�, �̹� �����ϴ� ������Ʈ�� ��ȯ�մϴ�.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T GetOrAddComponent<T>(this GameObject obj) where T : Component
        {
            return obj.TryGetComponent(out T comp) ? comp : obj.AddComponent<T>();
        }
    }
}