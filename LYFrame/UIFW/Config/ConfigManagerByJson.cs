﻿/***
 * 
 *  基于Json 配置文件的“配置管理器”  
 *
 *   
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LYFrame
{
	public class ConfigManagerByJson : IConfigManager
	{
        //保存（键值对）应用设置集合
	    private static Dictionary<string, string> _AppSetting;

        /// <summary>
        /// 只读属性： 得到应用设置（键值对集合）
        /// </summary>
	    public Dictionary<string, string> AppSetting
	    {
	        get { return _AppSetting; }
	    }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="jsonPath">Json配置文件路径</param>
	    public ConfigManagerByJson(string jsonPath)
	    {
	        _AppSetting=new Dictionary<string, string>();
            //初始化解析Json 数据，加载到（_AppSetting）集合。
            InitAndAnalysisJson(jsonPath);
	    }

        /// <summary>
        /// 得到AppSetting 的最大数值
        /// </summary>
        /// <returns></returns>
	    public int GetAppSettingMaxNumber()
	    {
            if (_AppSetting!=null && _AppSetting.Count>=1)
            {
                return _AppSetting.Count;
            }
            else
            {
                return 0;
            }
	    }

        /// <summary>
        /// 初始化解析Json 数据，加载到集合众。
        /// </summary>
        /// <param name="jsonPath"></param>
	    private void InitAndAnalysisJson(string jsonPath)
        {
            TextAsset configInfo = null;
            KeyValuesInfo keyvalueInfoObj = null;
            Debug.Log(jsonPath);
            //参数检查
            if (string.IsNullOrEmpty(jsonPath)) return;
            //解析Json 配置文件
            try{
                configInfo = Resources.Load<TextAsset>(jsonPath);
                keyvalueInfoObj=JsonUtility.FromJson<KeyValuesInfo>(configInfo.text);
            }
            catch{
                throw new JsonAnlysisException(GetType() + "/InitAndAnalysisJson()/Json Analysis Exception ! Parameter jsonPath=" + jsonPath);
            }
            //数据加载到AppSetting 集合中
            foreach (KeyValuesNode nodeInfo in keyvalueInfoObj.ConfigInfo)
            {
                _AppSetting.Add(nodeInfo.Key,nodeInfo.Value);
            }
        }



	}
}