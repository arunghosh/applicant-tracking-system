$(function () {

    $('.save-clse-frm').find('.validation-summary-errors').hide();
    $('.save-clse-frm').find('.frm-busy').hide();
    $('.btn-cancel').each(function () {
        var $this = $(this);
        $this.unbind('click');
        $this.click(function () {
            closePopup($this);
        });
    });

    $('.btn-hide').each(function () {
        var $this = $(this);
        debugger;
        var $form = $this.closest('form');
        $this.unbind('click');
        $this.click(function () {
            $form.find('#IsDeleted').val('True');
            $form.submit();
        });
    });

    $('.btn-unhide').each(function () {
        debugger;
        var $this = $(this);
        var $form = $this.closest('form');
        $this.unbind('click');
        $this.click(function () {
            $form.find('#IsDeleted').val('False');
            $form.submit();
        });
    });

    $('.save-clse-frm').each(function () {
        var $this = $(this);
        var $val = $this.find('.validation-summary-errors');
        $this.unbind('submit');
        $this.submit(function () {
            showFrmBusy($this);
            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize(),
                success: function (result) {
                    if (result.errMsg === undefined || result.errMsg === null) {
                        if ($this.attr('data-refresh') === 'true') {
                            location.reload();
                        }
                        else {
                            $val.show().text("Update Successful");
                            closePopup($this);
                        }
                    }
                    else {
                        var errMsg = result.errMsg;
                        $val.show().text(errMsg);
                    }
                    hideFrmBusy($this);
                }
            });
            return false;
        });
    });

    $('.ntfy-ctnr .close-btn').click(function () {
        var $this = $(this);
        $this.parents('.ntfy-ctnr').hide();
    });
});


function closePopup($this)
{
    $this.parents('.ntfy-ctnr').hide();
    $this.parents('.popover').hide();
    $this.closest('.ui-dialog-content').dialog('close');
}