"use strict";

class AlphaMessages {
    $: JQueryStatic;

    constructor($: JQueryStatic) {
        this.$ = $;
    }

    format = {
        monthFormat: "MMMM - yyyy",
        shortDateWithSlash_DatePicker: "YYYY-MM-DD",
        shortDateWithSlash_MonthPicker: "yyyy-MM-dd"
    };

    sort = {
        none: 0,
        ascending: 1,
        descending: -1
    };
    
}

const alphaMessages = new AlphaMessages($);

interface JQuery {
    stickyTableHeaders(): void;
}