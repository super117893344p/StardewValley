using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour,IPointerClickHandler
{
        public int  index  ;
        public Image iconImage ;
        public TextMeshProUGUI countText ; //文本类网格体
        private  SlotData data ;

        public void SetData(SlotData data)
        {
                this.data =data ;
                data.AddListener(OnDataChange); UpdateUI();
        }

        public void OnDataChange()
        {
                UpdateUI();
        }

        public SlotData GetData()
        {
                return this.data ;
        }

        void UpdateUI()
        {
                if (data.item==null)
                {
                        iconImage .enabled=false ;
                        countText .enabled =false;
                }
                else
                {
                        iconImage.enabled=true ;
                        countText.enabled =true ;
                        iconImage .sprite=this.data.item.sprite ;
                        countText.text =data.count.ToString();
                }


        }

        public void OnPointerClick(PointerEventData eventData)
        {
                ItemMoveHandler.Instance.OnSlotClick(this);
        }


}
