

//On Add Button Click
function AddQty(e) {
    var qty = parseInt($(e).siblings('.item-qty').text()); //convert to int
    qty += 1;
    $(e).siblings('.item-qty').text(qty);
}

//On Subtract Button Click
function SubtractQty(e) {
    var qty = parseInt($(e).siblings('.item-qty').text()); //convert to int
    qty -= 1;
    $(e).siblings('.item-qty').text(qty);
}

function AddtoCart_Submit(e, itemId, itemName) {
    var qty = $(e).siblings('.item-qty').text();
    var data = {
        id: itemId,
        qty: qty
    }

    //add item with qty to cart
    $.post("/Shopper/CartDetails/AddToCart", data, (result) => {
        console.log("Item Added to Cart");

        //add item to cart
        cartItem = "<div class='col-sm-2 cart-item'> " +
            "<img src='/img/placeholders/placeholder-product.png' width='35' class='col-sm-4 img-thumbnail' >" +
            "<p class='col-sm-7'> <b> " + itemName + "</b> <br> Qty:" + qty + " </p>" +
            "</div>";
        $("#Cart-Summary").append(cartItem);
    });

    //Show buttons
    $(e).parent().siblings().removeClass('hidden');   //show Add Cart Button
    $(e).parent().addClass('hidden');      //hide add cart button
}
