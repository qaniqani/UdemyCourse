(function ($) {
    var _customerService = abp.services.app.customer,
        l = abp.localization.getSource('Course'),
        _$modal = $('body'),
        _$form = _$modal.find('form');

    _$form.find('.save-button').on('click', (e) => {
        e.preventDefault();
        save()
    });
    
    function save(){
        if (!_$form.valid()) {
            return;
        }

        var customer = _$form.serializeFormToObject();

        abp.ui.setBusy(_$modal);
        _customerService
            .create(customer)
            .done(function () {
                _$form[0].reset();
                abp.notify.success(l('SavedSuccessfully'));
            })
            .always(function () {
                abp.ui.clearBusy(_$modal);
            });
    }

    $('input[type=text]').on('keypress', (e) => {
        if (e.which == 13) {
            e.preventDefault();
            save()
            return false;
        }
    });
})(jQuery);
