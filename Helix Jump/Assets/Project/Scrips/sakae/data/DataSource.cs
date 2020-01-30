using FileHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DataSource 
{
    protected IDictionary<Type, IDictionary> dict;

    public void ReadData(string path) 
    {
        var type = typeof(Item);
        var types = type.Assembly.GetTypes();
        var result =
            from t in types
            where t.IsSubclassOf(type)
            select t;
        // Read csv file by class name
        foreach (var item in result)
        {
            var t = typeof(CsvDataSource<>).MakeGenericType(item);
            IDictionary source = null;
            try
            {
                byte[] data = null;

                //
                var s = Resources.Load<TextAsset>("");
                data = s.bytes;
                {
                    source = Activator.CreateInstance(t, data) as IDictionary;
                }

                dict.Add(item, source);
            }
            catch (ConvertException e)
            {
                Debug.LogError(
                    "解析 " + item.Name + " 時發生錯誤 !!\r\n" +
                    "行數 : " + e.LineNumber + ", 欄位 : " + e.ColumnNumber + ", 類型 = " + e.FieldType.Name + ", 名稱 = " + e.FieldName + "\r\n" +
                    e);
            }
            catch (Exception e)
            {
               Debug.LogError("解析 " + item.Name + " 時發生錯誤 !!\r\n" + e);
            }
        }

    }

}
