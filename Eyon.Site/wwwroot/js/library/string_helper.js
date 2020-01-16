function textEscape(rawString) {
    rawString = rawString.replace(/'/g, "\\'");
    rawString = rawString.replace(/"/g, '\\"');
    return rawString;
}