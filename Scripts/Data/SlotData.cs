using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SlotData
{
     public ItemData item ;
     public  int count ; private Action OnChange ; //行为委托

     public void MoveSlot(SlotData data)
     {
          this.item=data.item ;
          this.count =data.count ;
          OnChange ?.Invoke() ;
     }

     public bool IsEmpty()
     {
          return this.count==0 ;
     }

     public bool CanAddItem()
     {
          return count <item.maxCount ;
     }

     public int GetFreeSpace()
     {
          return this.item.maxCount-this.count ;
     }

     public void Add(int numToAdd=1 )
     {
          this.count+=numToAdd ;OnChange? .Invoke() ;
     }

     public void AddItem(ItemData item , int count=1)
     {
          this.item =item ;
          this.count =count ;OnChange? .Invoke() ;
     }

     public void Reduce(int numToReduce=1)
     {

          if (this.count >0)
          {
                count-=numToReduce ;OnChange? .Invoke() ;
          }
          else
          {
               Clear() ;
          }
     }

     public void Clear()
     {
          this.count =0 ; this.item =null ;
          OnChange? .Invoke() ;
     }

     public void AddListener(Action action)
     {
          this.OnChange =action ;
     }

}
