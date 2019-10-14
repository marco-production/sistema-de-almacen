$(document).ready(function () {
    $.ajax({
        url: '../Productoes/ObtenerId',
        type: 'get',
        dataType: 'json',
        
        success: function (data) {
            var html;
                for (var i = 0; i < data.length; i++) {
                    html += "<option>" + data[i] + "</option>";
            }
                $('#option').html(html);
            }
    });
});