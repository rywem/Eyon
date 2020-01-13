class ListItemSelector {
    itemIdsArray = [];
    constructor(localName, initialItemIdsArray, initialNamesArray) {
        this.itemIdsArray = initialItemIdsArray;
        this.itemNamesArray = initialNamesArray;
        this.LocalName = localName;
    }

    printIds() {
        if (this.itemIdsArray !== 'undefined' && this.itemIdsArray !== '') {
            for (var i = 0; i < this.itemIdsArray.length; i++) {
                console.log(this.itemIdsArray[i]);
            }
        }
    }

    updateSelected(id, name) {
        this.addOrRemoveItemFromArray(this.itemIdsArray, id);
        this.addOrRemoveItemFromArray(this.itemNamesArray, name);
    }

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
    }

    // Prints a comma separated string to a textbox
    printCommaSeparatedStringToDocumentId(htmlElementId) {
        document.getElementById(htmlElementId).value = this.itemIdsArray.toString();
    }

    buildNamesList(ul_or_ol_jquery_selector, li_class, data) {
        var list = $(ul_or_ol_jquery_selector);
        list.empty();
        for (var i = 0; i < this.itemNamesArray.length; i++) {
            var id = li_class + "_" + this.itemIdsArray[i];
            var name = this.itemNamesArray[i];
            list.append(`<li id="${id}" class="${li_class}">${name}<i class="far fa-window-close"></i></li>`)
        }
    }
}