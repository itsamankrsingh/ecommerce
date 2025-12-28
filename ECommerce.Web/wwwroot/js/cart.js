function confirmRemove(cartId) {
    Swal.fire({
        title: "Are you sure?",
        text: "This item will be removed from your cart!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#d33",
        cancelButtonColor: "#3085d6",
        confirmButtonText: "Yes, remove it!"
    }).then((result) => {
        debugger;
        if (result.isConfirmed) {
            debugger;
            $.ajax({
                url: '/Customer/Cart/Remove',
                type: 'POST',
                data: { cartId: cartId },
                success: function (data) {
                    $("#cartRow_" + cartId).fadeOut(300, function () {
                        $(this).remove();
                    });

                    const formattedTotal = data.total.toLocaleString("en-IN", {
                        style: "currency",
                        currency: "INR"
                    });

                    $("#orderTotal").text(formattedTotal);

                    toastr.success("Item removed from cart");
                },
                error: function () {
                    toastr.error("Something went wrong");
                }
            });
        }
    });
}
