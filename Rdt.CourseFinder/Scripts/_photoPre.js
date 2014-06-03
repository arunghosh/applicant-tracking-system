function showPreview(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#preview')
                .attr('src', e.target.result)
                .height(150);
            $('#previewCtnr').show();
        };
        reader.readAsDataURL(input.files[0]);
    } else {
        $('#previewCtnr').hide();
    }
}