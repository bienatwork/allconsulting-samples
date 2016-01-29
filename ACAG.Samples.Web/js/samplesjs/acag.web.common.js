function formatDateToJson(d) {
    var str = Date.UTC(d.getFullYear(), d.getMonth(), d.getDate(), d.getHours(), d.getMinutes(), d.getSeconds(), 0);
    return "\/Date(" + (str) + ")\/"; //"\/Date(" + ( formatUTCDate( new Date() )) + ")\/",
}

function formatDateJsonToDate(date) {
    return new Date(parseInt(date.replace("/Date(", "").replace(")/", ""), 10));
}

function $httpPost(url, param, callback) {
    $.ajax({
        type: 'POST',
        url: url,
        data: angular.toJson(param),
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        processData: true,
        success: callback
    });
}