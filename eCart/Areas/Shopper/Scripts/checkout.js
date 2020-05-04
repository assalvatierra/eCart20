function RemoveItem(e, Id, subtotal) {
    var data = {
        id: Id
    }

    $.post('/Shopper/CartDetails/RemoveCartItem', data, (response) => {
        console.log(response);
        UpdatePrice(subtotal);
    });

    $(e).parent().parent().remove();
}


function UpdatePrice(removedsubtotal) {
    var totalPrice = parseFloat($('#Total-Price').text());
    totalPrice -= removedsubtotal;
    $('#Total-Price').text(totalPrice.toLocaleString('en-US', { minimumFractionDigits: 2 }));
}

function SubmitOrder(e, cartId) {

    $.post("/Shopper/CartDetails/SubmitOrder", { id: cartId }, (result) => {
        console.log(result);
        $(e).attr("disabled", true);
        $("#CheckOutSuccessModal").modal('show');
    });
}

function SubmitAllOrder() {
    $.post("/Shopper/CartDetails/SubmitAllOrder", null, (result) => {
        console.log(result);
        $(":button").attr("disabled", "disabled");
        $("#CheckOutSuccessModal").modal('show');
    });
}

function DisableAllButton() {
    var hasActive = '@Model.Any(s => s.CartStatusId == 1)';
    if (hasActive) {
        $(".btn-primary").attr("disabled", "disabled");
    }
}
