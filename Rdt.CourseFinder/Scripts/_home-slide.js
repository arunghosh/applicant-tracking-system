//$(function () {
//    var offerTimer = setInterval(function () { dispOffer() }, 2000);
//    var offerCnt = 0;
//    var offerCache = new Array(10);
//    function dispOffer() {
//        if (offerCache[offerCnt] === undefined) {
//            var cnt = offerCnt;
//            $.ajax({
//                url: '/Admin/SpecialOffer/Show/' + offerCnt,
//                type: "get",
//                success: function (result) {
//                    debugger;
//                    offerCache[cnt] = result;
//                    $('#hmeOffer').html(result);
//                }
//            });
//        }
//        else {
//            $('#hmeOffer').html(offerCache[offerCnt]);
//        }
//        offerCnt = (offerCnt + 1) % 3;
//    }
//});

