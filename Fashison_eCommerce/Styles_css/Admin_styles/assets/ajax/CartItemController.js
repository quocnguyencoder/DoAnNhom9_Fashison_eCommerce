var CartItem = {
    init: function () {
        CartItem.flusEvents();
    },
    flusEvents: function () {
        $('.btn').off('click').on('click', function (e) {
            e.preventDefault();
        });
    },

    minusEvents: function () {

    }
}
CartItem.init();