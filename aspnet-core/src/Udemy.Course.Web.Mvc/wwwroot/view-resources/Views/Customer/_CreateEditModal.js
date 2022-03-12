(function ($) {
    var _customerService = abp.services.app.customer,
        l = abp.localization.getSource('Course'),
        _$modal = $('#CustomerCreateEditModal'),
        _$form = _$modal.find('form');

    function save() {
        if (!_$form.valid()) {
            return;
        }

        var customer = _$form.serializeFormToObject();

        abp.ui.setBusy(_$form);
        _customerService
            .createOrUpdate(customer)
            .done(function () {
                _$modal.modal('hide');
                abp.notify.info(l('SavedSuccessfully'));
                abp.event.trigger('customer.edited', customer);
        }).always(function () {
            abp.ui.clearBusy(_$form);
        });
    }

    _$form.closest('div.modal-content').find(".save-button").click(function (e) {
        e.preventDefault();
        save();
    });

    _$form.closest('div.modal-content').find("#lblName").click(function (e) {        
        var txtName = _$form.closest('div.modal-content').find("#name");
        
        abp.event.trigger('customer.name', `Customer Name tiklandi: ${txtName.val()}`);
    });

    _$form.find('input').on('keypress', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            save();
        }
    });

    _$modal.on('shown.bs.modal', function () {
        _$form.find('input[type=text]:first').focus();
    });
})(jQuery);


