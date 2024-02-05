using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackUI : MonoBehaviour
{
  private GameObject parentUI ;
  public List<SlotUI> slotuiList ;

  void Awake()
  {
    parentUI =transform.Find("ParentUI").gameObject ;

  }

  void Start()
  {
      InitUI();
  }

  void Update()
  {
      if (Input.GetKeyDown(KeyCode.Tab))
      {
          ToggleUI();  //打开背包物品
      }
  }
  void InitUI()
  {
       slotuiList =new List<SlotUI>(new SlotUI[24]) ;
       SlotUI[] list =transform.GetComponentsInChildren<SlotUI>() ;
       foreach ( SlotUI slotUi in list)
       {
             slotuiList[slotUi.index] = slotUi ;
       }
        UpdateUI();
  }
  private void ToggleUI()
  {
      parentUI.SetActive(!parentUI.activeSelf);
  }

  public void OnCloseClick()
  {
      ToggleUI();
  }

  public void UpdateUI()
  {
      List<SlotData > slotdataList= InventoryManager.Instance .backpack
          .slotList ; //继承了scriptableObject 共享所有public属性
      for (int i = 0; i<slotdataList.Count;  i++)
      {
             slotuiList[i].SetData(slotdataList[i]);
      }

  }

}
