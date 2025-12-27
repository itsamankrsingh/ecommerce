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
                type: 'POST', // better than GET for delete
                data: { cartId: cartId },
                success: function (data) {
                    toastr.success("Item removed from cart");
                    setTimeout(function () {
                        location.reload();
                    }, 1000); // 1 second delay
                },
                error: function () {
                    toastr.error("Something went wrong");
                }
            });
        }
    });
}
