﻿class ListItemSelector {
    itemIdsArray = [];
    itemNamesArray = [];
    name = '';    
    
    constructor(objName, initialItemIdsArray, initialNamesArray, elementId_ItemIds, functionToCreateListItem) {
        this.itemIdsArray = initialItemIdsArray;
        this.itemNamesArray = initialNamesArray;
        this.name = objName;
        this.elementIdItemIds = elementId_ItemIds;        
        this.createListItemFunction = functionToCreateListItem;
    }

    // public 
    updateSelected(id, name) {
        this.itemIdsArray = this.addOrRemoveItemFromArray(this.itemIdsArray, id);
        this.itemNamesArray = this.addOrRemoveItemFromArray(this.itemNamesArray, name);
        this.buildUI();
    }
    // public 
    containsId(id) {
        if (typeof this.itemIdsArray !== "undefined" && this.itemIdsArray !== null) {
            return this.itemIdsArray.includes(id);
        }
        return false;
    }

    buildUI() {
        this.printCommaSeparatedStringToDocumentId();
        this.buildNamesList();
    }

    // Private 
    addOrRemoveItemFromArray(arrayToChange, theValueToAddOrRemove) {
        var val = theValueToAddOrRemove;
        if (typeof arrayToChange !== "undefined" && arrayToChange !== null) {
            if (arrayToChange.includes(val)) {
                arrayToChange = arrayToChange.filter(function (value, index, arr) {
                    return value != val;
                });
            }
            else {
                arrayToChange.push(val);
            }
        }
        else {
            arrayToChange = [val];
        }
        return arrayToChange;
    }

    // Prints a comma separated string to a textbox
    printCommaSeparatedStringToDocumentId() {
        document.getElementById(this.elementIdItemIds).value = this.itemIdsArray.toString();
    }

    buildNamesList() {
        var list = $('#'+this.getListId());
        list.empty();
        var self = this;
        for (var i = 0; i < this.itemNamesArray.length; i++) {
            var id = this.itemIdsArray[i];
            var li_id = this.getListItemId(this.itemIdsArray[i]);
            var name = this.itemNamesArray[i];
            var li_class = this.getListItemClass();
            var self = this.self;            
            list.append(this.createListItemFunction(id, name, li_id, li_class, self));
        }
    }

    getListId() {
        return 'list_selected_' + this.name.toLowerCase();
    }

    getListItemClass() {
        return "list_item_selected_" + this.name.toLowerCase();
    }
    getListItemId(id) {
        return this.getListItemClass() + '_' + id;
    }
}