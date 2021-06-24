var SignOut = {
    init: function()
    {
        SignOut.regEvents();
    },
    regEvents: function(){
        $('#SignOut').off('click').on('click', function(){
            $.ajax({
                url: '/Home/SignOut',
                dataType: 'json',
                type: 'POST',
                success: function(res){
                    if(res.status == true){
                        window.location.href = "/Login";
                    }
                }
            })
        })        
    }
}
SignOut.init();