using FileHelpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IItem
{
    string Id { get; }
}
/// <summary>
/// 所有要從文字檔讀入的遊戲資料皆須繼承此類別
/// </summary>
[DelimitedRecord("\t")]
public class Item : IItem
{
    [FieldOrder(0)]
    public string Id { get; set; }

    //[FieldOrder(1)]
    //public string Name { get; set; }

    public override string ToString() => Id;
}
