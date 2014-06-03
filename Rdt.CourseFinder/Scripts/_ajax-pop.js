// Show pop-up + busy indicator then the content
$(function () {
    $('.notify-icon').each(function () {
        var $this = $(this);
        $this.find('.ntfy-trig').click(function () {
            $this.find('.ntfy-ctnr').show();
            //var url = $this.attr('data-url');
            //$('.ntfy-ctnr').hide();
            //$this.find('.ntfy-ctnr').show().html('<div class="arrow"></div><img src="/Content/images/busy.gif" style="padding:10px 0;" />');
            //$.ajax({
            //    url: url,
            //    type: "Get",
            //    success: function (result) {
            //        $('.ntfy-ctnr').hide();
            //        $this.find('.ntfy-ctnr').show().html(result);
            //    }
            //});
        });
    });
});
