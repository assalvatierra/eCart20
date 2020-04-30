

//On Add Button Click
function AddQty(e) {
    var qty = parseInt($(e).siblings('.item-qty').text()); //convert to int
    if (qty < 100) {
        qty += 1;
    }

    $(e).siblings('.item-qty').text(qty);
}

//On Subtract Button Click
function SubtractQty(e) {
    var qty = parseInt($(e).siblings('.item-qty').text()); //convert to int

    if (qty > 1) {
        qty -= 1;
    }

    $(e).siblings('.item-qty').text(qty);
}

function AddtoCart_Submit(e, itemId, itemName, price) {
    var qty = $(e).siblings('.item-qty').text();
    var data = {
        id: parseInt(itemId),
        qty: parseInt(qty),
        itemName: itemName,
        itemPrice: parseInt(price)
    }

    console.log(data);

    var totalPrice = qty * price;


    //add item with qty to cart
    $.post("/Shopper/CartDetails/AddToCart", data, (response) => {
    //$.post("/Shopper/CartDetails/AddCartItem", data, (response) => {
        console.log(response);
        if (response == '1') {

            console.log("Item Added to Cart");
            //add item to cart
            cartItem = "<div class='col-sm-2 cart-item'> " +
                "<img src='/img/placeholders/placeholder-product.png' width='35' class='col-sm-4 img-thumbnail' >" +
                "<p class='col-sm-7'> <b> " + itemName + "</b> <br> Qty:" + qty + "  &nbsp;&nbsp;&nbsp; " +
                "<span class='text-success'> Price: " + totalPrice + " </span> </p>"+
                "<div><span class='cart-remove-item' onclick='RemoveItem(this,"+ itemId +")'> x </span></div>"+
                "</div>";
            $("#Cart-Summary").append(cartItem);
        }
    });

    //Show buttons
    $(e).parent().siblings().removeClass('hidden');   //show Add Cart Button
    $(e).parent().addClass('hidden');      //hide add cart button
}

function RemoveItem(e,Id) {

    var data = {
        id: Id
    }

    $.post('/Shopper/CartDetails/RemoveCartItem', data, (response) => {
        console.log(response);
    });

    $(e).parent().parent().remove();
}
