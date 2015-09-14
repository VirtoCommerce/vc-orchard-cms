$(function () {
    var productGridContainer = $(".vc-product-list");
    var btnAddToCart = $("#btn-add-to-cart");
    var btnCreateOrder = $("#btn-create-order");

    if (productGridContainer.length) {
        productGridContainer.masonry({
            itemSelector: ".vc-product"
        });
    }

    btnAddToCart.on("click", function () {
        $.ajax({
            cache: false,
            type: "GET",
            url: btnAddToCart.data("url"),
            success: function (jsonResponse) {
                if (jsonResponse) {
                    updateCartPreview(jsonResponse);
                }
            }
        });
    });

    $(".vc-quantity input").on("blur", function () {
        var element = $(this);
        var lineItemId = element.data("line-item-id");
        $.ajax({
            cache: false,
            type: "GET",
            url: element.data("url") + "&quantity=" + element.val(),
            success: function (jsonResponse) {
                if (jsonResponse) {
                    if (jsonResponse.errorMessage) {
                        alert(jsonResponse.errorMessage);
                    } else {
                        var lineItem = jsonResponse.LineItems.getElementByVal("Id", lineItemId);
                        element.parents("tr").find(".vc-item-price").text("$" + lineItem.LinePrice);
                        updateCartPreview(jsonResponse);
                        updateTotals(jsonResponse);
                    }
                }
            }
        });
    });

    $(".vc-remove-item").on("click", function () {
        var element = $(this);
        $.ajax({
            cache: false,
            type: "GET",
            url: element.data("url"),
            success: function (jsonResponse) {
                if (jsonResponse) {
                    if (jsonResponse.LineItemsCount < 1) {
                        window.location.reload();
                    }
                    element.parents("tr").remove();
                    updateCartPreview(jsonResponse);
                    updateTotals(jsonResponse);
                }
            }
        });
    });

    btnCreateOrder.on("click", function () {
        var checkoutForm = $("#frm-checkout");
        var data = checkoutForm.serialize();
        $.ajax({
            type: "POST",
            url: checkoutForm.prop("action"),
            data: data,
            success: function (jsonResponse) {
                if (jsonResponse) {
                    if (jsonResponse.errorMessage) {
                        alert(jsonResponse.errorMessage);
                    } else if (jsonResponse.redirectUrl) {
                        window.location.href = jsonResponse.redirectUrl;
                    }
                }
            }
        });
    });

    $(".vc-checkout-methods li").on("click", function () {
        var element = $(this);
        element.parents(".vc-checkout-methods").find("input[type='radio']").prop("checked", false);

        var radioButton = element.find("input[type='radio']");
        radioButton.prop("checked", true);

        if (radioButton.prop("name").indexOf("ShippingMethod") >= 0) {
            $.ajax({
                cache: false,
                type: "GET",
                url: radioButton.data("url"),
                success: function (jsonResponse) {
                    if (jsonResponse) {
                        updateTotals(jsonResponse);
                    }
                }
            });
        }
    });
});

Array.prototype.getElementByVal = function (propName, propValue) {
    var el = null;
    for (var i = 0; i < this.length; i++) {
        if (this[i][propName] == propValue) {
            el = this[i];
            break;
        }
    }
    return el;
}

var updateCartPreview = function (data) {
    var cartBadge = $(".vc-cart-preview").find("a");
    if (data.LineItemsCount > 0) {
        cartBadge.html("<span>" + data.LineItemsCount + "</span>");
    } else {
        cartBadge.find("span").remove();
    }
}

var updateTotals = function (data) {
    var totals = $(".vc-totals");
    var liCount = totals.find("li");

    if (liCount == 2) {
        totals.find("li:eq(0) strong").text("$" + data.Subtotal);
        totals.find("li:eq(1) strong").text("$" + data.Total);
    }
    if (liCount == 3) {
        totals.find("li:eq(0) strong").text("$" + data.Subtotal);
        totals.find("li:eq(1) strong").text("$" + data.ShippingPrice);
        totals.find("li:eq(2) strong").text("$" + data.Total);
    }
}