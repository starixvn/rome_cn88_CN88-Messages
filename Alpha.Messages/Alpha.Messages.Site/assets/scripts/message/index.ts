class Message {
    private $: JQueryStatic;

    constructor($: JQueryStatic) {
        this.$ = $;
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

        this.initDateTimePicker('#fromDateContainer');
        this.initDateTimePicker('#toDateContainer');
    }

    private initDateTimePicker = (id: string) => {
        this.$(id).datetimepicker({
            allowInputToggle: true,
            format: 'YYYY-MM-DD',
            ignoreReadonly: true
        });
    }

    $blockUI = () => {
        this.$.blockUI({
            css: { cursor: "pointer" },
            overlayCSS: { cursor: "pointer" }
        });
    }
}

$(() => {
    const message = new Message($);
    message.registerEvents();
});