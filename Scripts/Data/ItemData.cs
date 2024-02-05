using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    None,
    Seed_Carrot,
    Seed_Tomato,
    Hoe
}

[CreateAssetMenu()]
public class ItemData :ScriptableObject //MonoBehaviour 用于创建游戏对象的行为和逻辑
                                        //，而 ScriptableObject 用于创建可重用的数据对象， 相当于一个数据库类型的脚本组件
                                        //并且具有在编辑器中方便操作和持久存储数据的特性
{
  public ItemType  type=  ItemType.None  ;
  public Sprite sprite ;
  public GameObject prefab ;
  public int maxCount =1 ;

}
