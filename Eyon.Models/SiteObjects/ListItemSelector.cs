using Eyon.Models.Errors;
using Eyon.Models.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eyon.Models.SiteObjects
{
    public class ListItemSelector<T>
        where T : class, INamed, IRecord
    {

        public string PluralName { get; set; }
        public string Name { get; set; }
        public List<T> Items { get; set; }
        
        private string _itemIds;
        
        public string ItemIds
        {
            get
            {
                if (_itemIds == null )
                    SetItemIds();
                return _itemIds;
            }
            set
            {
                _itemIds = value;
            }
        }

        private List<SelectListItem> _selectedItems;
        public List<SelectListItem> SelectedItems 
        { 
            get
            {
                if ( _selectedItems == null )
                    SetSelectedItems();

                return _selectedItems;

            }
            set
            {
                _selectedItems = value;
            }
        }

        public ListItemSelector(string name, bool setIds = true) : this(new List<T>(), name, setIds)
        {
            
        }

        public ListItemSelector( List<T> items, string name = null, bool setIds = true )
        {
            this.Items = items;            
            if ( name != null )
                this.Name = name;
            else
                this.Name = nameof(T);

            if ( setIds == true )
                SetItemIds();
        }

        public void AddListItems (List<T> items)
        {
            Items.AddRange(items);
            SetItemIds();
        }


        private void SetSelectedItems()
        {
            if ( Items != null && Items.Count > 0 )
                _selectedItems = ( from k in Items
                                   select new SelectListItem
                                   {
                                       Text = k.Name,
                                       Value = k.Id.ToString()
                                   } ).ToList();
            else
                _selectedItems = new List<SelectListItem>();
        }


        public string GetListId()
        {
            return string.Format("list_selected_{0}", Name.ToLower());            
        }

        public string GetListItemClass()
        {            
            return string.Format("list_item_selected_{0}", Name.ToLower());            
        }

        public string GetListItemId( string id )
        {
            return string.Format("{0}_{1}", GetListItemClass(), id);
        }
        

        public string GetListItemId(long id )
        {
            return GetListItemId(id.ToString());
        }

        private void SetItemIds()
        {
            if ( Items != null && Items.Count > 0 )
                _itemIds = string.Join(",", Items.Select(x => x.Id.ToString()));
            else
                _itemIds = string.Empty;
        }

        public List<long> ParseItemIds()
        {
            List<long> items = new List<long>();
            if ( !string.IsNullOrEmpty(ItemIds) )
            {
                string[] itemsStringArray = ItemIds.Split(',');

                for ( int i = 0; i < itemsStringArray.Length; i++ )
                {
                    long id = 0;
                    if ( long.TryParse(itemsStringArray[i], out id) )
                        items.Add(id);
                    else
                        throw new SafeException(string.Format("Invalid id: {0} selected.", itemsStringArray[i]));
                }
            }
            return items;
        }
    }
}
