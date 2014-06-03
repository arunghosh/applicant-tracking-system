$(function () {
    $('.ntfy-lst').slimScroll({
        height: 350
    });

    $('.ntfy-lnk').each(function () {
        var $this = $(this);
        $this.unbind('click');
        $this.click(function () {
            var id = $this.attr('data-id');
            var $ctnr = $this.closest('.ntfy-pop');
            var url = $ctnr.attr('data-url');
            document.location.href = url + id;
        });
    });

    $('.new-obj').each(function () {
        var $this = $(this);
        $this.unbind('click');
        $this.click(function () {
            showDialog($this, null);
        })
    });

    // Connect request accptance
    $('.nty-cnt-frm form').submit(function () {
        $this = $(this);
        showBusy($this);
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                $this.parents('.nty-cnt-frm').html(result.status)
            }
        });
        return false;
    });
});