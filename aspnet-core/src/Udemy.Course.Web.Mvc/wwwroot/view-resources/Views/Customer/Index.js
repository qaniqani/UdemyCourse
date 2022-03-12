(function ($) {
    var _customerService = abp.services.app.customer,
        l = abp.localization.getSource('Course'),
        _$modal = $('#CustomerCreateModal'),
        _$form = _$modal.find('form'),
        _$table = $('#CustomersTable');

    var _$customersTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        listAction: {
            ajaxFunction: _customerService.getAll,
            inputFilter: function () {
                return $('#CustomersSearchForm').serializeFormToObject(true);
            }
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$customersTable.draw(false)
            }
        ],
        responsive: {
            details: {
                type: 'column'
            }
        },
        columnDefs: [
            {
                targets: 0,
                className: 'control',
                defaultContent: '',
            },
            {
                targets: 1,
                data: 'name',
                sortable: false
            },
            {
                targets: 2,
                data: 'surname',
                sortable: false
            },
            {
                targets: 3,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [
                        `   <button type="button" class="btn btn-sm bg-secondary edit-customer" data-customer-id="${row.id}" data-toggle="modal" data-target="#CustomerCreateEditModal">`,
                        `       <i class="fas fa-pencil-alt"></i> ${l('Edit')}`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm bg-danger delete-customer" data-customer-id="${row.id}" data-customer-name="${row.name}">`,
                        `       <i class="fas fa-trash"></i> ${l('Delete')}`,
                        '   </button>',
                    ].join('');
                }
            }
        ]
    });

    _$form.find('.save-button').on('click', (e) => {
        e.preventDefault();

        if (!_$form.valid()) {
            return;
        }

        var customer = _$form.serializeFormToObject();

        abp.ui.setBusy(_$modal);
        _customerService
            .create(customer)
            .done(function () {
                _$modal.modal('hide');
                _$form[0].reset();
                abp.notify.success(l('SavedSuccessfully'));
                _$customersTable.ajax.reload();
            })
            .always(function () {
                abp.ui.clearBusy(_$modal);
            });
    });

    $(document).on('click', '.delete-customer', function () {
        var customerId = $(this).attr("data-customer-id");
        var customerName = $(this).attr('data-customer-name');

        deleteCustomer(customerId, customerName);
    });

    $(document).on('click', '.edit-customer', function (e) {
        var customerId = $(this).attr("data-customer-id");

        e.preventDefault();
        abp.ui.setBusy('body');
        abp.ajax({
            url: abp.appPath + 'Customer/CreateEditModal?customerId=' + customerId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#CustomerCreateEditModal div.modal-content').html(content);
            },
            error: function (e) {
            },
            complete: function (){
                abp.ui.clearBusy('body');
            }
        })
    });
    
    $('#btnCreateOrEdit').click(function(e) {
        e.preventDefault();
        abp.ui.setBusy('body');
        abp.ajax({
            url: abp.appPath + 'Customer/CreateEditModal',
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#CustomerCreateEditModal div.modal-content').html(content);
            },
            error: function (e) {
            },
            complete: function (){
                abp.ui.clearBusy('body');
            }
        })
    })

    abp.event.on('customer.edited', (data) => {
        _$customersTable.ajax.reload();
    });

    abp.event.on('customer.name', (data) => {
        console.log(data)
    });

    function deleteCustomer(customerId, customerName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                customerName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _customerService.delete({
                        id: customerId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$customersTable.ajax.reload();
                    });
                }
            }
        );
    }

    _$modal.on('shown.bs.modal', () => {
        _$modal.find('input:not([type=hidden]):first').focus();
    }).on('hidden.bs.modal', () => {
        _$form.clearForm();
    });

    $('.btn-search').on('click', (e) => {
        _$customersTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$customersTable.ajax.reload();
            return false;
        }
    });
})(jQuery);