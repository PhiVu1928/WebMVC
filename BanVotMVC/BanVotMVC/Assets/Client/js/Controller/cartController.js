var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('#btnContinue').off('click').on('click', function () {
            window.location.href = "/";
        });    
        $('#btnPayment').off('click').on('click', function (){
            window.location.href = "/thanh-toan";
        })     
        $('#btnUpdate').off('click').on('click', function(){
            var listproduct = $('.cart_quantity_input');
            var cartlist = [];
            $.each(listproduct,function(i, item){
                cartlist.push({
                    Quantity: $(item).val(),
                    Product: {
                        id: $(item).data('id')
                    }
                })
            });
            $.ajax({
                data: { cartModel: JSON.stringify(cartlist) },
                url: '/Cart/Update',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            })
        });
        $('.cart_quantity_delete').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                data: { id: $(this).data('id') },
                url: '/Cart/Delete',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            })
        });
        $('#btnDeleteAll').off('click').on('click', function () {
            $.ajax({
                url: '/Cart/DeleteAll',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            })
        })
    }
}
cart.init();