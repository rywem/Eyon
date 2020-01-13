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

        public ListItemSelector( List<T> items, string pluralName, string name = null )
        {
            this.Items = items;
            this.PluralName = pluralName;
            if ( name != null )
                this.Name = name;
            else
                this.Name = nameof(T);
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
                        throw new WebUserSafeException(string.Format("Invalid id: {0} selected.", itemsStringArray[i]));
                }
            }
             
            return items;
        }
    }
}
