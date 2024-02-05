using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemMoveHandler : MonoBehaviour
{
    public static ItemMoveHandler Instance {get; private set ;}
     private Image icon ; //Image属性有sprite 和 enable
     private SlotData selectedSlotData;
     private bool  isCtrlDown =false ;
     private Player player ;

     void Awake()
     {
         Instance =this ;
         icon =GetComponentInChildren<Image>() ;
         player =FindAnyObjectByType<Player>() ;
         HideIcon();
      }

     void Update()
     {
         Vector2 position;
         RectTransformUtility.ScreenPointToLocalPointInRectangle(
             GetComponent<RectTransform>(), Input.mousePosition,
             null,
             out position);
         icon.GetComponent<RectTransform>().anchoredPosition = position;

         if (Input.GetMouseButtonDown(0))
         {
             if (EventSystem.current.IsPointerOverGameObject() == false)
             {
                 ThrowItem();
             }
         }

         if (Input.GetKeyDown(KeyCode.LeftControl))
         {
             isCtrlDown = true;
         }

         if (Input.GetKeyUp(KeyCode.LeftControl))
         {
             isCtrlDown = false;
         }

         if (Input.GetMouseButtonDown(1))
         {
             ClearHandForced();
         }
     }

     public void OnSlotClick(SlotUI slotui )

     {
         if (selectedSlotData!=null)
         {
             if (slotui.GetData().IsEmpty())
             {
                 MoveToEmptySlot(selectedSlotData ,slotui.GetData());
             }
             else
             {
                 if (selectedSlotData.item == slotui.GetData().item)
                 {
                     MoveToEmptySlot(selectedSlotData,slotui.GetData());
                 }
                 else
                 {
                     SwitchData(selectedSlotData, slotui.GetData());
                 }
             }
         }
         else
         {
             if (slotui.GetData().IsEmpty()) return;
             selectedSlotData = slotui.GetData();
             ShowIcon(selectedSlotData.item.sprite);
         }
     }

     void HideIcon()
     {
         icon.enabled =false ;
     }

     void ShowIcon(Sprite sprite)
     {
         icon .enabled =true ;
         icon.sprite=sprite ;
     }

     void ClearHand()
     {
         if (selectedSlotData.IsEmpty())
         {
              selectedSlotData=null ; HideIcon();
         }
     }
     void ClearHandForced()
     {
         HideIcon();
         selectedSlotData = null;
     }

     void ThrowItem()
     {
         if (selectedSlotData!=null)
         {
             GameObject prefab = selectedSlotData.item.prefab ;
             int count =selectedSlotData.count ;
             if (isCtrlDown)
             {
                 player.ThrowItem(prefab ,1);
                 selectedSlotData.Reduce();
             }
             else
             {
                 player.ThrowItem(prefab ,count);
                 selectedSlotData.Clear();
             }
             ClearHand();
         }
     }

     void MoveToEmptySlot(SlotData formData, SlotData toData)
     {
         if (isCtrlDown)
         {
             toData.AddItem(formData .item );
             formData.Reduce();
         }
         else
         {
              toData.MoveSlot(formData);formData.Clear();
         }
         ClearHand();
     }

     void MoveToNotEmptySlot(SlotData fromData, SlotData toData)
     {
         if (isCtrlDown )
         {
             if ( toData .CanAddItem())
             {
                 toData.Add(); fromData.Reduce();
             }
             else
             {
                 int  freespace = toData.GetFreeSpace();
                 if (freespace<  fromData.count)
                 {
                       toData.Add(freespace); fromData.Reduce(freespace);
                 }
                 else
                 {
                     toData.Add(fromData.count); fromData.Clear();
                 }
             }
         }ClearHand() ;
     }

     void SwitchData(SlotData data1 , SlotData data2 )
     {
          ItemData data =data1.item ;
          int count =data1. count ;
          data1.MoveSlot(data2);
          data2.AddItem(data,count );ClearHandForced();
     }

}
