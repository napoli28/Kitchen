using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumTools
{
    public delegate void EnumForEachHandle<T>(T item);
    /// <summary>
    /// 遍历枚举并执行委托函数（参数method），函数必须有一个该枚举类型参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="method"></param>
    public static void ForEachEnumerator<T>(EnumForEachHandle<T> method)where T : Enum
    {
        foreach (T item in Enum.GetValues(typeof(T)))
        {
            method(item);
        }
    }
}
