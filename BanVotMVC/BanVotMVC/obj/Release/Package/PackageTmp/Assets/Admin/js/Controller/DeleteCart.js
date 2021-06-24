var DeleteCart = {
    init: function()
    {
        DeleteCart.regEvents();
    },
    regEvents: function(){
        $('#delete').off('click').on('click', function(e){
            e.preventDefault();
            $.ajax({
                data : {id: $(this).data('id')},
                url: 'Cart/Delete',
                dataType: 'json',
                type : 'POST',
                success : function(res){
                    if(res.status == true)
                    {
                        window.location.href = "/Cart/Index";
                    }
                }
            })
        })
    }
}
DeleteCart.init();