using FileHelpers;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO;
using UnityEngine;

/// <summary>
/// CSV 格式資料來源
/// </summary>
public class CsvDataSource<T> : Dictionary<string, T> where T : Item
{
    public CsvDataSource(byte[] data)
    {
        if (data == null || data.Length == 0) return;

        var ms = new MemoryStream(data);
        var sr = new StreamReader(ms);
        var engine = new FileHelperAsyncEngine<T>();

        engine.Options.IgnoreEmptyLines = true;
        //engine.Options.IgnoreFirstLines = 1;
        engine.Options.IgnoreCommentedLines.CommentMarker = "#";
        engine.Options.IgnoreCommentedLines.InAnyPlace = true;

        //for (int i = 0; i < engine.Options.FieldsNames.Length; i++)
        //{
        //    UnityEngine.Logger.Log(engine.Options.FieldsNames[i] + " : " + engine.Options.Fields[i]);
        //}

        //var time = DateTime.Now;

        using (engine.BeginReadStream(sr))
        {
            foreach (var item in engine)
            {
                if (ContainsKey(item.Id))
                {
                   Debug.LogError(typeof(T).Name + " 有重複項目 Id = " + item.Id);
                }
                else
                {
                    Add(item.Id, item);
                }
            }
        }

        //Logger.Log(typeof(T).Name + " " + (DateTime.Now - time).TotalMilliseconds);

        ms.Dispose();
        sr.Close();
        sr.Dispose();
        engine.Close();
    }
}
