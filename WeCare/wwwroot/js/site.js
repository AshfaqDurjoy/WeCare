$(document).ready(function () {
    var nav = $('.wecare-nav');
    $(window).on('scroll', function () {
        if ($(this).scrollTop() > 50) {
            nav.addClass('scrolled');
        } else {
            nav.removeClass('scrolled');
        }
    });

    $('.select-card').on('click', function () {
        var target = $(this).data('target');
        if (target) {
            window.location.href = target;
        }
    });

    $('.password-toggle').on('click', function () {
        var input = $(this).siblings('input');
        if (input.length === 0) {
            input = $(this).closest('.form-floating, .position-relative').find('input');
        }
        var type = input.attr('type') === 'password' ? 'text' : 'password';
        input.attr('type', type);
        $(this).toggleClass('bi-eye').toggleClass('bi-eye-slash');
    });

    $('.org-type-select').on('change', function () {
        var selected = $(this).val();
        var label = selected === 'Hospital' ? 'Hospital Name' : 'Organization Name';
        $('.org-name-label').text(label);
    });

    $('.realtime-validate').on('input change', function () {
        var isValid = this.checkValidity();
        $(this).toggleClass('is-invalid', !isValid);
        $(this).toggleClass('is-valid', isValid);
    });

    $('.login-required').on('click', function () {
        var message = $(this).data('login-message') || 'Login required to continue.';
        $('.login-required-message').text(message);
    });

    $('#eventImageInput').on('change', function () {
        var preview = $('#eventImagePreview');
        var file = this.files && this.files[0];
        if (!file) {
            preview.addClass('d-none');
            preview.attr('src', '');
            return;
        }

        var reader = new FileReader();
        reader.onload = function (e) {
            preview.removeClass('d-none');
            preview.attr('src', e.target.result);
        };
        reader.readAsDataURL(file);
    });

    $('#eventCreateForm').on('submit', function () {
        $('#eventSubmitText').text('Posting...');
        $('#eventSubmitSpinner').removeClass('d-none');
        $(this).find('button[type="submit"]').prop('disabled', true);
    });

    $('.respond-event').on('click', function () {
        var button = $(this);
        var eventId = button.data('event-id');
        var token = $('input[name="__RequestVerificationToken"]').val();

        $.ajax({
            url: '/Event/Respond',
            method: 'POST',
            data: { eventId: eventId, __RequestVerificationToken: token },
            success: function (response) {
                if (response.success) {
                    button.prop('disabled', true).text('Responded');
                    return;
                }

                $('.login-required-message').text(response.message);
                var modal = new bootstrap.Modal(document.getElementById('loginRequiredModal'));
                modal.show();
            }
        });
    });
});
