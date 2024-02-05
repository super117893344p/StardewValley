using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarUI : MonoBehaviour
{
    public List<ToolbarSlotUI>slotuiList ;
    private ToolbarSlotUI selectedSlotUI ;

    void Start()
    {
        InitUI() ;
    }

    void Update()
    {
        ToolbarSelectControl();
    }

    public ToolbarSlotUI  GetSelectedSlotUI()
    {
        return  selectedSlotUI ;
    }
    void InitUI()
    {
      slotuiList = new List<ToolbarSlotUI> (new ToolbarSlotUI[9]) ;
      ToolbarSlotUI[] array =transform. GetComponentsInChildren<ToolbarSlotUI>() ;
      foreach ( ToolbarSlotUI slotUi in array)
      {
           slotuiList[slotUi.index] =slotUi ;
      }
      UpdateUI();
    }

    public void UpdateUI()
    {

        List<SlotData> slotdataList = InventoryManager.Instance.toolbarData.slotList;

        for (int i = 0; i < slotdataList.Count; i++)
        {
            slotuiList[i].SetData(slotdataList[i]);
        }
    }

    void ToolbarSelectControl()
    {
        for (int i =(int)KeyCode.Alpha1; i<= (int)KeyCode.Alpha9 ; i++)
        {
            if (Input.GetKeyDown((KeyCode)i)) //判断按下的值与 i是否相等  keycode ==i
               // KeyCode 和 i 是不同类型的值，不能直接进行相等性比较。
            {
                if (selectedSlotUI!=null)
                {
                     selectedSlotUI.UnHighlight();
                }
                int index = (int)(i-KeyCode.Alpha1 );
                selectedSlotUI.Highlight();
                selectedSlotUI=  slotuiList [index] ;
            }
        }



    }
}
