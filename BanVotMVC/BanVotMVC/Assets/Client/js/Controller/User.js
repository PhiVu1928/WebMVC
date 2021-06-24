var Login = {
    init: function()
    {
        Login.regEvents();
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
        });
        $('#SignIn').off('click').on('click',function(){
            $.ajax({
                url: '/User/Login',
                dataType: 'json',
                type: 'POST',
                success: function(res){
                    if(res.status == true){
                        window.location.href = "/";
                    }
                }
            })
        });       
    }
}
Login.init();