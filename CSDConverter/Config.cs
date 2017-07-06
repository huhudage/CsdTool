using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Text;

public class LocalConfig
{
    // 存放所有的配置信息
    private Dictionary<string, object> mConfigs = new Dictionary<string, object>();

    // 构建
    public LocalConfig()
    {
    }

    public LocalConfig(string content)
    {
        var lines = Explode(content, "\r\n");
        foreach (string l in lines)
        {
            string line = l.Trim();
            if (string.IsNullOrEmpty(line))
                continue;
            if (line[0] == '#')
                continue;
            string[] arr = Explode(line, "=");
            if (arr.Length != 2)
                continue;
            arr[0] = arr[0].Trim();
            arr[1] = arr[1].Trim();
            if (string.IsNullOrEmpty(arr[0]))
                continue;
            Add(arr[0], arr[1]);
        }
    }

    // 序列化
    public void Serialize(Stream stream)
    {
        foreach (var kv in mConfigs)
        {
            string l = string.Format("{0}={1}\r\n", kv.Key, kv.Value);
            var b = Encoding.UTF8.GetBytes(l);
            stream.Write(b, 0, b.Length);
        }
    }

    // 打断字符串
    public static string[] Explode(string path, string seperator)
    {
        return path.Split(seperator.ToCharArray());
    }

    // 添加一个配置信息
    public void Add(string k, object v)
    {
        mConfigs[k] = v;
    }

    // 取得配置信息
    public T Get<T>(string k)
    {
        if (mConfigs.ContainsKey(k))
            return (T)mConfigs[k];
        return default(T);
    }
}
