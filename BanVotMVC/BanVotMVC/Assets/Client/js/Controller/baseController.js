var common = {
    init: function () {
        common.registerEvent();
    },
    registerEvent: function () {
        $('#txtKeyword').autocomplete({
            minLength: 0,
            source: function( request, response ) {
                $.ajax({
                    url: '/Product/ListName',
                    dataType: 'json',
                    data: {
                        q: request.term
                    },
                    success: function( res ) {
                        response(res.data);
                    }
                });
            }            
    }
}
common.init();