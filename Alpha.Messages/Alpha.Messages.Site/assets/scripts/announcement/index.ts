class Announcement {
    private $: JQueryStatic;
    constraintFromTo = 6;   
    dateRangePickerComponent: DateRangePickerComponent;

    constructor($: JQueryStatic, dateRangePickerComponent: DateRangePickerComponent) {
        this.dateRangePickerComponent = dateRangePickerComponent;
        this.$ = $;
    }

    init = () => {        

        this.registerEvents();
    }

    registerEvents = () => {
        var self = this;

        this.$('.nav-tabs > li').on('click', function () {
            self.$blockUI();
            self.$(this).find('span').val('');
        });
        this.$('.btn-submit').on('click', () => {
            self.$blockUI();
        });

        this.dateRangePickerComponent.render(new DateRangePickerOption("daterange-picker", "report-form", this.constraintFromTo));
    }

    $blockUI = () => {
        this.$.blockUI({
            css: { cursor: "pointer" },
            overlayCSS: { cursor: "pointer" }
        });
    }
}

$(() => {
    const message = new Announcement($, new DateRangePickerComponent($));
    message.init();
});